using UnityEngine;

public class LightManager : MonoBehaviour
{

    [SerializeField] private Light NormalLight;
    [SerializeField] private Light GreenNeon;
    [SerializeField] private Light RedNeon;
    [SerializeField] private float GreenCost;
    [SerializeField] private float RedCost;
    private Player _player;
    public enum LightType
    {
        Normal,
        GreenNeon,
        RedNeon
    }

    public LightType CurrentLight;

    private void Start()
    {
        Dependencies.Instance.RegisterDependency<LightManager>(this);
        _player = Dependencies.Instance.GetDependancy<Player>();
        CurrentLight = LightType.Normal;
    }

    public void SwapLight(LightType lightType)
    {
        switch (lightType)
        {
            case LightType.Normal:
                CurrentLight = LightType.Normal;
                NormalLight.enabled = true;
                GreenNeon.enabled = false;
                RedNeon.enabled = false;
                break;
            case LightType.GreenNeon:
                CurrentLight = LightType.GreenNeon;
                GreenNeon.enabled = true;
                RedNeon.enabled = false;
                NormalLight.enabled = false;
                break;
            case LightType.RedNeon:
                CurrentLight = LightType.RedNeon;
                RedNeon.enabled = true;
                NormalLight.enabled = false;
                GreenNeon.enabled = false;
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapLight(LightType.Normal);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _player.GreenNeonAmount >0)
        {
            SwapLight(LightType.GreenNeon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && _player.RedNeonAmount >0)
        {
            SwapLight(LightType.RedNeon);
        }
    }

    private void FixedUpdate()
    {
        switch (CurrentLight)
        {
            case LightType.GreenNeon:
                if (_player.GreenNeonAmount <= 0) SwapLight(LightType.Normal);
                _player.DrainNeon("Green", GreenCost);
                break;
            case LightType.RedNeon:
                if (_player.RedNeonAmount <= 0) SwapLight(LightType.Normal);
                _player.DrainNeon("Red", RedCost);
                break;
        }
    }
}
