using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEndParallax : MonoBehaviour
{
    //check if parallax is on
    public bool parallaxEnabled = false;
    //true if you want to be able to change parallaxEnabled
    public bool parallaxToggle = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!parallaxEnabled && parallaxToggle)
        {
            enableParallax();
            parallaxToggle = false;

        }
    }

    void Update()
    {
        
    }

    private void enableParallax()
    {
        this.gameObject.SetActive(true);

    }
}
