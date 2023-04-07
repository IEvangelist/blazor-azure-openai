// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Shared;

public sealed partial class MainLayout
{
    MudTheme _theme = new();
    bool _drawerOpen = true;
    bool _isDarkTheme
    {
        get => LocalStorage.GetItem<bool>(StorageKeys.PrefersDarkThemeKey);
        set => LocalStorage.SetItem<bool>(StorageKeys.PrefersDarkThemeKey, value);
    }

    bool _isRightToLeft =>
        Thread.CurrentThread.CurrentUICulture is { TextInfo.IsRightToLeft: true };

    [Inject] public required ILocalStorageService LocalStorage { get; set; }
    [Inject] public required IDialogService Dialog { get; set; }
    [Inject] public required IStringLocalizer<MainLayout> Localizer { get; set; }

    string SelectLanguageTitle => Localizer[nameof(SelectLanguageTitle)];
    string SwitchToDarkTheme => Localizer[nameof(SwitchToDarkTheme)];
    string SwitchToLightTheme => Localizer[nameof(SwitchToLightTheme)];
    string ToggleNavBar => Localizer[nameof(ToggleNavBar)];
    string VisitGitHubRepository => Localizer[nameof(VisitGitHubRepository)];

    void ShowCultureDialog() => Dialog.Show<CultureDialog>(SelectLanguageTitle);

    void DrawerToggle() => _drawerOpen = !_drawerOpen;

    void OnToggledChanged() => _isDarkTheme = !_isDarkTheme;
}
