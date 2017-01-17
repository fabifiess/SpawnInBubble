using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DragManager : NetworkBehaviour
{
    public GameObject curDragObj;
    private Rigidbody curDragRbd;

    private Vector3 lastPos;
    private Vector3 curPos;
    private Vector3 newVel;
    private float velMulti = 1000f;

    private float dragSpeed = 10f;
    public string tagEffect = "effect";
    public string tagItem = "item";
    public string tagColor = "color";


    void Update ()
    {
        if (Input.GetMouseButtonUp(0) && curDragObj != null)
        {
            DeactivateOldDragObject();
        }

    }

    void FixedUpdate()
    {
        FollowMouse();
        if(curDragRbd != null)
        {
            SetVelocity();
        }
    }

    void FollowMouse()
    {
        if (Input.GetMouseButton(0) && curDragObj != null)
        {
            Vector2 mPosScreen = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mPosScreen);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 newPos = new Vector3(hit.point.x, curDragObj.transform.position.y, hit.point.z);
                curDragObj.transform.position = Vector3.Lerp(curDragObj.transform.position, newPos, Time.deltaTime * dragSpeed);
            }
        }
    }

    void SetVelocity()
    {
        curPos = curDragObj.transform.position;
        newVel = (curPos - lastPos) * Time.deltaTime * velMulti;
        lastPos = curPos;
        curDragRbd.velocity = newVel;
    }

    RaycastHit GetScreenHit()
    {
        Vector2 mPosScreen = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mPosScreen);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }

    public void ActivateDragObject(GameObject obj)
    {
        DeactivateOldDragObject();
        curDragObj = obj;
        curDragObj.layer = 2;

        if (curDragObj.tag == tagItem)
        {
            curDragRbd = curDragObj.GetComponent<Rigidbody>();
            lastPos = curDragObj.transform.position;
        }
    }

    public void DeactivateOldDragObject()
    {
        if(curDragObj != null)
        {
            if (curDragObj.tag == tagEffect)
            {
                SetEffectParent();
            }
            curDragObj.layer = 0;
            curDragObj = null;
        }
        curDragRbd = null;
    }

    void SetEffectParent()
    {
        GameObject newParentGameObject = GetScreenHit().collider.gameObject;
        if (newParentGameObject.tag == tagItem)
        {
            Transform newParent = GetScreenHit().collider.gameObject.transform;
            curDragObj.transform.SetParent(newParent);
            curDragObj.transform.localPosition = Vector3.zero;
        }
        else
        {
            Destroy(curDragObj);
        }
    }
}
