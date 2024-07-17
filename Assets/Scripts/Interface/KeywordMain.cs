using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordMain : MonoBehaviour
{
    protected FightManager fightManager;

    #region 메인 키워드 제원 변수
    [Header("메인 키워드 제원")]
    [SerializeField] private string _keywordName = "";
    [SerializeField] private int _keywordDamage = 0;
    [SerializeField] private int _keywordProtect = 0;
    [SerializeField] private string _debuffType = "";
    [SerializeField] private int _debuffStack = 0;
    [SerializeField] private int _keywordTension = 0;

    private Color keywordColor;
    [Header("키워드 특성별 색")]
    [SerializeField] protected Color RED = new Color(255, 0, 0);
    [SerializeField] protected Color BLUE = new Color(0, 255, 0);
    [SerializeField] protected Color GREEN = new Color(0, 0, 255);
    [SerializeField] protected Color YELLOW = new Color(255, 255, 0);

    [Multiline(3)]
    [SerializeField] private string keywordDescription = "";

    private bool _isCanUse = true;


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

    #endregion


    public void OnClickButton()
    {
        fightManager.GetKeywordMain(this);
    }

    private void Start()
    {
        fightManager = FightManager.fightManager;
    }
    
    public abstract void Execute(Actor caster, Actor target);
    public abstract void Check(KeywordSup _keywordSup);

    public Color GetKeywordColor() { return keywordColor; }
    public void SetKeywordColor(Color color) { keywordColor = color; }

}
