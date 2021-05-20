using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private LevelTimer timerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerScript.boss)
        {
            anim.SetTrigger("attack");
        }
    }
}
