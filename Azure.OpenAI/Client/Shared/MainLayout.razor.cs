// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Shared;

public sealed partial class MainLayout
{
    const string PrefersDarkThemeKey = "prefers-dark-scheme";

    readonly MudTheme _theme = new();
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

    void ShowCultureDialog() => Dialog.Show<CultureDialog>(
        Localizer["SelectLanguageTitle"]);

    void OnMenuClicked() => _drawerOpen = !_drawerOpen;
    void OnThemeChanged() => _isDarkTheme = !_isDarkTheme;
}
