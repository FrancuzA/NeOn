using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using DG.Tweening;

public class LightManager : MonoBehaviour
{

    [SerializeField] private Light NormalLight;
    [SerializeField] private Light GreenNeon;
    [SerializeField] private Light RedNeon;
    [SerializeField] private Light RedNeonBeam;
    [SerializeField] private GameObject normalLamp;
    [SerializeField] private GameObject GreenLamp;
    [SerializeField] private GameObject GreenLampInside;
    [SerializeField] private GameObject RedLamp;
    [SerializeField] private GameObject RedLampInside;
    [SerializeField] private float GreenCost;
    [SerializeField] private float RedCost;

    

    [Header("Bars")]
    public Image GreenNeonBar;
    public Image RedNeonBar;
    public GameObject GreenBarFrameOn;
    public GameObject GreenBarFrameOff;
    public GameObject RedBarFrameOn;
    public GameObject RedBarFrameOff;
    public GameObject FlashlightOn;
    public GameObject FlashlightOff;
    

    public float GreenNeonAmount;
    public float RedNeonAmount;

    private float FullLightInsideHeight = 0;
    [SerializeField] private float EmptyLightInsideHeight;
    private float LampInHandHeight = -2.825948f;
    private float LampHideHeight = -9f;

    public enum LightType
    {
        Normal,
        GreenNeon,
        RedNeon
    }

    public LightType CurrentLight;
    private GameObject currentLamp;

    private void Awake()
    {
        Dependencies.Instance.RegisterDependency<LightManager>(this);

    }
    private void Start()
    {
        CurrentLight = LightType.Normal;
        currentLamp = normalLamp;
        SwapLight(CurrentLight);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HideLamp(GreenLamp);
        HideLamp(RedLamp);
    }
    

    public void SwapLight(LightType lightType)
    {
        ResetBarFrames();
        switch (lightType)
        {
            case LightType.Normal:
                CurrentLight = LightType.Normal;
                NormalLight.enabled = true;
                GreenNeon.enabled = false;
                RedNeon.enabled = false;
                RedNeonBeam.enabled = false;
                FlashlightOn.SetActive(true);
                FlashlightOff.SetActive(false);

                break;
            case LightType.GreenNeon:
                CurrentLight = LightType.GreenNeon;
                GreenNeon.enabled = true;
                RedNeon.enabled = false;
                RedNeonBeam.enabled = false;
                NormalLight.enabled = false;
                GreenBarFrameOn.SetActive(true);
                GreenBarFrameOff.SetActive(false);
                break;
            case LightType.RedNeon:
                CurrentLight = LightType.RedNeon;
                RedNeon.enabled = true;
                RedNeonBeam.enabled = true;
                NormalLight.enabled = false;
                GreenNeon.enabled = false;
                RedBarFrameOn.SetActive(true);
                RedBarFrameOff.SetActive(false);
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapLight(LightType.Normal);

            HideLamp(currentLamp);
            currentLamp = normalLamp;
            ShowLamp(normalLamp);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GreenNeonAmount >0)
        {
            SwapLight(LightType.GreenNeon);
            HideLamp(currentLamp);
            currentLamp = GreenLamp;
            ShowLamp(GreenLamp);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && RedNeonAmount >0)
        {
            SwapLight(LightType.RedNeon);
            HideLamp(currentLamp);
            currentLamp = RedLamp;
            ShowLamp(RedLamp);
        }
    }

    private void FixedUpdate()
    {
        switch (CurrentLight)
        {
            case LightType.GreenNeon:
                if (GreenNeonAmount <= 0) SwapLight(LightType.Normal);
                DrainNeon("Green", GreenCost);
                break;
            case LightType.RedNeon:
                if (RedNeonAmount <= 0) SwapLight(LightType.Normal);
                DrainNeon("Red", RedCost);
                break;
        }
    }

    public void DrainNeon(string NeonColor, float amount)
    {
        switch (NeonColor)
        {
            case "Green":
                GreenNeonAmount -= amount;
                if (GreenNeonAmount < 0) {GreenNeonAmount = 0; }
                SetNeonSubstanceInLamp(GreenNeonAmount, GreenLampInside);
                break;
            case "Red":
                RedNeonAmount -= amount;
                if (RedNeonAmount < 0) RedNeonAmount = 0;
                SetNeonSubstanceInLamp(RedNeonAmount,RedLampInside);
                break;
        }
        UpdateUI();
    }

    public void RefillNeon(string NeonColor, float amount)
    {
        switch (NeonColor)
        {
            case "Green":
                GreenNeonAmount += amount;
                if (GreenNeonAmount > 1) GreenNeonAmount = 1;
                break;
            case "Red":
                RedNeonAmount += amount;
                if (RedNeonAmount > 1) RedNeonAmount = 1;
                break;
        }
        UpdateUI();
    }

    public void SetNeonSubstanceInLamp(float amount, GameObject lampInside)
    {
       float newY = Mathf.Lerp(EmptyLightInsideHeight, FullLightInsideHeight, amount);
       lampInside.transform.localPosition = new Vector3(0, newY, 0);
    }

    public void UpdateUI()
    {
        GreenNeonBar.fillAmount = GreenNeonAmount;
        RedNeonBar.fillAmount = RedNeonAmount;
    }

    public void ResetBarFrames()
    {
        FlashlightOff.SetActive(true);
        FlashlightOn.SetActive(false);
        GreenBarFrameOff.SetActive(true);
        GreenBarFrameOn.SetActive(false);
        RedBarFrameOff.SetActive(true);
        RedBarFrameOn.SetActive(false);
    }
    public void HideLamp(GameObject lamp)
    {
        lamp.transform.DOKill();

        Vector3 pos = lamp.transform.localPosition;
        pos.y = LampHideHeight;
        lamp.transform.localPosition = pos;
        lamp.SetActive(false);
    }


    public void ShowLamp(GameObject lamp)
    {
        lamp.SetActive(true);
        lamp.transform.DOKill();
        lamp.transform.DOLocalMoveY(LampInHandHeight, 0.5f).SetEase(Ease.OutCubic);
    }
}
