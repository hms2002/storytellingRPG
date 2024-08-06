using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_Predator : KeywordMain
{
    // Start is called before the first frame update
    void Awake()
    {
        keywordName = "포식자";
        SetKeywordColor(R);
        keywordTension = 10;

        if(FightManager.fightManager.getPlayerDamaged() > 0)
        {
            isCanUse = false;
            keywordDamage = FightManager.fightManager.getPlayerDamaged();
        }
        else
        {
            isCanUse = true;
            keywordDamage = 0;
        }
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.tension += keywordTension;
        target.damage += keywordDamage;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
