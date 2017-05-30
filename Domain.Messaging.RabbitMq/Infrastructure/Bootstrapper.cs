using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Infrastructure.Events;
using Domain.Infrastructure.Messaging;
using MassTransit.Courier.Contracts;
using MassTransit.RabbitMqTransport;
using Microsoft.Practices.Unity;

namespace Domain.Messaging.RabbitMq.Infrastructure
{
    public static class BootstrapperExtension
    {
        private static IUnityContainer ConfigureRabbitMq(this IUnityContainer container,
            Action<IRabbitMqReceiveEndpointConfigurator> configure)
        {
            try
            {
                container.RegisterInstance<IMessageBus>(new RabbitMqMassTransitDomainEventBus(configure));
            }
            catch (Exception exception)
            {
                throw new Exception("Could not configure MassTransit for RabbitMq connection. See inner exception",
                    exception);
            }

            return container;
        }
    }
}
