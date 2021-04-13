using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_test : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineArrow line;
    void Start()
    {
        line.SetUpLine(points);
    }

}
