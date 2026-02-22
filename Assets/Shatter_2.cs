using UnityEngine;
using DG.Tweening;

public class Shatter_2 : MonoBehaviour
{
    [Header("Scale Settings")]
    public Vector3 targetScale = new Vector3(1.3f, 1.3f, 1f);
    public float duration = 1.2f;
    public float initialDelay = 0f; // Useful if you put this on 2 layers

    [Header("Feel")]
    public Ease pulseEase = Ease.InOutQuart;

    void Start()
    {
        // One-liner to handle the pulse with a delay
        transform.DOScale(targetScale, duration)
            .SetEase(pulseEase)
            .SetLoops(-1, LoopType.Yoyo)
            .SetDelay(initialDelay);
    }

    private void OnDestroy()
    {
        // Clean up to prevent ghost animations
        transform.DOKill();
    }
}