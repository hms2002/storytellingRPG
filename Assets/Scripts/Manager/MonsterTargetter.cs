using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTargetter : MonoBehaviour
{
    public static MonsterTargetter monsterTargetter;
    public Actor _target;
    public Transform targetUIObj;
    public Actor target
    {
        get { return _target; }
        set { _target = value;
            targetUIObj.position = _target.transform.position;
        }
    }
    const int MaxDistance = 10;
    LayerMask layerMask;

    private void Awake()
    {
        if (monsterTargetter == null)
            monsterTargetter = this;
        layerMask = LayerMask.NameToLayer("Monster");
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 15);
            Debug.DrawRay(mousePos, transform.forward * 10, Color.red, 0.3f);
            if(hit)
            {
                Debug.Log("목표 : " + hit.transform.gameObject.name);
                target = hit.transform.GetComponent<Actor>();
            }
        }

    }
}
