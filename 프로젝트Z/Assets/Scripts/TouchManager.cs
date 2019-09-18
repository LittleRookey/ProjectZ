using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    Camera mMaincamera;
    // Start is called before the first frame update
    void Start()
    {
        mMaincamera = Camera.main;
    }
    
    private bool CheckTouch()
    {
        
        
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = mMaincamera.nearClipPlane;
        Vector3 nearPos = mMaincamera.ScreenToWorldPoint(screenPos);
        nearPos.z = mMaincamera.nearClipPlane;
        Vector3 farPos = mMaincamera.ScreenToWorldPoint(screenPos);
        farPos.z = mMaincamera.nearClipPlane;

        Ray ray = new Ray(nearPos, farPos - nearPos);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject == gameObject)
            {
                return true;
            } 
        }
        return false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(fase && CheckTouch())
        {

        } else if(OnMouseButtonDown(0))
        {
            Vector3 screenPos = Input.mousePosition;
            screenPos.z = mMaincamera.nearClipPlane;
            Vector3 nearPos = mMaincamera.ScreenToWorldPoint(screenPos);
            nearPos.z = mMaincamera.nearClipPlane;
            Vector3 farPos = mMaincamera.ScreenToWorldPoint(screenPos);
            farPos.z = mMaincamera.nearClipPlane;

            Ray ray = new Ray(nearPos, farPos - nearPos);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // effect instantiate

                }
            }
        }
    }
}
