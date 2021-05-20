using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    [Header("Boss")]
    [SerializeField] private LevelTimer timerScript;
    [SerializeField] private Animation anim;
    bool play;
    // Start is called before the first frame update
    void Start()
    {
        offset = player.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!timerScript.boss)
            transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - offset.z);
        if(timerScript.boss && !play)
        {
            play = true;
            anim.Play("CameraAnimation");
        }
        
    }
}
