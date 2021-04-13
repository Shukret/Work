using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;  

    void Start () 
    {        
        //StartCoroutine(TimelineWait()); 
        offset = transform.position - player.transform.position;
    }

    void LateUpdate () 
    {        
        transform.position = player.transform.position + offset;
    }
    IEnumerator TimelineWait()
    {
        yield return new WaitForSeconds(6.5f);
        offset = transform.position - player.transform.position;
    }
}
