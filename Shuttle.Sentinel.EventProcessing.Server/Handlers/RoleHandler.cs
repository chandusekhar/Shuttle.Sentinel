﻿using Shuttle.Core.Infrastructure;
using Shuttle.Recall;
using Shuttle.Sentinel.DomainEvents.Role.v1;

namespace Shuttle.Sentinel.EventProcessing.Server
{
    public class RoleHandler :
        IEventHandler<Added>,
        IEventHandler<PermissionAdded>,
        IEventHandler<PermissionRemoved>
    {
        private readonly ISystemRoleQuery _query;

        public RoleHandler(ISystemRoleQuery query)
        {
            Guard.AgainstNull(query, "query");

            _query = query;
        }

        public void ProcessEvent(IEventHandlerContext<Added> context)
        {
            _query.Added(context.ProjectionEvent, context.DomainEvent);
        }

        public void ProcessEvent(IEventHandlerContext<PermissionAdded> context)
        {
            _query.PermissionAdded(context.ProjectionEvent, context.DomainEvent);
        }

        public void ProcessEvent(IEventHandlerContext<PermissionRemoved> context)
        {
            _query.PermissionRemoved(context.ProjectionEvent, context.DomainEvent);
        }
    }
}