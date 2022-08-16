using System.Collections.Generic;
using UnityEngine;

public class VideoManager : Singleton<VideoManager>
{
    public List<Resolution> Resolutions;
    public int currentResolutionIndex;
    public bool currentFullscreen;

    private const string ResolutionIndexKey = "RESOLUTION_INDEX";
    private const string FullscreenKey = "FULLSCREEN";

    protected override void Awake()
    {
        base.Awake();

        Resolutions = new List<Resolution>();
        var resolution = new Resolution
        {
            width = 960,
            height = 540,
            refreshRate = 60
        };
        Resolutions.Add(resolution);
        resolution = new Resolution
        {
            width = 1280,
            height = 720,
            refreshRate = 60
        };
        Resolutions.Add(resolution);
        resolution = new Resolution
        {
            width = 1920,
            height = 1080,
            refreshRate = 60
        };
        Resolutions.Add(resolution);

        // var resolution = Screen.resolutions;

        currentResolutionIndex = PlayerPrefs.GetInt(ResolutionIndexKey, currentResolutionIndex);
        currentFullscreen = PlayerPrefs.GetInt(FullscreenKey, currentFullscreen ? 1 : 0) == 1;
        SetResolution(currentResolutionIndex, currentFullscreen);
    }

    public void SetResolution(int index, bool fullscreen)
    {
        index = Mathf.Clamp(index, 0, Resolutions.Count - 1);
        currentResolutionIndex = index;
        currentFullscreen = fullscreen;
        PlayerPrefs.SetInt(ResolutionIndexKey, currentResolutionIndex);
        PlayerPrefs.SetInt(FullscreenKey, currentFullscreen ? 1 : 0);
        Screen.SetResolution(Resolutions[currentResolutionIndex].width, Resolutions[currentResolutionIndex].height,
            currentFullscreen);
    }
}