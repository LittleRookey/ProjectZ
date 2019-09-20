using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LocateHealthBar(Enemy enemy)
    {
        Image enemHealthBar = Instantiate(healthBar);
        enemHealthBar.gameObject.transform.SetParent(canvas.transform);
        enemHealthBar.transform.position = enemy.transform.position + Vector3.up * 3;
        enemHealthBar.transform.localScale = Vector3.one;

        // set canvas as parent TODO
    }

    public void ResetHealthBar()
    {
        healthBar.fillAmount *= 100;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
