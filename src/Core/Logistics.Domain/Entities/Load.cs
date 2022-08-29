﻿using Logistics.Domain.ValueObjects;

namespace Logistics.Domain.Entities;

public class Load : Entity, ITenantEntity
{
    private LoadStatus _status = LoadStatus.Dispatched;
    
    public ulong RefId { get; set; } = 100_000;
    public string? Name { get; set; }
    public string? SourceAddress { get; set; }
    public string? DestinationAddress { get; set; }
    public decimal DeliveryCost { get; set; }
    public double Distance { get; set; }
    public DateTime DispatchedDate { get; set; } = DateTime.UtcNow;
    public DateTime? PickUpDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    
    public LoadStatus Status
    {
        get => _status;
        set
        {
            _status = value;
            if (_status == LoadStatus.PickedUp)
            {
                PickUpDate = DateTime.UtcNow;
                DeliveryDate = null;
            }
            else if (_status == LoadStatus.Delivered)
            {
                DeliveryDate = DateTime.UtcNow;
            }
            else if (_status == LoadStatus.Dispatched)
            {
                PickUpDate = null;
                DeliveryDate = null;
            }
        }
    }
    
    public string? AssignedDispatcherId { get; set; }
    public string? AssignedDriverId { get; set; }
    public string? AssignedTruckId { get; set; }

    public virtual Truck? AssignedTruck { get; set; }
    public virtual Employee? AssignedDispatcher { get; set; }
    public virtual Employee? AssignedDriver { get; set; }
}