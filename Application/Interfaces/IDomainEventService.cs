﻿using System.Threading.Tasks;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
