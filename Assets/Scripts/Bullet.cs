using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Player _player;
    private LightManager lightManager;
    public EventReference bulletSound;
    private EventInstance bulletSoundInstance;

    private void OnEnable()
    {
        bulletSoundInstance = RuntimeManager.CreateInstance(bulletSound);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(bulletSoundInstance, this.gameObject, this.GetComponent<Rigidbody>());
        bulletSoundInstance.start();
        bulletSoundInstance.release();
    }
    private void Start()
    {
        _player = Dependencies.Instance.GetDependancy<Player>();
        lightManager = Dependencies.Instance.GetDependancy<LightManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GreenEnemy"))
        {
            Destroy(other.gameObject);
            lightManager.RefillNeon("Green", 0.005f);
            Destroy(gameObject);
        }

        if (other.CompareTag("RedEnemy"))
        {
            Destroy(other.gameObject);
            lightManager.RefillNeon("Red", 0.005f);
            Destroy(gameObject);
        }

        if (other.CompareTag("NonEnemy"))
        {
            Destroy(other.gameObject);
            _player.LoseLife();
            Destroy(gameObject);
        }

        else { Destroy(gameObject); }
    }

    private void OnDestroy()
    {
        bulletSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
