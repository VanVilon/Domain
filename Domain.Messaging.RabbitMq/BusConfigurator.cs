using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.vNext;

namespace Domain.Messaging.RabbitMq
{
    public static class BusConfigurator
    {
        public static IBusClient ConfigureBus(RawRabbitConfiguration config)
        {
            var client = BusClientFactory.CreateDefault(config);
            return client;
        }
    }
}
