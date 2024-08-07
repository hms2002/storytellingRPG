using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordSup : Keyword
{
    private void Start()
    {
        fightManager = FightManager.fightManager;
    }

    public void OnClickButton()
    {
        switch (buttonType)
        {
            case ButtonType.Use:

                fightManager.GetKeywordSup(this);

                break;

            case ButtonType.Purchase:
                break;
        }
    }

    public virtual void CanUseCheck(Actor caster, Actor target) { isCanUse = true ; }
    public abstract void Execute(Actor caster, Actor target);
    public abstract void Check(KeywordMain _keywordMain);
}
