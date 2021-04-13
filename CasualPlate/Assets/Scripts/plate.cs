using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate : MonoBehaviour
{
    //сломана ли тарелка
    [SerializeField] private bool broke;
    public Level levelScript;
    //сломанный префаб
    [SerializeField] private GameObject brokenPlate;
    [SerializeField] private MeshFilter meshF;
    [SerializeField] private Collider colDel;
    //аниматор деда
    [SerializeField] private Animator anim;
    bool ded;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("ded").GetComponent<Animator>();
        broke = false;
        levelScript.platesOst -= 1;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shelf") || other.CompareTag("table"))
        {
            StartCoroutine(zone());
            if (other.CompareTag("table"))
            {
                ded = true;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "shelf" && other.gameObject.tag != "table")
        {
            brokenPlate.SetActive(true);
            meshF.mesh = null;
            StartCoroutine(colD());
            broke = true;
            if (other.gameObject.tag == "forDed")
            {
                anim.SetTrigger("bad");
            }
        }
    }
    IEnumerator colD()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(colDel);
    }
    IEnumerator zone()
    {
        yield return new WaitForSeconds(0.5f);
        if (!broke)
        {
            levelScript.platesOnShelf += 1;
            if (ded)
            {
                ded = false;
                anim.SetTrigger("good");
            }
        }
        else
        {
            if (ded)
            {
                ded = false;
                anim.SetTrigger("bad");
            }
        }
    }
}
