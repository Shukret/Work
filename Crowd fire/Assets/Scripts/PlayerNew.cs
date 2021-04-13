using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNew : MonoBehaviour
{
    [SerializeField] private Text pointsGameText;
    public WinPanel win;
    public GameObject[] crowd;

    public bool move;
    public float speedMove;

    private float gravityForce;
    Vector3  moveVector;

    private CharacterController ch_controller;
    private Animator ch_animator;

    public MobileController mc;

    // Start is called before the first frame update
    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pointsGameText.text = (win.stickmanPoints+1).ToString();
        CharacterMove();
        GamingGravity();
    }

    void CharacterMove()
    {
        moveVector = Vector3.zero;
        moveVector.x = mc.Horizontal() * speedMove;
        moveVector.z = mc.Vertical() * speedMove;

        if (moveVector.x != 0 || moveVector.z != 0)
        {
            ch_animator.SetBool("run", true);
            move = true;
        }
        else
        {
            move = false;
            ch_animator.SetBool("run", false);
        }

        if (Vector3.Angle(Vector3.forward, moveVector) > 1 || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0);
            transform.rotation = Quaternion.LookRotation(direct);
        }
        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime);
    }

    void GamingGravity()
    {
        if (!ch_controller.isGrounded) gravityForce -= 20 * Time.deltaTime;
        else gravityForce = -1f;
    }
}
