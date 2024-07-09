using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordSup : MonoBehaviour
{
    protected Color RED = new Color(225, 0, 0);
    protected Color BLUE = new Color(0, 255, 0);
    protected Color GREEN = new Color(0, 0, 255);
    protected Color YELLOW = new Color(255, 255, 0);
    private Color keywordColor;
    public abstract void Execute(Actor caster, Actor target, Sentence sentence);
    public abstract void Check(KeywordMain _keywordMain);

    protected Color GetKeywordColor() { return keywordColor; }
    protected void SetKeywordColor(Color color) { keywordColor = color; }
}
