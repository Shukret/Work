using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localFire : MonoBehaviour
{
    [SerializeField] private Transform bigFire;
    // Update is called once per frame
    void Update()
    {
        if (bigFire.localScale.x <= 1)
        {
            transform.localScale = new Vector3(6,6,6);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}
