using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    [SerializeField]
    public Actor player; 
    public enum Select
    {
        first,
        second,
        third
    }
    private List<GameObject> eventHand = new List<GameObject>();      // Support 키워드 실체 타입

    [SerializeField]
    private GameObject eventImage;
    public Select select;
    public bool isSelected = false;
    public bool isBattle = false;
    private Vector2 createLocation = new Vector2(0, -930);
    public EventData eventData;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        select = Select.first;
        isSelected = false;
        eventData = null;
        isBattle = false;
    }

    public void ShowEvent(EventData eventData)
    {
        eventImage.GetComponent<SpriteRenderer>().sprite = eventData.roomImage;
        player.gameObject.SetActive(true);
        this.eventData = eventData;
        StartCoroutine(PlayTextByLine(eventData.roomContents, 1.5f, 2f, eventData));
    }

    private IEnumerator PlayTextByLine(string fullText, float time, float nextTime, EventData eventData)
    {
        // 텍스트를 줄바꿈(\n)으로 분리
        string[] lines = fullText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        TextManager.instance.Text.alignment = TextAlignmentOptions.Midline;

        foreach (string line in lines)
        {
            // 현재 줄을 타이핑 효과로 출력
            yield return TextManager.instance.Text.DOText(line, time).WaitForCompletion();
            // 다음 줄을 출력하기 전에 대기 시간 설정
            yield return new WaitForSeconds(nextTime);
        }

        if (!isSelected)
        {
            TextManager.instance.Text.alignment = TextAlignmentOptions.Top;
            for (int i = 0; i < 3; i++)
            {
                eventHand.Add(Instantiate(eventData.eventKeyword[i], createLocation, Quaternion.identity, CanvasData.canvasData.handCanvas.transform.Find("SelectedKeywordPivot")));
            }
            KeywordUIMovement.instance.StretchKeywords(eventHand);
        }
        else
        {
            if(isBattle)
            {
                eventImage.SetActive(false);
                FightManager.fightManager.FightStart();
            }
            else
            {
                Book.instance.EnterMap();
                player.gameObject.SetActive(false);
                GameManager.instance.eventIndex += 1;
                Init();
            }
        }
    }

    public void NextEvent(EventData eventData)
    {
        switch(select)
        {
            case Select.first:
                StartCoroutine(PlayTextByLine(eventData.roomContentsAfter, 1.5f, 2f, eventData));
                if(eventData.nextImage1 != null)
                {
                    eventImage.GetComponent<SpriteRenderer>().sprite = eventData.nextImage1;
                }
                break;
            case Select.second:
                StartCoroutine(PlayTextByLine(eventData.roomContentsAfter2, 1.5f, 2f, eventData));
                if (eventData.nextImage2 != null)
                {
                    eventImage.GetComponent<SpriteRenderer>().sprite = eventData.nextImage2;
                }
                break;
            case Select.third:
                StartCoroutine(PlayTextByLine(eventData.roomContentsAfter3, 1.5f, 2f, eventData));
                if (eventData.nextImage3 != null)
                {
                    eventImage.GetComponent<SpriteRenderer>().sprite = eventData.nextImage3;
                }
                break;
            default:
                Debug.Log("말도안됨");
                break;
        }
    }
}
