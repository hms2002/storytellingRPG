using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyword : MonoBehaviour
{
    protected FightManager fightManager;

    #region 키워드 제원 변수
    [Header("키워드 제원")]
    [SerializeField] private string _keywordName;
    [SerializeField] private int _keywordDamage = 0;
    [SerializeField] private int _keywordProtect = 0;
    [SerializeField] private string _debuffType = "";
    [SerializeField] private int _debuffStack = 0;
    [SerializeField] private int _keywordTension = 0;

    protected Color keywordColor;

    [Header("키워드 특성별 색")]
    [SerializeField] protected Color R = new Color32(255, 0, 0, 1);
    [SerializeField] protected Color G = new Color32(0, 255, 0, 1);
    [SerializeField] protected Color B = new Color32(0, 0, 255, 1);
    [SerializeField] protected Color Y = new Color32(255, 255, 0, 1);
    /// <summary>
    /// 검은색임
    /// </summary>
    [SerializeField] protected Color D = new Color32(0, 0, 0, 1);



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

    private void Start()
    {
        fightManager = FightManager.fightManager;
    }

    public void PlayClickSound()
    {
        AudioManager.instance.PlaySound("Keyword","키워드_잡기");
    }
    public Color GetKeywordColor() { return keywordColor; }
    public void SetKeywordColor(Color color) { keywordColor = color; }
}
