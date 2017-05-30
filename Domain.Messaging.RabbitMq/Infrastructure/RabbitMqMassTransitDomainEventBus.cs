using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Infrastructure.Messaging;
using GreenPipes;
using MassTransit;
using MassTransit.Configuration;
using MassTransit.ConsumeConfigurators;
using MassTransit.RabbitMqTransport;

namespace Domain.Messaging.RabbitMq.Infrastructure
{
    public class RabbitMqMassTransitDomainEventBus : MassTransitDomainEventBus
    {
        public RabbitMqMassTransitDomainEventBus(Action<IRabbitMqReceiveEndpointConfigurator> configure)
            : base(ConfigureBus(configure))
        {
        }

        private static IBusControl ConfigureBus(Action<IRabbitMqReceiveEndpointConfigurator> configure)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.UseRetry(retrySbc =>
                {
                    retrySbc.SetRetryPolicy(filter => filter.Immediate(5));
                });

                sbc.ConnectConsumerConfigurationObserver(new ReceiveObserver());
                sbc.ReceiveEndpoint(host, configure);
            });

            return busControl;
        }
    }

    public class ReceiveObserver :
        IConsumerConfigurationObserver
    {
        public void ConsumerConfigured<TConsumer>(IConsumerConfigurator<TConsumer> configurator) where TConsumer : class
        {
            File.WriteAllText("C:\\Users\\Magorion\\Desktop\\test.txt", "Configured");
        }

        public void ConsumerMessageConfigured<TConsumer, TMessage>(
            IConsumerMessageConfigurator<TConsumer, TMessage> configurator)
            where TConsumer : class where TMessage : class
        {
            File.WriteAllText("C:\\Users\\Magorion\\Desktop\\test.txt", "Message_configured");
        }
    }
}