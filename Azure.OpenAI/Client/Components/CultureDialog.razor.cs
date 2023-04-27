// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Components;

public sealed partial class CultureDialog
{
    static readonly Lazy<RegionInfo?> s_lazyRegion = new(
        () => TryNewRegionInfoCtor(CultureInfo.CurrentCulture.LCID));

    [Inject] public required NavigationManager Navigation { get; set; }
    [Inject] public required ILocalStorageService LocalStorage { get; set; }
    [Inject] public required ILogger<CultureDialog> Logger { get; set; }
    [Inject] public required CultureService CultureService { get; set; }
    [Inject] public required IStringLocalizer<CultureDialog> Localizer { get; set; }

    private IDictionary<CultureInfo, AzureCulture>? _supportedCultures;
    private CultureInfo _selectedCulture = CultureInfo.CurrentCulture;

    protected override async Task OnInitializedAsync()
    {
        _supportedCultures = await CultureService.GetAvailableCulturesAsync();
    }

    void OnSaveCulture()
    {
        if (CultureInfo.CurrentCulture != _selectedCulture)
        {
            CultureInfo.CurrentCulture = _selectedCulture;
            LocalStorage.SetItem(StorageKeys.AppCultureKey, _selectedCulture.Name);
            Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
        }
    }

    static string GetCultureTwoLetterRegionName(CultureInfo? culture = null) =>
        (culture is null ? s_lazyRegion.Value ?? TryNewRegionInfoCtor() : TryNewRegionInfoCtor(culture.LCID))
            ?.TwoLetterISORegionName
            ?.ToLowerInvariant() ?? "en";

    static RegionInfo? TryNewRegionInfoCtor(int? lcid = null)
    {
        try
        {
            return new RegionInfo(lcid.GetValueOrDefault());
        }
        catch (Exception ex)
        {
            _ = ex;
            return default!;
        }
    }
}
