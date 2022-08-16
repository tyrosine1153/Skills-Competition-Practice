using UnityEngine;

public enum BGMClip
{
    None = -1,
    Test
}

public enum SfxClip
{
    None = -1,
    Test,
    ButtonClick,
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private BGMClip currentBGMClip = BGMClip.None;

    public float BGMVolume
    {
        get => bgmSource.volume;
        set => bgmSource.volume = value;
    }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] sfxClips;

    public float SfxVolume
    {
        get => sfxSource.volume;
        set => sfxSource.volume = value;
    }

    public void PlayBGM(BGMClip clip)
    {
        if (clip == BGMClip.None) return;

        bgmSource.clip = bgmClips[(int)clip];
        bgmSource.Play();

        currentBGMClip = clip;
    }

    public void StopBGM()
    {
        bgmSource.Stop();

        currentBGMClip = BGMClip.None;
    }

    public void PlaySfx(SfxClip clip, bool oneShot = true)
    {
        if (clip == SfxClip.None) return;

        if (oneShot)
        {
            sfxSource.PlayOneShot(sfxClips[(int)clip]);
        }
        else if (!sfxSource.isPlaying)
        {
            sfxSource.clip = sfxClips[(int)clip];
            sfxSource.Play();
        }
    }
}