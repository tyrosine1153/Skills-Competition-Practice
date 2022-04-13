using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private BgmClip bgmClip;
    private void Start()
    {
        AudioManager.Instance.PlayBgm(bgmClip);
    }
}