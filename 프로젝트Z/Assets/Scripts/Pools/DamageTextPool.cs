using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextPool : ObjectPool<Text>
{

    [SerializeField]
    private Canvas canvas;

    protected override Text AddFunctionality(int id)
    {
        Text temp = Instantiate(mOrigin[id], canvas.transform);

        mPool[id].Add(temp);

        return temp;
    }

    //private override Text SetTransform(int id)
    //{
    //    Text temp = Instantiate(mOrigin[id], canvas.transform);
    //    //temp.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position + Vector3.up * 2;
        
    //    //temp.transform.parent = canvas.gameObject.transform;
    //    //temp.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position;

    //    //Image enemHealthBar = Instantiate(healthBarEnemy, canvas.transform);
    //    //enemHealthBar.transform.position = enemy[i].transform.position + Vector3.up * 1f;
    //    //enemHealthBar.transform.localScale = Vector3.one;

    //    mPool[id].Add(temp);
    //    return temp;
    //}
}
