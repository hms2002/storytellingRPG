using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordSup : Keyword
{
    public void OnClickButton()
    {
        fightManager.GetKeywordSup(this);
        PlayClickSound();
    }

    private void Start()
    {
        fightManager = FightManager.fightManager;
    }

    public abstract void Execute(Actor caster, Actor target);
    public abstract void Check(KeywordMain _keywordMain);
}
