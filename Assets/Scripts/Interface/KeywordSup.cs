using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordSup : Keyword
{
    public void OnClickButton()
    {
        fightManager.GetKeywordSup(this);
    }

    private void Start()
    {
        fightManager = FightManager.fightManager;
    }

    public virtual void CanUseCheck(Actor caster, Actor target) { isCanUse = true; }
    public abstract void Execute(Actor caster, Actor target);
    public abstract void Check(KeywordMain _keywordMain);
}
