using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public int Lifes = 3;
    public float GreenNeonAmount;
    public float RedNeonAmount;

    [Header("Ui Elements")]
    public Image GreenNeonBar;
    public Image RedNeonBar;
    public GameObject DeathScreen;
    public List<GameObject> Hearts;

    public void Start()
    {
        Time.timeScale = 1;
        Dependencies.Instance.RegisterDependency<Player>(this);
    }

    public void LoseLife()
    {
        Lifes--;

        if(Hearts.Count > 0)
        {
           Destroy(Hearts[^1]);
        }

        if( Lifes == 0)
        {
            Time.timeScale = 0;
            DeathScreen.SetActive(true);
        }
    }

    public void DrainNeon(string NeonColor, float amount)
    {
        switch (NeonColor)
        {
            case "Green":
                GreenNeonAmount -= amount;
                if (GreenNeonAmount < 0) GreenNeonAmount = 0;
                break;
            case "Red":
                RedNeonAmount -= amount;
                if (RedNeonAmount < 0) RedNeonAmount = 0;
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
                if (GreenNeonAmount >1) GreenNeonAmount = 1;
                break;
            case "Red":
                RedNeonAmount += amount;
                if (RedNeonAmount >1) RedNeonAmount = 1;
                break;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        GreenNeonBar.fillAmount = GreenNeonAmount;
        RedNeonBar.fillAmount = RedNeonAmount;
    }
}
