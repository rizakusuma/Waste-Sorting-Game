using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashbinController : MonoBehaviour
{
    public bool Moveable { get; set; } = true;

    public float borderLeft;
    public float borderRight;

    private Camera cameraV;
    private Collider2D coll;
    private bool dragged = false;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        cameraV = Camera.main;
    }

    private void Update()
    {
        if (Moveable)
        {
            Controller();
        }
    }

    private void Controller()
    {
        if (Application.isMobilePlatform)
        {
            TouchController();
        } else
        {
            MouseController();
        }
    }

    private void MouseController()
    {
        Move(Input.mousePosition, Input.GetMouseButtonDown(0), Input.GetMouseButton(0), Input.GetMouseButtonUp(0));
    }

    private void TouchController()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Move(
                touch.position, 
                touch.phase == TouchPhase.Began, 
                touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary, 
                touch.phase == TouchPhase.Ended);
        }
    }

    private void Move(Vector3 screenPos, bool tapStatus, bool holdStatus, bool leaveStatus)
    {
        if (tapStatus)
        {
            Vector3 pos = cameraV.ScreenToWorldPoint(screenPos);
            if (coll.OverlapPoint(pos))
            {
                dragged = true;
            }
        } else if (holdStatus)
        {
            if (dragged)
            {
                Vector3 pos = transform.position;
                pos.x = Mathf.Clamp(cameraV.ScreenToWorldPoint(screenPos).x, borderLeft, borderRight);
                transform.position = pos;
            }
        } else
        {
            dragged = false;
        }
    }

    public void ResetPosition()
    {
        Vector3 pos = transform.position;
        pos.x = 0;
        transform.position = pos;
    }
}
