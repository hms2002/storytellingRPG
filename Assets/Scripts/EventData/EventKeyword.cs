using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventKeyword : MonoBehaviour
{
    [Header("이벤트에서 추가될 키워드")]
    [SerializeField]
    private Keyword addKeyword;
    [Header("키워드 정보를 확인할 수 있는 이벤트인지")]
    [SerializeField]
    private bool isKeywordAddEvent = false;
    [SerializeField] public TextMeshProUGUI nameText;
    private Button button;
    private bool selectedKeyword = false;
    [SerializeField]
    [Header("전투이벤트일 때 몬스터셋 설정")]
    private List<GameObject> monsterSet;
    #region 키워드 제원
    [Header("키워드 색")]
    protected Color R = Color.red;
    protected Color G = Color.green;
    protected Color B = Color.blue;
    protected Color Y = new Color(217f / 255f, 187f / 255f, 54f / 255f, 255f / 255f);
    /// <summary> 검은색임 </summary>
    protected Color D = Color.black;
    protected Color Default = new Color(127f / 255f, 98f / 255f, 71f / 255f, 255f / 255f);
    [Header("이벤트 선택지 이름")]
    [SerializeField] private string _keywordName;
    protected Color keywordColor;
    public enum KeywordColorOption
    {
        Default,
        Red,
        Green,
        Blue,
        Yellow,
        Black
    }
    [SerializeField]
    private KeywordColorOption selectedColor = KeywordColorOption.Default;

    [SerializeField]
    private EventManager.Select select = EventManager.Select.first;

    public string keywordName
    {
        get { return _keywordName; }
        set { _keywordName = value; }
    }
    #endregion

    public void ChangeTension(int tension)
    {
        TensionManager.tensionManagerUI.tension += tension;
    }

    public void ChangeGold(int gold)
    {
        EventManager.instance.player.gold += gold;
    }

    public void AddSupKeyword(GameObject keyword)
    {
        EventManager.instance.player.AddSupKeywordToOriginalDeck(keyword);
    }

    public void AddMainKeyword(GameObject keyword)
    {
        EventManager.instance.player.AddMainKeywordToOriginalDeck(keyword);
    }

    public void AddRelic(Relic relic)
    {

    }

    public void ChangeHp(int hp)
    {
        EventManager.instance.player.hp += hp;
    }
    
    public void Battle()
    {
        MonsterSetDatabase.monsterSetDatabase.SetSelectedSet(monsterSet);
        EventManager.instance.isBattle = true;
    }

    protected void Start()
    {
        if(addKeyword != null)
        {
            addKeyword.Start();
        }
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);
        nameText = FindInfoText("Text (TMP)");
        nameText.text = keywordName;
        switch (selectedColor)
        {
            case KeywordColorOption.Red:
                keywordColor = R;
                break;
            case KeywordColorOption.Green:
                keywordColor = G;
                break;
            case KeywordColorOption.Blue:
                keywordColor = B;
                break;
            case KeywordColorOption.Yellow:
                keywordColor = Y;
                break;
            case KeywordColorOption.Black:
                keywordColor = D;
                break;
            default:
                keywordColor = Default;
                break;
        }

        nameText.color = keywordColor;
    }

    public void Select()
    {
        PlayClickSound();
        WhatSelected();
        EventManager.instance.NextEvent(EventManager.instance.eventData);
        EventManager.instance.isSelected = true;
        KeywordUIMovement.instance.MoveSelectedEventKeyword(this);
    }

    public void PlayClickSound()
    {
        AudioManager.instance.PlaySound("Keyword", "키워드_잡기");
    }
    
    public void WhatSelected()
    {
        EventManager.instance.select = select;
    }

    public void ShowInfoUI()
    {
        if (isKeywordAddEvent)
        {
            string title = addKeyword.keywordName;
            string content = addKeyword.FormatDescription(addKeyword.keywordDescription);

            InfoManager.instance.ShowTipUI(title, addKeyword.GetKeywordColor(), content, transform);
        }
    }

    private TextMeshProUGUI FindInfoText(string name)
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts)
        {
            if (text.gameObject.name == name)
            {
                return text;
            }
        }
        return null;
    }
}
