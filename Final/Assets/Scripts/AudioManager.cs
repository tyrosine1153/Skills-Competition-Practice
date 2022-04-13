using UnityEngine;

public enum BgmClip
{
    Title,
    Stage1,
    Stage1Boss,
    Stage2,
    Stage2Boss,
    End,
}

public enum SfxClip
{
    PlayerAttack,
    EnemyAttack,
    BossAttack,
    SpawnItem,
    UseItem,
    ButtonClick,
    PlayerDie,
    GameStart,
    BonusScoreCalc,
    BonusScoreGet,
    BodyDie,
    BossDie,
}

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource _bgmAudioSource;
    private AudioSource _sfxAudioSource;
    
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] sfxClips;

    protected override void Awake()
    {
        base.Awake();
        _bgmAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        _sfxAudioSource = transform.GetChild(1).GetComponent<AudioSource>();
    }
    
    public void PlayBgm(BgmClip bgmClip)
    {
        if(_bgmAudioSource.isPlaying)
            _bgmAudioSource.Stop();
        
        _bgmAudioSource.clip = bgmClips[(int) bgmClip];
        _bgmAudioSource.Play();
    }

    public void StopBgm()
    {
        _bgmAudioSource.Stop();
    }

    public void PlaySfx(SfxClip sfxClip)
    {
        Debug.Log(sfxClip);
        _sfxAudioSource.PlayOneShot(sfxClips[(int) sfxClip]);
    }

    public void StopSfx()
    {
        _sfxAudioSource.Stop();
    }
}
