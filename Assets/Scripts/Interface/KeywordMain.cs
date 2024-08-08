using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordMain : Keyword
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

                fightManager.GetKeywordMain(this);

                break;

            case ButtonType.Purchase:

                ShopManager.instance.PurchaseKeyword(this.gameObject, this);

                break;
        }
    }

    public virtual void CanUseCheck(Actor caster, Actor target) { isCanUse = true; }
    public abstract void Execute(Actor caster, Actor target);
    public abstract void Check(KeywordSup _keywordSup);
}
