using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Keyword : MonoBehaviour
{
    protected FightManager fightManager;
    [SerializeField] public TextMeshProUGUI nameText;
    public string name;
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
    [SerializeField] private string _debuffType = "";
    [SerializeField] private int    _debuffStack = 0;
    [SerializeField] private int    _buffStack = 0;
    [SerializeField] private int    _keywordTension = 0;
    [SerializeField] private bool   _isOneTimeUse = false;
    protected Color keywordColor;

    [Header("키워드 특성별 색")]
    protected Color R = Color.red;
    protected Color G = Color.green;
    protected Color B = Color.blue;
    protected Color Y = Color.yellow;
    /// <summary>
    /// 검은색임
    /// </summary>
    protected Color D = Color.black;

    [Multiline(3)]
    [SerializeField] protected string keywordDescription = "";

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

    public bool isPlayerKeyword { get => _isPlayerKeyword; set => _isPlayerKeyword = value; }

    #endregion

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
        if(descriptionText)
        {
            descriptionText.text = FormatDescription(keywordDescription);
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
        AudioManager.instance.PlaySound("Keyword","키워드_잡기");
    }
    public Color GetKeywordColor() { return keywordColor; }
    public void SetKeywordColor(Color color) { keywordColor = color; }
    private string FormatDescription(string template)
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
            InfoManager.instance.ShowTipUI(title, GetKeywordColor(), "긴장도 " + keywordTension.ToString(), content, transform);
        }
        else
        {
            InfoManager.instance.ShowTipUI(title, GetKeywordColor(), content, transform);
        }
    }
}
