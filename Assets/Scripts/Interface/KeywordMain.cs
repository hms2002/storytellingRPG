using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordMain : MonoBehaviour
{
    protected FightManager fightManager;

    #region 키워드 능력치,이름,색 관련 변수
    private string _keywordName = "";
    private int _keywordDamage = 0;
    private int _keywordProtect = 0;
    private string _debuffType = "";
    private int _debuffStack = 0;

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

    private Color keywordColor;
    [Header("키워드 특성별 색")]
    [SerializeField] protected Color RED = new Color(255, 0, 0);
    [SerializeField] protected Color BLUE = new Color(0, 255, 0);
    [SerializeField] protected Color GREEN = new Color(0, 0, 255);
    [SerializeField] protected Color YELLOW = new Color(255, 255, 0);
    #endregion

    [Multiline(3)]
    [SerializeField] private string keywordDescription = "";

    public void OnClickButton()
    {
        fightManager.GetKeywordMain(this);
    }
    private void Start()
    {
        fightManager = FightManager.fightManager;
    }
    

    public abstract void Execute(Actor caster, Actor target, Sentence sentence);
    public abstract void Check(KeywordSup _keywordSup);

    public Color GetKeywordColor() { return keywordColor; }
    public void SetKeywordColor(Color color) { keywordColor = color; }

    public int GetDebuffStack() { return debuffStack; }

    public void SetDebuffStack(int stack) { debuffStack = stack; }

}
