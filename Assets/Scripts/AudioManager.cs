using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public EventReference musicRef;
    private EventInstance musicInstance;


    private void Start()
    {
        musicInstance = RuntimeManager.CreateInstance(musicRef);
        musicInstance.start();
        musicInstance.release();
    }

    private void OnDestroy()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
