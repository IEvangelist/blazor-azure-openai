// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Azure.OpenAI.Client.Models;

public class VoicePreferences(ILocalStorageService storage)
{
    const string PreferredVoiceKey = "preferred-voice";
    const string PreferredSpeedKey = "preferred-speed";
    const string TtsIsEnabledKey = "tts-is-enabled";

    private string? _voice;
    private double? _rate;
    private bool? _isEnabled;

    public string? Voice
    {
        get => _voice ??= storage.GetItem<string>(PreferredVoiceKey);
        set
        {
            if (_voice != value && value is not null)
            {
                _voice = value;
                storage.SetItem<string>(PreferredVoiceKey, value);
            }
        }
    }

    public double Rate
    {
        get => _rate ??= storage.GetItem<double>(PreferredSpeedKey) is double rate && rate > 0 ? rate : 1;
        set
        {
            if (_rate != value)
            {
                _rate = value;
                storage.SetItem<double>(PreferredSpeedKey, value);
            }
        }
    }

    public bool IsEnabled
    {
        get => _isEnabled ??= (storage.GetItem<bool?>(TtsIsEnabledKey) is { } enabled && enabled);
        set
        {
            if (_isEnabled != value)
            {
                _isEnabled = value;
                storage.SetItem<bool?>(TtsIsEnabledKey, value);
            }
        }
    }

    public void Deconstruct(out string? voice, out double rate, out bool isEnabled) =>
        (voice, rate, isEnabled) = (Voice, Rate, IsEnabled);
}
