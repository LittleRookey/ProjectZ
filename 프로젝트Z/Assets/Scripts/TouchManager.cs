using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private static TouchManager instance;

    public static TouchManager Instance
    {
        get {
            return instance;
        }
    }

    Camera mMaincamera;
    [SerializeField]
    private PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        mMaincamera = Camera.main;
        
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
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

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Click");
                    // effect instantiate
                    
                    player.Attack(GameController.Instance.currentEnemy[0]);
                    
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
