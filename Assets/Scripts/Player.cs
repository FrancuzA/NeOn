using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public int Lifes = 3;


    [Header("Ui Elements")]

    public GameObject DeathScreen;
    public List<GameObject> Hearts;

    public void Awake()
    {
        Time.timeScale = 1;
        Dependencies.Instance.RegisterDependency<Player>(this);
    }

    public void LoseLife()
    {
        Lifes--;

        if(Hearts.Count > 0)
        {
           Hearts.RemoveAt(Lifes);
        }

        if( Lifes == 0)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            DeathScreen.SetActive(true);
        }
    }

    
}
