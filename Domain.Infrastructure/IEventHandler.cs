using System;
using System.Collections.Generic;
using System.Text;
using Domain.Infrastructure.Events;
using Domain.Infrastructure.Messaging;

namespace Domain.Infrastructure
{
    public interface IMessageHandler<in TMessage> where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}
