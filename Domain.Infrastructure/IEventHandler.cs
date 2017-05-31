using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure.Events;
using Domain.Infrastructure.Messaging;

namespace Domain.Infrastructure
{
    public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent message);
    }
}
