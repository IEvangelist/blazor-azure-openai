namespace Azure.OpenAI.Client.Models;

public readonly record struct VoicePreferences(
    SpeechSynthesisVoice Voice, 
    double Rate);
