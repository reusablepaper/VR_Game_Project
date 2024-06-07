using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = new GameObject("Sound Manager").AddComponent<SoundManager>();
            }

            return _instance;
        }
    }

    [Header("BGM")]
    [SerializeField] private AudioClip[] _bgmClips;
    private float _bgmVolume;
    private AudioSource _bgmPlayer;


    [Header("SFX")]
    private float _sfxVolume;
    [SerializeField] private int _sfxChannels;
    private int _sfxIndex;
    private AudioSource[] _sfxPlayers;

    void Awake()
    {
        Init();
        
    }

    void Init()
    {
        // 배경음 플레이어 세팅
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        _bgmPlayer = bgmObject.AddComponent<AudioSource>();
        _bgmPlayer.playOnAwake = false;
        _bgmPlayer.loop = true;
        _bgmPlayer.volume = _bgmVolume;

        // 효과음 플레이어 세팅
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        _sfxPlayers = new AudioSource[_sfxChannels];
        for (int i = 0; i < _sfxChannels; i++)
        {
            _sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            _sfxPlayers[i].playOnAwake = false;
            _sfxPlayers[i].volume = _sfxVolume;
        }
    }

    public void ChangeBGM(int stage)
    {
        if (_bgmPlayer.isPlaying)
            _bgmPlayer.Stop();

        _bgmPlayer.clip = _bgmClips[(int)stage];
        _bgmPlayer.Play();
    }

    public void StopBGM()
    {
        if (_bgmPlayer.isPlaying)
            _bgmPlayer.Stop();
    }

    public void PlaySFX(SFX sfx)
    {
        AudioClip sfxClip = ResourceManager.Instance.GetAudioClip(sfx);

        for (int i = 0; i < _sfxChannels; i++)
        {
            int loopIndex = (i + _sfxIndex) % _sfxChannels;

            if (_sfxPlayers[loopIndex].isPlaying)
                continue;

            _sfxPlayers[loopIndex].clip = sfxClip;
            _sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void ChangeVolume(bool isBGM, float value)
    {
        if (isBGM)
        {
            _bgmPlayer.volume = value;
        }
        else
        {
            for (int i = 0; i < _sfxChannels; i++)
            {
                _sfxPlayers[i].volume = value;
            }
        }
    }
}