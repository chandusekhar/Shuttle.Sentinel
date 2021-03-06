﻿using System;
using System.Collections.Generic;
using System.IO;
using Shuttle.Esb;

namespace Shuttle.Sentinel.Queues
{
    public interface IInspectionQueue
    {
        void Enqueue(string sourceQueueUri, TransportMessage transportMessage, Stream stream);
        IEnumerable<InspectionMessage> Messages();
        void Remove(Guid messageId);
        InspectionMessage Get(Guid messageId);
    }
}