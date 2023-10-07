﻿using Logistics.Shared.Enums;
using Logistics.DriverApp.Models;
using Logistics.DriverApp.Services;
using Logistics.Models;

namespace Logistics.DriverApp.ViewModels;

public class LoadPageViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IApiClient _apiClient;
    private readonly IMapsService _mapsService;
    private string? _lastLoadId;

    public LoadPageViewModel(IApiClient apiClient, IMapsService mapsService)
    {
        _apiClient = apiClient;
        _mapsService = mapsService;
        ConfirmPickUpCommand = new AsyncRelayCommand(() => ConfirmLoadStatusAsync(LoadStatus.PickedUp));
        ConfirmDeliveryCommand = new AsyncRelayCommand(() => ConfirmLoadStatusAsync(LoadStatus.Delivered));
    }

    
    #region Commands

    public IAsyncRelayCommand ConfirmPickUpCommand { get; }
    public IAsyncRelayCommand ConfirmDeliveryCommand { get; }

    #endregion

    
    #region Bindable properties

    private ActiveLoad? _load;
    public ActiveLoad? Load
    {
        get => _load;
        set => SetProperty(ref _load, value);
    }

    private string? _embedMapHtml;
    public string? EmbedMapHtml
    {
        get => _embedMapHtml;
        set => SetProperty(ref _embedMapHtml, value);
    }

    #endregion

    
    protected override async Task OnAppearingAsync()
    {
        await FetchLoadAsync();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query["load"] is ActiveLoad load)
        {
            Load = load;
        }
        else if (query["load"] is LoadDto loadDto)
        {
            Load = new ActiveLoad(loadDto);
        }

        if (Load is not null)
        {
            EmbedMapHtml = GetEmbedMapHtml(Load);
            _lastLoadId = Load.Id;
        }
    }

    private async Task FetchLoadAsync()
    {
        if (string.IsNullOrEmpty(_lastLoadId) || _lastLoadId == Load?.Id)
        {
            return;
        }
        
        IsLoading = true;
        var result = await _apiClient.GetLoadAsync(_lastLoadId);
        
        if (!result.IsSuccess)
        {
            await PopupHelpers.ShowErrorAsync("Failed to fetch the load data, try later");
            IsLoading = false;
            return;
        }

        Load = new ActiveLoad(result.Data!);
        EmbedMapHtml = GetEmbedMapHtml(Load);
        IsLoading = false;
    }

    private async Task ConfirmLoadStatusAsync(LoadStatus status)
    {
        if (Load is null)
            return;
        
        IsLoading = true;
        ResponseResult? result = default;

        switch (status)
        {
            case LoadStatus.PickedUp:
                Load.Status = LoadStatus.PickedUp;
                Load.CanConfirmPickUp = false;
                result = await _apiClient.ConfirmLoadStatusAsync(new ConfirmLoadStatus
                {
                    LoadId = Load.Id,
                    LoadStatus = LoadStatus.PickedUp
                });
                break;
            case LoadStatus.Delivered:
                Load.Status = LoadStatus.Delivered;
                Load.CanConfirmDelivery = false;
                result = await _apiClient.ConfirmLoadStatusAsync(new ConfirmLoadStatus
                {
                    LoadId = Load.Id,
                    LoadStatus = LoadStatus.Delivered
                });
                break;
        }

        if (result is { IsSuccess: false })
        {
            await PopupHelpers.ShowErrorAsync($"Could not confirm the load status, error: {result.Error}");
            IsLoading = false;
            return;
        }

        Load = default;
        await FetchLoadAsync();
        IsLoading = false;
    }

    private string GetEmbedMapHtml(ActiveLoad load)
    {
        var originAddress = $"{load.OriginAddressLat},{load.OriginAddressLong}";
        var destinationAddress = $"{load.DestinationAddressLat},{load.DestinationAddressLong}";
        var embedMapHtml = _mapsService.GetDirectionsMapHtml(originAddress, destinationAddress);
        return embedMapHtml;
    }
}
