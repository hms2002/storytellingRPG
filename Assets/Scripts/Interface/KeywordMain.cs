using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordMain : MonoBehaviour
{
    private string _keywordName = "";
    private int _keywordDamage = 0;
    private int _keywordProtect = 0;
    private string _debuffType = "";
    private int _debuffStack = 0;

    // Properties with get and set accessors
    public string KeywordName
    {
        get { return _keywordName; }
        set { _keywordName = value; }
    }

    public int KeywordDamage
    {
        get { return _keywordDamage; }
        set { _keywordDamage = value; }
    }

    public int KeywordProtect
    {
        get { return _keywordProtect; }
        set { _keywordProtect = value; }
    }

    public string DebuffType
    {
        get { return _debuffType; }
        set { _debuffType = value; }
    }

    public int DebuffStack
    {
        get { return _debuffStack; }
        set { _debuffStack = value; }
    }
    private Color keywordColor;
    protected Color RED = new Color(225, 0, 0);
    protected Color BLUE = new Color(0, 255, 0);
    protected Color GREEN = new Color(0, 0, 255);
    protected Color YELLOW = new Color(255, 255, 0);

    public abstract void Execute(Actor caster, Actor target, Sentence sentence);
    public abstract void Check(KeywordSup _keywordSup);

    public Color GetKeywordColor() { return keywordColor; }
    public void SetKeywordColor(Color color) { keywordColor = color; }

    public int GetDebuffStack() { return DebuffStack; }

    public void SetDebuffStack(int stack) { DebuffStack = stack; }

}
