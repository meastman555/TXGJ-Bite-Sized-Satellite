using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndParallax : MonoBehaviour
{
    //check if parallax is on
    public bool parallaxEnabled = false;
    //true if you want to be able to change parallaxEnabled
    public bool parallaxToggle = false;

    public GameObject parallax;

    private void Start()
    {
        if (parallax.gameObject.activeInHierarchy)
        {
            disableParallax();
            parallaxToggle = true;
            Debug.Log("disabled");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!parallaxEnabled && parallaxToggle == true)
        {
            enableParallax();
            parallaxToggle = false;
            Debug.Log("enabled");

        }
    }


    private void enableParallax()
    {
        parallax.gameObject.SetActive(true);

    }

    private void disableParallax()
    {
        parallax.gameObject.SetActive(false);
    }
}
