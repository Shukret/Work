using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFire : MonoBehaviour
{
    [SerializeField] private Timer timer;
    int i;
    // Update is called once per frame
    void Start()
    {
        i = 0;
        StartCoroutine(Scale());
        //StartCoroutine(TimelineWait());
    }

    IEnumerator Scale()
    {
        while(i < 28)
        {
            if(!timer.canWin)
                yield return new WaitForSeconds(1.8f);
            else
                yield return new WaitForSeconds(1f);
            Vector3 sc = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = new Vector3(sc.x-0.16f, sc.y-0.16f, sc.z-0.16f);
            i++;
        }
    }

    IEnumerator TimelineWait()
    {
        yield return new WaitForSeconds(6.5f);
        StartCoroutine(Scale());
    }
}
