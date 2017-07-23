﻿using Shuttle.Core.Data;
using Shuttle.Core.Infrastructure;
using Shuttle.Sentinel.Query;

namespace Shuttle.Sentinel
{
    public class SubscriptionQueryFactory : ISubscriptionQueryFactory
    {
        public IQuery All()
        {
            return RawQuery.Create(@"
select
    MessageType,
    InboxWorkQueueUri
from
    SubscriberMessageType
order by
    MessageType
");
        }

        public IQuery Add(Subscription subscription)
        {
            Guard.AgainstNull(subscription, "subscription");

            return RawQuery.Create(@"
if not exists (select null from SubscriberMessageType where MessageType = @MessageType and InboxWorkQueueUri = @InboxWorkQueueUri)
    insert into SubscriberMessageType
    (
        MessageType,
        InboxWorkQueueUri
    ) 
    values 
    (
        @MessageType,
        @InboxWorkQueueUri
    )
")
                .AddParameterValue(SubscriptionColumns.MessageType, subscription.MessageType)
                .AddParameterValue(SubscriptionColumns.InboxWorkQueueUri, subscription.InboxWorkQueueUri);
        }

        public IQuery Remove(Subscription subscription)
        {
            Guard.AgainstNull(subscription, "subscription");

            return RawQuery.Create(@"
delete 
from 
    SubscriberMessageType 
where 
    MessageType = @MessageType 
and 
    InboxWorkQueueUri = @InboxWorkQueueUri
")
                .AddParameterValue(SubscriptionColumns.MessageType, subscription.MessageType)
                .AddParameterValue(SubscriptionColumns.InboxWorkQueueUri, subscription.InboxWorkQueueUri);
        }
    }
}