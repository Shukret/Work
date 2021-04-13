using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stickman : MonoBehaviour
{
    public bool inCrowd;
    bool death;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject Text3D;
    [SerializeField] private WinPanel winScript;
    NavMeshAgent navMeshAgent;

    //прикосновение
    [SerializeField] private Material AnotherMaterial;
    [SerializeField] private Material WhiteMaterial;
    [SerializeField] private Renderer rend;
    [SerializeField] private Animator anim;
    bool firstCol;
    
    //движение
    [SerializeField] private PlayerNew playerScript;
    [SerializeField] private Transform target;
    public float speed;
    bool canMove;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNew>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !firstCol)
        {
            for (int i=0; i< playerScript.crowd.Length; i++)
            {
                if (playerScript.crowd[i] == null)
                {
                    playerScript.crowd[i] = gameObject;
                    break;
                }
            }
            inCrowd = true;
            Destroy(Text3D);
            winScript.stickmanPoints += 1;
            Debug.Log(winScript.stickmanPoints);
            firstCol = true;
            //смена материала
            var mats = rend.sharedMaterials;
            mats [0] = AnotherMaterial;
            rend.sharedMaterials = mats;

            //смена анимации
            anim.SetBool("run", true);

            //преследование
            canMove = true;
        }

        if (other.CompareTag("Fire"))
        {
            if (other.GetComponent<FireStickman>().deathNumber <= 1 && !death)
            {
                death = true;
                fire.SetActive(true);
                anim.SetTrigger("death");
                var mats = rend.sharedMaterials;
                mats [0] = WhiteMaterial;
                rend.sharedMaterials = mats;
                if(inCrowd)
                {
                    winScript.stickmanPoints -= 1;
                    inCrowd = false;
                }
                Debug.Log(winScript.stickmanPoints);
                canMove = false;
            }
        }

        if (other.CompareTag("lose") && !death)
        {
            death = true;
            fire.SetActive(true);
            anim.SetTrigger("death");
            var mats = rend.sharedMaterials;
            mats [0] = WhiteMaterial;
            rend.sharedMaterials = mats;
            if(inCrowd)
            {
                inCrowd = false;
                winScript.stickmanPoints -= 1;
            }
            Debug.Log(winScript.stickmanPoints);
            canMove = false;
        }
    }

    void Update()
    {
        if (canMove)
        {
            navMeshAgent.SetDestination(target.position);
            if (playerScript.move)
            {
                navMeshAgent.speed = playerScript.speedMove;
                anim.SetBool("run", true);
            }
            else
            {
                navMeshAgent.speed = 0;
                anim.SetBool("run", false);
            }
        }
    }
}
