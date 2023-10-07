﻿using Logistics.Client.Models;
using Logistics.Shared;
using Logistics.Shared.Models;

namespace Logistics.Client.Abstractions;

public interface ITruckApi
{
    Task<ResponseResult<TruckDto>> GetTruckAsync(GetTruckQuery query);
    Task<PagedResponseResult<TruckDto>> GetTrucksAsync(SearchableRequest request, bool includeLoads = false);
    Task<ResponseResult> CreateTruckAsync(CreateTruck truck);
    Task<ResponseResult> UpdateTruckAsync(UpdateTruck truck);
    Task<ResponseResult> DeleteTruckAsync(string id);
}
