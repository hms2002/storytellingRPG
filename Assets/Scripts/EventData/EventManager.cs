using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

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
    [SerializeField]
    private Sprite treasureCloseImg;
    [SerializeField]
    private Sprite treasureOpenImg;
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
            EventDatabase.eventDatas.ShuffleList();
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
        eventHand.Clear();
    }

    public void ShowEvent(EventData eventData)
    {
        eventImage.GetComponent<SpriteRenderer>().sprite = eventData.roomImage;
        player.gameObject.SetActive(true);
        this.eventData = eventData;
        StartCoroutine(PlayTextByLine(eventData.roomContents, 1f, 1.2f, eventData));
    }
    public void ShowTreasure()
    {
        TextManager.instance.Text.text = string.Empty;
        eventImage.GetComponent<SpriteRenderer>().sprite = treasureCloseImg;
        player.gameObject.SetActive(true);
        string script =
            "당신은 여정 중에서 보물을 발견하였습니다.\n상자 안에는......";
        
        StartCoroutine(PlayTreasureTextByLine(script, 1f, 1.2f, eventData));
    }

    private IEnumerator PlayTextByLine(string fullText, float time, float nextTime, EventData eventData)
    {
        TextManager.instance.Text.text = string.Empty;
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
            for (int i = 0; i < eventData.eventKeyword.Count; i++)
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
                Init();
            }
            else
            {
                GameManager.instance.gameState = GameState.EndBattle;
                Book.instance.EnterMap();
                player.gameObject.SetActive(false);
                Init();
            }
        }
    }

    private IEnumerator PlayTreasureTextByLine(string fullText, float time, float nextTime, EventData eventData)
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

        eventImage.GetComponent<SpriteRenderer>().sprite = treasureOpenImg;

        if (!isSelected)
        {
            // 보상 요청
            RewardManager.instance.MakeTreasures();
        }
    }

    public void NextEvent(EventData eventData)
    {
        switch(select)
        {
            case Select.first:
                StartCoroutine(PlayTextByLine(eventData.roomContentsAfter, 1f, 1.2f, eventData));
                if(eventData.nextImage1 != null)
                {
                    eventImage.GetComponent<SpriteRenderer>().sprite = eventData.nextImage1;
                }
                break;
            case Select.second:
                StartCoroutine(PlayTextByLine(eventData.roomContentsAfter2, 1f, 1.2f, eventData));
                if (eventData.nextImage2 != null)
                {
                    eventImage.GetComponent<SpriteRenderer>().sprite = eventData.nextImage2;
                }
                break;
            case Select.third:
                StartCoroutine(PlayTextByLine(eventData.roomContentsAfter3, 1f, 1.2f, eventData));
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
