using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireStickman : MonoBehaviour
{
    public int deathNumber;
    [SerializeField] private Collider cyl;
    [SerializeField] private Collider box;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    NavMeshAgent navMeshAgent;
    Transform target;
    [SerializeField] private PlayerNew playerScript;
    bool tar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Death());
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNew>();
        if (playerScript.crowd[0] != null)
            target = playerScript.crowd[0].transform;
        else
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || deathNumber == 2)
        {
            anim.SetTrigger("death");
            navMeshAgent.speed = 0;
        }
        else
        {
            navMeshAgent.SetDestination(target.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stickman"))
        {
            deathNumber += 1;
            anim.SetTrigger("death");
            navMeshAgent.speed = 0;
            rb.isKinematic = true;
            StartCoroutine(del());
        }
    }

    IEnumerator del()
    {
        yield return new WaitForSeconds(1);
        Destroy(box);
        Destroy(cyl);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(7);
        anim.SetTrigger("death");
        navMeshAgent.speed = 0;
        rb.isKinematic = true;
        StartCoroutine(del());
    }
}
