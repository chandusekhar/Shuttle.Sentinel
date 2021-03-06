﻿using System.Collections.Generic;

namespace Shuttle.Sentinel.Messages.v1
{
    public class RegisterSystemMetricsCommand
    {
        public RegisterSystemMetricsCommand()
        {
            SystemMetrics = new List<SystemMetric>();
        }

        public List<SystemMetric> SystemMetrics { get; set; }

        public class SystemMetric
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}