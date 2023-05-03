// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Shared;

public sealed partial class MainLayout
{
    readonly MudTheme _theme = new();
    bool _drawerOpen = true;

    bool IsDarkTheme
    {
        get => LocalStorage.GetItem<bool>(StorageKeys.PrefersDarkThemeKey);
        set => LocalStorage.SetItem<bool>(StorageKeys.PrefersDarkThemeKey, value);
    }

    bool IsRightToLeft =>
        Thread.CurrentThread.CurrentUICulture is { TextInfo.IsRightToLeft: true };

    bool IsDeleteDisabled =>
        SessionStorage.Length is 0;

    [Inject] public required ILocalStorageService LocalStorage { get; set; }
    [Inject] public required ISessionStorageService SessionStorage { get; set; }
    [Inject] public required IDialogService Dialog { get; set; }
    [Inject] public required IStringLocalizer<MainLayout> Localizer { get; set; }
    [Inject] public required AppState State { get; set; }

    void ShowCultureDialog() => Dialog.Show<CultureDialog>(Localizer["SelectLanguageTitle"]);
    void OnMenuClicked() => _drawerOpen = !_drawerOpen;
    void OnThemeChanged() => IsDarkTheme = !IsDarkTheme;
    void OnDeleteClicked() => State.DeleteClicked();
}
