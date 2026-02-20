using UnityEngine;

public class LightManager : MonoBehaviour
{

    [SerializeField] private Light NormalLight;
    [SerializeField] private Light GreenNeon;
    [SerializeField] private Light RedNeon;
    public enum LightType
    {
        Normal,
        GreenNeon,
        RedNeon
    }

    public LightType CurrentLight;

    private void Start()
    {
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
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapLight(LightType.GreenNeon);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapLight(LightType.RedNeon);
        }
    }
}
