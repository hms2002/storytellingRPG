using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Rest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Actor player;
    [Header("효과 가이드")]
    [SerializeField] private GameObject InfoTextImage;

    private void Awake()
    {
        RestManager.btnList.Add(GetComponent<Button>());
    }

    //안정된 휴식
    public void StableHealing()
    {
        if(player == null)
            player = EventManager.instance.player;
        player.hp += 30;
        ClickOff();
    }

    //효율적인 휴식
    public void EfficientHealing()
    {
        if (player == null)
            player = EventManager.instance.player;
        TensionManager.tensionManagerUI.tension += 50;

        ClickOff();
    }

    //조화로운 휴식
    public void HarmoniousHealing()
    {
        if (player == null)
            player = EventManager.instance.player;
        player.hp += 15;
        TensionManager.tensionManagerUI.tension += 25;

        ClickOff();
    }

    public void ClickOff()
    {
        EventManager.instance.isSelected = true;
        EventManager.instance.NextEvent(EventDatabase.eventDatas.rest);
        KeywordUIMovement.instance.MoveSelectedEventKeyword(gameObject);
        RestManager.allBtnOff();
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
