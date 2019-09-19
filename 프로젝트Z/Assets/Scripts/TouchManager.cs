using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    Camera mMaincamera;
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        mMaincamera = Camera.main;
        
    }
    
    private bool CheckTouch()
    {
        int touchCount = Input.touchCount;

        for(int i = 0; i < touchCount; i++)
        {
            Touch currentTouch = Input.GetTouch(i);
            if(currentTouch.phase == TouchPhase.Began)
            {

                Vector3 screenPos = Input.mousePosition;
                screenPos.z = mMaincamera.nearClipPlane;
                Vector3 nearPos = mMaincamera.ScreenToWorldPoint(screenPos);
                screenPos.z = mMaincamera.farClipPlane;
                Vector3 farPos = mMaincamera.ScreenToWorldPoint(screenPos);


                Ray ray = new Ray(nearPos, farPos - nearPos);
                Debug.Log("farpos:" + farPos);
                Debug.Log("nearpos:" + nearPos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        // effect instantiate
                        return true;
                    }
                }
                
            }

        }
        return false;

       
    }

    //private void OnMouseDown()
    //{
    //    enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    //    player.Attack(enemy);
    //    Debug.Log("Player attack enemy");
    //    // show damage with UI

    //}

    // Update is called once per frame
    void Update()
    {
        //if(!enemy.isDead())
        //{
               
        //}
        //mouse
        if (CheckTouch())
        {
            Debug.Log("Touch!");
        }
#if UNITY_EDITOR
        else if (Input.GetMouseButtonDown(0))
        {

            Vector3 screenPos = Input.mousePosition;
            screenPos.z = mMaincamera.nearClipPlane;
            Vector3 nearPos = mMaincamera.ScreenToWorldPoint(screenPos);
            screenPos.z = mMaincamera.farClipPlane;
            Vector3 farPos = mMaincamera.ScreenToWorldPoint(screenPos);


            Ray ray = new Ray(nearPos, farPos - nearPos);
            Debug.Log("farpos:" + farPos);
            Debug.Log("nearpos:" + nearPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // effect instantiate
                    player.Attack(enemy);
                    Debug.Log("Attacked player");
                }
            }
        }
#endif
    }
    //Vector3 screenPos = Input.mousePosition;
    //screenPos.z = mMaincamera.nearClipPlane;
    //        Vector3 nearPos = mMaincamera.ScreenToWorldPoint(screenPos);
    //screenPos.z = mMaincamera.farClipPlane;
    //        Vector3 farPos = mMaincamera.ScreenToWorldPoint(screenPos);


    //Ray ray = new Ray(nearPos, farPos - nearPos);
    //Debug.Log("farpos:" + farPos);
    //        Debug.Log("nearpos:" + nearPos);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.collider.gameObject == gameObject)
    //            {
    //                // effect instantiate
    //            }
            //}
}
