﻿namespace Logistics.Domain.Common;

public abstract class Entity : IEntity<string>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}