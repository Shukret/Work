using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int startTime = 20;
    int time;
    public bool canWin;

    [SerializeField] private Slider slider;
    [SerializeField] private GameObject fireTrack;
    [SerializeField] private Transform[] spawnPlace;

    void Minus()
    {
        if (time > 0)
        {
            time -= 1;
            if (time <= 0)
            {
                canWin = true;
                Spawn();
            }
        }
    }

    // Start s called before the first frame update
    void Start()
    {
        //StartCoroutine(TimelineWait()); 
        InvokeRepeating("Minus",1,1);
        slider.maxValue = startTime;
        time = startTime;
    }
    IEnumerator TimelineWait()
    {
        yield return new WaitForSeconds(6.5f);
        InvokeRepeating("Minus",1,1);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = time;
    }

    void Spawn()
    {
        fireTrack.transform.position = spawnPlace[Random.Range(0,spawnPlace.Length)].position;
    }
}
