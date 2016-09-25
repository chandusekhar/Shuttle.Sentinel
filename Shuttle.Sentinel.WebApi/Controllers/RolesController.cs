using System;
using System.Linq;
using System.Web.Http;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Shuttle.Sentinel.Messages.v1;

namespace Shuttle.Sentinel.WebApi
{
    public class RolesController : SentinelApiController
    {
        private readonly IServiceBus _bus;
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly ISystemRoleQuery _systemRoleQuery;

        public RolesController(IServiceBus bus, IDatabaseContextFactory databaseContextFactory, ISystemRoleQuery systemRoleQuery)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(bus, "bus");
            Guard.AgainstNull(systemRoleQuery, "systemRoleQuery");

            _databaseContextFactory = databaseContextFactory;
            _bus = bus;
            _systemRoleQuery = systemRoleQuery;
        }

        [RequiresPermission(SystemPermissions.View.Roles)]
        public IHttpActionResult Get()
        {
            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = from row in _systemRoleQuery.Search()
                        select new
                        {
                            Id = SystemRoleColumns.Id.MapFrom(row),
                            Rolename = SystemRoleColumns.RoleName.MapFrom(row)
                        }
                });
            }
        }

        [RequiresPermission(SystemPermissions.Manage.Roles)]
        public IHttpActionResult Get(Guid id)
        {
            using (_databaseContextFactory.Create())
            {
                return Ok(new
                {
                    Data = _systemRoleQuery.Get(id)
                });
            }
        }

        [RequiresPermission(SystemPermissions.Manage.Roles)]
        public IHttpActionResult Delete(Guid id)
        {
            _bus.Send(new RemoveRoleCommand
            {
                Id = id
            });

            return Ok();
        }

        [RequiresPermission(SystemPermissions.Manage.Roles)]
        public IHttpActionResult Post([FromBody] RegisterRoleModel model)
        {
            Guard.AgainstNull(model, "model");

            _bus.Send(new AddRoleCommand
            {
                Name = model.Name
            });

            return Ok();
        }
    }
}