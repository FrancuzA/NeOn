using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening; // Pamiêtaj o zaimportowaniu DOTween!

public class GlitchSequence : MonoBehaviour
{
    [Header("Ustawienia Komponentów")]
    public Image glitchImage; // Przeci¹gnij tutaj swój Image z Canvasa
    public Sprite[] glitchSprites; // Wstaw tutaj w inspektorze sprinty w kolejnoœci: [0]=2, [1]=3, [2]=4

    [Header("Ustawienia Animacji")]
    public float delayBeforeStart = 2f;
    public float totalDuration = 1f;

    void Start()
    {
        // Ukrywamy glitch na starcie (opcjonalne)
        glitchImage.gameObject.SetActive(false);

        // Rozpoczynamy sekwencjê po 2 sekundach
        Invoke("StartGlitchEffect", delayBeforeStart);
    }

    void StartGlitchEffect()
    {
        glitchImage.gameObject.SetActive(true);

        // 1. EFEKT WSTRZ¥SU (SCREEN SHAKE)
        // Wykorzystujemy DOShakePosition na RectTransform obrazka
        // Parametry: czas, si³a, czêstotliwoœæ
        glitchImage.rectTransform.DOShakePosition(totalDuration, 50f, 30);

        // 2. SEKWENCJA ZMIANY SPRITE'ÓW
        // Twoja kolejnoœæ: 2->3->4->3->2->3->4->3->2->3->4->3->2 (13 kroków)
        int[] sequence = { 0, 1, 2, 1, 0, 1, 2, 1, 0, 1, 2, 1, 0 };
        StartCoroutine(PlaySpriteSequence(sequence));
    }

    IEnumerator PlaySpriteSequence(int[] sequence)
    {
        float timeStep = totalDuration / sequence.Length;

        foreach (int index in sequence)
        {
            if (index < glitchSprites.Length)
            {
                glitchImage.sprite = glitchSprites[index];
            }
            yield return new WaitForSeconds(timeStep);
        }

        // Po zakoñczeniu wy³¹czamy glitch (lub ustawiamy przezroczystoœæ na 0)
        glitchImage.gameObject.SetActive(false);
    }
}