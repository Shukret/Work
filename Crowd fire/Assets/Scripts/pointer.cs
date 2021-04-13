using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointer : MonoBehaviour
{
    [SerializeField] private GameObject pointerGFX;
    [SerializeField] private Timer timer;
    [SerializeField] private Transform fireTruck;
    // Start is called before the first frame update
    void Start()
    {
        pointerGFX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.canWin)
            pointerGFX.SetActive(true);
        transform.LookAt(fireTruck);
    }
}
