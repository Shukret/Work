using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCamp : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Inst());
    }

    IEnumerator Inst()
    {
        yield return new WaitForSeconds(2f);
        fire.SetActive(true);
    }
}
