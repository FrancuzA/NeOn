using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using DG.Tweening;

public class EndScreenShaker : MonoBehaviour
{
    [Header("Volume References")]
    public Volume globalVolume;
    private LensDistortion lensDist;

    [Header("Blue Layer (Background)")]
    public RectTransform blueLayer;
    public float blueDuration = 10f;
    public float blueMaxOffset = 50f;
    public Vector3 blueTargetScale = new Vector3(1.2f, 1.2f, 1f);
    public float blueScaleDuration = 4f;
    public Ease blueEase = Ease.InOutSine;

    [Header("Purple Layer (Midground)")]
    public RectTransform purpleLayer;
    public float purpleDuration = 2f;
    public float purpleMaxOffset = 30f;
    public float purpleRotationAngle = 30f;
    public Vector3 purpleTargetScale = new Vector3(1.5f, 1.5f, 1f);
    public float purpleScaleDuration = 1.5f;
    public Ease purpleEase = Ease.InQuart; // Snappy nonlinear ease
    public float purpleDelay = 0.2f;

    [Header("Global Pucker (Lens)")]
    public float targetLensIntensity = -0.6f;
    public float targetLensScale = 1.2f;
    public Ease puckerEase = Ease.InOutBack; // Nonlinear "wind-up" feel

    void Start()
    {
        // --- 1. GLOBAL VOLUME (NONLINEAR) ---
        if (globalVolume != null && globalVolume.profile.TryGet<LensDistortion>(out lensDist))
        {
            DOTween.To(() => lensDist.intensity.value, x => lensDist.intensity.value = x, targetLensIntensity, purpleScaleDuration)
                .SetEase(puckerEase)
                .SetLoops(-1, LoopType.Yoyo);

            DOTween.To(() => lensDist.scale.value, x => lensDist.scale.value = x, targetLensScale, purpleScaleDuration)
                .SetEase(puckerEase)
                .SetLoops(-1, LoopType.Yoyo);
        }

        // --- 2. BLUE LAYER (SMOOTH) ---
        if (blueLayer != null)
        {
            blueLayer.DOShakePosition(blueDuration, blueMaxOffset, 1, 90f, false, false).SetLoops(-1);
            blueLayer.DOScale(blueTargetScale, blueScaleDuration)
                .SetEase(blueEase)
                .SetLoops(-1, LoopType.Yoyo);
        }

        // --- 3. PURPLE LAYER (AGGRESSIVE/NONLINEAR) ---
        if (purpleLayer != null)
        {
            purpleLayer.DOShakePosition(purpleDuration, purpleMaxOffset, 10, 90f, false, false).SetLoops(-1);

            // Rotation Snap
            purpleLayer.DORotate(new Vector3(0, 0, purpleRotationAngle), 0.25f).SetEase(Ease.OutBack).OnComplete(() => {
                purpleLayer.DORotate(new Vector3(0, 0, -purpleRotationAngle), 0.5f).SetEase(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo);
            });

            // Scale Pulse with nonlinear ease and delay
            purpleLayer.DOScale(purpleTargetScale, purpleScaleDuration)
                .SetEase(purpleEase)
                .SetDelay(purpleDelay)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    void OnDestroy()
    {
        if (lensDist != null)
        {
            lensDist.intensity.value = 0f;
            lensDist.scale.value = 1f;
        }
        DOTween.KillAll();
    }
}