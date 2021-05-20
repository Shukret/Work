using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("TouchPlayer")]
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private Transform axe;
    Vector3 axeScale;

    [Header("Rotation")]
    [SerializeField] private float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        axe = GameObject.FindGameObjectWithTag("axe").transform;
        axeScale = axe.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up*rotateSpeed*Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            axe.localScale = new Vector3(axeScale.x+0.9f,axeScale.y+0.9f, axeScale.z+0.9f);
            playerScript.pickUps += 1;
            playerScript.ShowPickUps();
            Destroy(gameObject);
        }
    }
}
