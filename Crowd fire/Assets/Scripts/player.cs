using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class player : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform pad;

    [SerializeField] private Transform playerG;
    [SerializeField] private Animator anim;
    public Vector3 moveForward;
    Vector3 moveRotate;
    public float moveSpeed;
    [SerializeField] private float rotateSpeed;

    public bool move;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)pad.position, pad.rect.width * 0.5f);

        moveForward = new Vector3(0,0,transform.localPosition.y).normalized;
        moveRotate = new Vector3(0,transform.localPosition.x, 0).normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        moveForward = Vector3.zero;
        moveRotate = Vector3.zero;
        anim.SetBool("run", false);
        move = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetBool("run", true);
        move = true;
    }


    void Update()
    {
        if (move)
        {
            playerG.Translate(moveForward*moveSpeed*Time.deltaTime);

            if (Mathf.Abs(transform.localPosition.x) > pad.rect.width * 0.3f)
            {
                playerG.Rotate(moveRotate*rotateSpeed*Time.deltaTime);
            }
        }
    }
}
