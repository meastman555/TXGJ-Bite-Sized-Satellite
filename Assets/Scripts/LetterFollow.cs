using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LetterFollow : MonoBehaviour
{
    public GameObject followedObject;
    public float followDistance;
    public float followSpeed;
    private Vector2 target;
    private float targetX;
    private float targetY;

    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.tag == "Letter")
        {
            StartCoroutine(LetterUpdate());
        }
    }
    private void Update()
    {
        if(this.gameObject.tag == "MainCamera")
        {
            cameraUpdate();
        }
    }

    private IEnumerator LetterUpdate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        while (true)
        {
            yield return wait;
            positionUpdate();
        }
    }

    public void positionUpdate()
    {
        target = new Vector2(followedObject.transform.position.x - followDistance, followedObject.transform.position.y);
        this.transform.DOMove(target, followSpeed);

    }

    public void cameraUpdate()
    {
        targetX = followedObject.transform.position.x;
        targetY = followedObject.transform.position.y;

        this.transform.DOMoveX(targetX, followSpeed);
        this.transform.DOMoveY(targetY, followSpeed);
    }
}
