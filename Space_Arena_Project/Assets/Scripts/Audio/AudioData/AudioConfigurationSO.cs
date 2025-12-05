using UnityEngine;
using UnityEngine.Audio;

//TODO: Check which settings we really need at this level
[CreateAssetMenu(menuName = "Audio/Audio Configuration")]
public class AudioConfigurationSO : ScriptableObject
{
    public AudioMixerGroup OutputAudioMixerGroup = null;

    // Simplified management of priority levels (values are counterintuitive, see enum below)
    [SerializeField] private PriorityLevel _priorityLevel = PriorityLevel.Standard;
    [HideInInspector]
    public int Priority
    {
        get { return (int)_priorityLevel; }
        set { _priorityLevel = (PriorityLevel)value; }
    }

    public float Time { get => _time; }
    public bool UseFade { get => _useFade; }
    public float FadeDuration { get => _fadeDuration; }

    [Header("Sound properties")]
    public bool Mute = false;
    [Range(0f, 1f)] public float Volume = 1f;
    [Range(-3f, 3f)] public float Pitch = 1f;
    [Range(-1f, 1f)] public float PanStereo = 0f;
    [Range(0f, 1.1f)] public float ReverbZoneMix = 1f;

    [Header("Spatialisation")]
    [Range(0f, 1f)] public float SpatialBlend = 1f;
    public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;
    [Range(0.01f, 5f)] public float MinDistance = 0.1f;
    [Range(5f, 100f)] public float MaxDistance = 50f;
    [Range(0, 360)] public int Spread = 0;
    [Range(0f, 5f)] public float DopplerLevel = 1f;

    [Header("Ignores")]
    public bool BypassEffects = false;
    public bool BypassListenerEffects = false;
    public bool BypassReverbZones = false;
    public bool IgnoreListenerVolume = false;
    public bool IgnoreListenerPause = false;

    [Header("Other")]
    [SerializeField, Range(0f, 1f)] float _time = 0f;
    [SerializeField] bool _usePitchRange = false;
    [SerializeField] Vector2 _pitchRange = default;
    [Space]
    [SerializeField] bool _useFade = false;
    [SerializeField] float _fadeDuration = 5f;

    private enum PriorityLevel
    {
        Highest = 0,
        High = 64,
        Standard = 128,
        Low = 194,
        VeryLow = 256,
    }

    public void ApplyTo(AudioSource audioSource)
    {
        audioSource.outputAudioMixerGroup = this.OutputAudioMixerGroup;
        audioSource.mute = this.Mute;
        audioSource.bypassEffects = this.BypassEffects;
        audioSource.bypassListenerEffects = this.BypassListenerEffects;
        audioSource.bypassReverbZones = this.BypassReverbZones;
        audioSource.priority = this.Priority;
        audioSource.volume = this.Volume;

        if (_usePitchRange)
        {
            var _randomPitch = Random.Range(_pitchRange.x, _pitchRange.y);
            audioSource.pitch = _randomPitch;
        }
        else
        {
            audioSource.pitch = this.Pitch;
        }

        audioSource.panStereo = this.PanStereo;
        audioSource.spatialBlend = this.SpatialBlend;
        audioSource.reverbZoneMix = this.ReverbZoneMix;
        audioSource.dopplerLevel = this.DopplerLevel;
        audioSource.spread = this.Spread;
        audioSource.rolloffMode = this.RolloffMode;
        audioSource.minDistance = this.MinDistance;
        audioSource.maxDistance = this.MaxDistance;
        audioSource.ignoreListenerVolume = this.IgnoreListenerVolume;
        audioSource.ignoreListenerPause = this.IgnoreListenerPause;
        //audioSource.time = _time;
    }
}
