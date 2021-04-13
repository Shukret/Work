using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed;
    public fireTruck helScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (helScript.up)
            gameObject.transform.Rotate(Vector3.forward*speed*Time.deltaTime);
    }
}
