using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveLetter : MonoBehaviour
{
    //variables exposed to editor
    [SerializeField]
    private GameObject letter;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //gives the player the letter specified to the script
    //simply activates the child gameobject of the player
    public void Give()
    {
        letter.SetActive(true);
        //
    }
}
