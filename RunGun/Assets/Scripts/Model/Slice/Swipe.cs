using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour, IBeginDragHandler, IDragHandler , IEndDragHandler
{
    [SerializeField] private Transform _movementObject;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private TrailRenderer trailRenderer;

    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }

    private void RotateObject(int angle)
    {

        _movementObject.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (AngleIsValid(eventData.delta, 0, 45))
        {
            RotateObject(90);
        }
        if (AngleIsValid(eventData.delta, 45, 45))
        {
            RotateObject(45);
        }
        if (AngleIsValid(eventData.delta, 90, 45))
        {
            RotateObject(0);
        }
        if (AngleIsValid(eventData.delta, 135, 45))
        {
            RotateObject(-45);
        }
        if (AngleIsValid(eventData.delta, 180, 45))
        {
            RotateObject(90);
        }
        if (AngleIsValid(eventData.delta, 225, 45))
        {
            RotateObject(45);
        }
        if (AngleIsValid(eventData.delta, 270, 45))
        {
            RotateObject(0);
        }
        if (AngleIsValid(eventData.delta, 315, 45))
        {
            RotateObject(-45);
        }
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit, 35))
        {
            _movementObject.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            trailRenderer.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
        }
    }
    
    protected bool AngleIsValid(Vector2 vector , int RequiredAngle , int RequiredArc)
    {
        if (RequiredArc >= 0.0f)
        {
            var angle = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
            var angleDelta = Mathf.DeltaAngle(angle, RequiredAngle);

            if (angleDelta < RequiredArc * -0.5f || angleDelta >= RequiredArc * 0.5f)
            {
                return false;
            }
        }
        return true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _movementObject.position = Vector3.zero;
    }
}
