using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //public TextMeshProUGUI title, start, close;
    public float fadeOutLength = 1f;
    
    public void StartPressed()
    {
        //Color white = new Color(0, 0, 0, 0);
        //title.DOColor(white, fadeOutLength);
        //start.DOColor(white, fadeOutLength);
        //close.DOColor(white, fadeOutLength);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClosePressed()
    {
        Application.Quit();
    }

}
