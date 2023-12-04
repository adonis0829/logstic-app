﻿using Logistics.Application.Core;
using Logistics.Domain.Entities;
using Logistics.Domain.Persistence;
using Logistics.Shared;

namespace Logistics.Application.Admin.Commands;

internal sealed class UpdateSubscriptionPlanHandler : RequestHandler<UpdateSubscriptionPlanCommand, ResponseResult>
{
    private readonly IMasterUnityOfWork _masterUow;

    public UpdateSubscriptionPlanHandler(IMasterUnityOfWork masterUow)
    {
        _masterUow = masterUow;
    }

    protected override async Task<ResponseResult> HandleValidated(
        UpdateSubscriptionPlanCommand req, CancellationToken cancellationToken)
    {
        var subscriptionPlan = await _masterUow.Repository<SubscriptionPlan>().GetByIdAsync(req.Id);

        if (subscriptionPlan is null)
        {
            return ResponseResult.CreateError($"Could not find a subscription plan with ID '{req.Id}'");
        }

        if (!string.IsNullOrEmpty(req.Name) && subscriptionPlan.Name != req.Name)
        {
            subscriptionPlan.Name = req.Name;
        }
        if (!string.IsNullOrEmpty(req.Description) && subscriptionPlan.Description != req.Description)
        {
            subscriptionPlan.Description = req.Description;
        }
        if (req.Price.HasValue && subscriptionPlan.Price != req.Price)
        {
            subscriptionPlan.Price = req.Price.Value;
        }
        
        _masterUow.Repository<SubscriptionPlan>().Update(subscriptionPlan);
        await _masterUow.SaveChangesAsync();
        return ResponseResult.CreateSuccess();
    }
}
