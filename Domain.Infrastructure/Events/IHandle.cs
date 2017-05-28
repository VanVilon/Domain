using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.Events
{
    public interface IHandle<in TEvent> where TEvent: IDomainEvent
    {
        void Handle(TEvent @eEvent);
    }
}
