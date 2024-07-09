using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordMain : MonoBehaviour
{
    protected string keywordName;
    protected int keywordDamage;
    protected int keywordProtect;

    private Color keywordColor;
    protected Color RED = new Color(225, 0, 0);
    protected Color BLUE = new Color(0, 255, 0);
    protected Color GREEN = new Color(0, 0, 255);
    protected Color YELLOW = new Color(255, 255, 0);

    public abstract void Execute(Actor caster, Actor target, Sentence sentence);
    public abstract void Check(KeywordSup _keywordSup);

    public Color GetKeywordColor() { return keywordColor; }
    public void SetKeywordColor(Color color) { keywordColor = color; }
}
