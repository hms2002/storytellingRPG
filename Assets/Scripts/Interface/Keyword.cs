using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Keyword : MonoBehaviour
{
    public enum ButtonType
    {
        Use,
        Display,
        Purchase,
        Erase
    }

    protected ButtonType _buttonType = ButtonType.Use;          // 키워드 버튼의 클릭 타입
    public ButtonType buttonType { get => _buttonType; set => _buttonType = value; }

    protected FightManager fightManager;
    [SerializeField] public TextMeshProUGUI nameText;
    [HideInInspector]public string name;
    [SerializeField] public TextMeshProUGUI descriptionText;

    public enum EffectTarget
    {
        caster,
        target
    }

    public EffectTarget effectTarget;
    public EffectManager.EffectType effectType;

    #region 키워드 제원 변수
    [Header("키워드 제원")]
    [SerializeField] private string _keywordName;
    [SerializeField] private int    _keywordDamage = 0;
    [SerializeField] private int    _keywordProtect = 0;
    [SerializeField] private int    _keywordHeal = 0;

    [Space(10.0f)]
    [SerializeField] private string _debuffType = "";
    [SerializeField] private int    _debuffStack = 0;
    [SerializeField] private int    _buffStack = 0;

    [Space(10.0f)]
    [SerializeField] private int    _keywordTension = 0;

    [Space(10.0f)]
    [SerializeField] private bool   _isOneTimeUse = false;

    [Space(10.0f)]
    [Header("불규칙적인 연타키워드일 때")]
    [SerializeField] private bool _isIrregularCombo = false;

    protected Color keywordColor;
    [Header("키워드 특성별 색")]
    protected Color R = Color.red;
    protected Color G = Color.green;
    protected Color B = Color.blue;
    protected Color Y = new Color(217 / 255f, 173f / 255f, 50f / 255f, 255f / 255f);
    /// <summary> 검은색임 </summary>
    protected Color D = Color.black;
    public enum KeywordColorOption
    {
        Red,
        Green,
        Blue,
        Yellow,
        Black
    }
    [Header("키워드의 색상")]
    [SerializeField]
    private KeywordColorOption selectedColor;
    [Multiline(3)]
    [SerializeField] public string keywordDescription = "";
    [Header ("긴장도가 변경되는 키워드일 때 긴장도툴팁 텍스트 변경")]
    [SerializeField] private string changeTensionText = "";
    [Header("긴장도가 변경되는 키워드일 때 체크하면됨")]
    [SerializeField] private bool _isChangeTension = false;
    private bool _isCanUse = true;
    private bool _isPlayerKeyword = false;

    public string keywordName
    {
        get { return _keywordName; }
        set { _keywordName = value; }
    }

    public int keywordDamage
    {
        get { return _keywordDamage; }
        set { _keywordDamage = value; }
    }

    public int keywordProtect
    {
        get { return _keywordProtect; }
        set { _keywordProtect = value; }
    }

    public int keywordHeal
    {
        get { return _keywordHeal; }
        set { _keywordHeal = value; }
    }

    public string debuffType
    {
        get { return _debuffType; }
        set { _debuffType = value; }
    }

    public int debuffStack
    {
        get { return _debuffStack; }
        set { _debuffStack = value; }
    }

    public int buffStack
    {
        get { return _buffStack; }
        set { _buffStack = value; }
    }

    public int keywordTension
    {
        get { return _keywordTension; }
        set { _keywordTension = value; }
    }

    public bool isCanUse
    {
        get { return _isCanUse; }
        set { _isCanUse = value; }
    }

    public bool isOneTimeUse
    {
        get { return _isOneTimeUse; }
        set { _isOneTimeUse = value; }
    }
    public bool isChangeTension
    {
        get { return _isChangeTension; }
        set { _isChangeTension = value; }
    }

    public bool isPlayerKeyword { get => _isPlayerKeyword; set => _isPlayerKeyword = value; }
    public bool isIrregularCombo { get => _isIrregularCombo; set => _isIrregularCombo = value; }

    #endregion
    public void Start()
    {
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
                keywordColor = D;
                break;
        }
    }
    protected void Init()
    {
        descriptionText = transform.Find("Info").GetComponentInChildren<TextMeshProUGUI>();
        nameText = FindInfoText("Text (TMP)");
        fightManager = FightManager.fightManager;
        nameText.text = keywordName;
        name = new string(nameText.text);
        nameText.color = keywordColor;
        if(keywordColor == R)
        {
            effectTarget = EffectTarget.target;
            effectType = EffectManager.EffectType.Attack;
        }
        if(keywordColor == B)
        {
            effectTarget = EffectTarget.caster;
            effectType = EffectManager.EffectType.Shield;
        }
        if(keywordColor == Y)
        {
            effectType = EffectManager.EffectType.None;
        }
        if(descriptionText)
        {
            descriptionText.text = FormatDescription(keywordDescription);
            descriptionText.transform.parent.gameObject.SetActive(false);
        }
    }

    public void CantUseEffect()
    {
        GetComponent<Button>().enabled = false;
        DOVirtual.DelayedCall(0.3f, () => GetComponent<Button>().enabled = true);
        transform.DOPunchPosition(new Vector3(10, 0, 0), 0.3f, 10, 1);
    }

    public void PlayClickSound()
    {
        if(buttonType == ButtonType.Purchase)
        {
            AudioManager.instance.PlaySound("Shop", "상점키워드");
        }
        else
        {
            AudioManager.instance.PlaySound("Keyword", "키워드_잡기");
        }
    }

    public Color GetKeywordColor() { return keywordColor; }
    public void SetKeywordColor(Color color) { keywordColor = color; }
    public string FormatDescription(string template)
    {
        return template.Replace("debuffstack", debuffStack.ToString())
                       .Replace("buffstack", buffStack.ToString())
                       .Replace("tension", keywordTension.ToString())
                       .Replace("damage", keywordDamage.ToString())
                       .Replace("protect", keywordProtect.ToString())
                       .Replace("heal", keywordHeal.ToString());
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

    public void ShowInfoUI()
    {
        string title = keywordName;
        string content = FormatDescription(keywordDescription);

        // InfoManager를 통해 InfoUI를 표시
        if (!isPlayerKeyword)
        {
            if(isChangeTension)
            {
                if(keywordTension > 0)
                {
                    InfoManager.instance.ShowTipUI(title, GetKeywordColor(), changeTensionText + " +" + keywordTension.ToString(), content, transform);
                }
                else
                {
                    InfoManager.instance.ShowTipUI(title, GetKeywordColor(), changeTensionText + " " + keywordTension.ToString(), content, transform);
                }
            }
            else
            {
                if(keywordTension > 0)
                {
                    InfoManager.instance.ShowTipUI(title, GetKeywordColor(), "긴장도 +" + keywordTension.ToString(), content, transform);
                }
                else
                {
                    InfoManager.instance.ShowTipUI(title, GetKeywordColor(), "긴장도 " + keywordTension.ToString(), content, transform);
                }
            }
        }
        else
        {
            InfoManager.instance.ShowTipUI(title, GetKeywordColor(), content, transform);
        }
    }
}
