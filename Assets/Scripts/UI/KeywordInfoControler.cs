using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeywordInfoControler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject info;
    private void Awake()
    {
        info = transform.GetChild(0).gameObject;
        info.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        info.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        info.SetActive(false);
    }
}
