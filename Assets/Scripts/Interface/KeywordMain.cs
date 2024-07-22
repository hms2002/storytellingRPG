using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordMain : Keyword
{

    public void OnClickButton()
    {
        fightManager.GetKeywordMain(this);
        PlayClickSound();
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
