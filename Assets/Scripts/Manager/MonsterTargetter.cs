using System;
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
        TargetUIOff();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 15);
            if(hit)
            {
                Debug.Log("목표 : " + hit.transform.gameObject.name);
                targetUIObj.gameObject.SetActive(true);
                target = hit.transform.GetComponent<Actor>();
            }
        }

    }

    public void ReAimTarget(List<Monster> monsterList)
    {
        if(monsterList.Count == 0) return;
        targetUIObj.gameObject.SetActive(true);
        target = monsterList[0];
    }

    public void TargetUIOn()
    {
        targetUIObj.gameObject.SetActive(true);
    }
    public void TargetUIOff()
    {
        targetUIObj.gameObject.SetActive(false);
    }
}
