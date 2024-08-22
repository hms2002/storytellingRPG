using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeywordInfoControler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool onDestroying = true;
    private Keyword keyword;
    private EventKeyword eventKeyword;
    private void Awake()
    {
        keyword = GetComponent<Keyword>();
        eventKeyword = GetComponent<EventKeyword>();
    }

    
    private void OnDestroy()
    {
    }

    public void DisableShowInfo()
    {
        onDestroying = true;
        InfoManager.instance.HideTipUI();
    }

    public void ableShowInfo()
    {
        onDestroying = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onDestroying) return;
        if (keyword != null)
        {
            keyword.ShowInfoUI();
        }
        if (eventKeyword != null)
        {
            eventKeyword.ShowInfoUI();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onDestroying) return;
        InfoManager.instance.HideTipUI();
    }
}
