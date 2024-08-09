using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeywordInfoControler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject info;
    bool onDestroying = true;
    private Keyword keyword;

    private void Awake()
    {
        info = transform.GetChild(0).gameObject;
        info.SetActive(false);
        keyword = GetComponent<Keyword>();
    }

/*    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onDestroying) return;
        info.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onDestroying) return;
        info.SetActive(false);
    }*/
    
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onDestroying) return;
        InfoManager.instance.HideTipUI();
    }
}
