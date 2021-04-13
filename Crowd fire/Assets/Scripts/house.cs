using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject[] stickmans;

    void Start()
    {
        Time.timeScale = 1;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("house");
        }
    }
}
