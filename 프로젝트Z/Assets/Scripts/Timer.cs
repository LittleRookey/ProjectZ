using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float endTime;

    private IEnumerator TimeCount()
    {
        yield return new WaitForSeconds(endTime);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(TimeCount());
    }
}
