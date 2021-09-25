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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LetterUpdate());
    }

    private IEnumerator LetterUpdate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            yield return wait;
            positionUpdate();
        }
    }

    public void positionUpdate()
    {
        target = new Vector2(followedObject.transform.position.x + followDistance, followedObject.transform.position.y);
        this.transform.DOMove(target, followSpeed);

    }
}
