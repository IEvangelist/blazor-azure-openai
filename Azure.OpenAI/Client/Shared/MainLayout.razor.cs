// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Shared;

public sealed partial class MainLayout
{
    const string PrefersDarkThemeKey = "prefers-dark-scheme";

    MudTheme _theme = new();
    bool _drawerOpen = true;
    bool _isDarkTheme
    {
        get => LocalStorage.GetItem<bool>(PrefersDarkThemeKey);
        set => LocalStorage.SetItem<bool>(PrefersDarkThemeKey, value);
    }

    bool _isRightToLeft =>
        Thread.CurrentThread.CurrentUICulture is { TextInfo.IsRightToLeft: true };

    [Inject] public required ILocalStorageService LocalStorage { get; set; }
    [Inject] public required IDialogService Dialog { get; set; }
    [Inject] public required IStringLocalizer<MainLayout> Localizer { get; set; }

    string SelectLanguageTitle => Localizer[nameof(SelectLanguageTitle)];

    void ShowCultureDialog() => Dialog.Show<CultureDialog>(SelectLanguageTitle);

    void DrawerToggle() => _drawerOpen = !_drawerOpen;

    void OnToggledChanged() => _isDarkTheme = !_isDarkTheme;
}
