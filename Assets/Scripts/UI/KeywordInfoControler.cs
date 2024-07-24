using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeywordInfoControler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject info;
    bool onDestroying = false;
    
    
    private void Awake()
    {
        info = transform.GetChild(0).gameObject;
        info.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onDestroying) return;
        info.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onDestroying) return;
        info.SetActive(false);
    }
    private void OnDestroy()
    {
    }
    public void DisableShowInfo()
    {
        info.SetActive(false);
        onDestroying = true;
    }
}
