﻿using Logistics.Models;

namespace Logistics.Application.Admin.Queries;

internal sealed class GetAppRolesHandler : RequestHandler<GetAppRolesQuery, PagedResponseResult<AppRoleDto>>
{
    private readonly IMainRepository _repository;

    public GetAppRolesHandler(IMainRepository repository)
    {
        _repository = repository;
    }

    protected override Task<PagedResponseResult<AppRoleDto>> HandleValidated(
        GetAppRolesQuery req, CancellationToken cancellationToken)
    {
        var totalItems = _repository.Query<AppRole>().Count();

        var rolesDto = _repository
            .ApplySpecification(new SearchAppRoles(req.Search))
            .Skip((req.Page - 1) * req.PageSize)
            .Take(req.PageSize)
            .Select(i => new AppRoleDto()
            {
                Name = i.Name,
                DisplayName = i.DisplayName
            })
            .ToArray();
        
        var totalPages = (int)Math.Ceiling(totalItems / (double)req.PageSize);
        return Task.FromResult(new PagedResponseResult<AppRoleDto>(rolesDto, totalItems, totalPages));
    }
}