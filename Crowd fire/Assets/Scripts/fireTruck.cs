using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class fireTruck : MonoBehaviour
{
    [SerializeField] private CharacterController ch;
    [SerializeField] private Animator anim;
    //[SerializeField] private Transform[] fireMans;
    [SerializeField] private Transform player;
    [SerializeField] private Collider col;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private PlayerNew playerScript;
    public bool up;
    public bool win;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            win = true;
            Destroy(col);
            rb.isKinematic = true;
            StartCoroutine(Up());
        }
        else if(other.CompareTag("Fire"))
        {
            Destroy(other.gameObject);
        }
    }
    void Update()
        {
            if (win==true)
            {
                for (int i = 0; i < playerScript.crowd.Length; i++)
                {
                    if (playerScript.crowd[i]==null)
                    {
                        i++;
                    }
                    else
                    {
                        if (playerScript.crowd[i].GetComponent<Stickman>().inCrowd != null)
                        {
                            if(playerScript.crowd[i].GetComponent<Stickman>().inCrowd == true)
                            {
                                playerScript.crowd[i].transform.parent = gameObject.transform;
                                playerScript.crowd[i].transform.position = Vector3.MoveTowards (playerScript.crowd[i].transform.position, transform.position, Time.deltaTime * 7);
                            }
                        }
                        Destroy(playerScript.crowd[i].GetComponent<NavMeshAgent>());
                        //Destroy(playerScript.crowd[i].GetComponent<Stickman>());
                        Destroy(playerScript.crowd[i].GetComponent<CapsuleCollider>());
                        playerScript.crowd[i].GetComponent<Rigidbody>().isKinematic = true;
                    }
                    if (i==playerScript.crowd.Length-1)
                    {
                        break;
                    }
                }
                //for (int i = 0; i < fireMans.Length; i++)
                //{
                //    fireMans[i].position = Vector3.MoveTowards (fireMans[i].position, transform.position, Time.deltaTime * 7);
                //}
                player.position = Vector3.MoveTowards (player.position, transform.position, Time.deltaTime * 7);
                Destroy(ch);
                player.transform.parent = gameObject.transform;
                if (up)
                {
                    gameObject.transform.Translate(Vector3.up*5*Time.deltaTime);
                    gameObject.transform.Rotate(Vector3.up*30*Time.deltaTime);
                }
            }
        }

        IEnumerator Up()
        {
            anim.SetTrigger("up");
            yield return new WaitForSeconds(2);
            up = true;
            yield return new WaitForSeconds(3);
            winPanel.SetActive(true);
            Time.timeScale = 0;
        }
}
