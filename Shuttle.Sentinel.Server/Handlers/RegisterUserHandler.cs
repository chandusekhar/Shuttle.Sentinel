﻿using System;
using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Shuttle.Esb.EMail;
using Shuttle.Recall;
using Shuttle.Sentinel.DomainEvents.User.v1;
using Shuttle.Sentinel.Messages.v1;

namespace Shuttle.Sentinel.Server
{
    public class RegisterUserHandler : IMessageHandler<RegisterUserCommand>
    {
        private readonly IDatabaseContextFactory _databaseContextFactory;
        private readonly IEventStore _eventStore;
        private readonly IKeyStore _keyStore;
        private readonly ILog _log;

        public RegisterUserHandler(IDatabaseContextFactory databaseContextFactory, IEventStore eventStore, IKeyStore keyStore)
        {
            Guard.AgainstNull(databaseContextFactory, "databaseContextFactory");
            Guard.AgainstNull(eventStore, "eventStore");
            Guard.AgainstNull(keyStore, "keyStore");

            _databaseContextFactory = databaseContextFactory;
            _eventStore = eventStore;
            _keyStore = keyStore;

            _log = Log.For(this);
        }

        public void ProcessMessage(IHandlerContext<RegisterUserCommand> context)
        {
            var message = context.Message;

            if (string.IsNullOrEmpty(message.Username))
            {
                return;
            }

            if (string.IsNullOrEmpty(message.RegisteredBy))
            {
                return;
            }

            var id = Guid.NewGuid();

            Registered registered;

            using (_databaseContextFactory.Create())
            {
                var key = User.Key(message.Username);

                if (_keyStore.Contains(key))
                {
                    return;
                }

                _keyStore.Add(id, key);

                var user = new User(id);
                var stream = new EventStream(id);

                registered = user.Register(message.Username, message.PasswordHash, message.RegisteredBy);

                stream.AddEvent(registered);

                _eventStore.SaveEventStream(stream);
            }

            context.Publish(new UserRegisteredEvent
            {
                Username = message.Username,
                RegisteredBy = message.RegisteredBy,
                DateRegistered = registered.DateRegistered
            });
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}