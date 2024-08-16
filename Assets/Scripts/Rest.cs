using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Actor player;
    [Header("효과 가이드")]
    [SerializeField] private GameObject InfoTextImage;

    //안정된 휴식
    public void StableHealing()
    {
        player.hp += 30;
        ClickOff();
    }

    //효율적인 휴식
    public void EfficientHealing()
    {
        player.tension += 50;

        ClickOff();
    }

    //조화로운 휴식
    public void HarmoniousHealing()
    {
        player.hp += 15;
        player.tension += 25;

        ClickOff();
    }

    public void ClickOff()
    {
        UIManager.instance.ActiveRestUI(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoTextImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoTextImage.SetActive(false);
    }
}
