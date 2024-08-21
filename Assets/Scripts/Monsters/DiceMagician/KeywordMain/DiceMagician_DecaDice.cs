using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMagician_DecaDice : KeywordMain
{
    private void Awake()
    {
        keywordName = "십면체 주사위";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        int randomFlag = Random.Range(0, 2);
        if (randomFlag == 0)
            caster.dmgList.Add(Random.Range(1, 11));
        else
            target.hp += Random.Range(1, 11);

        caster.tension += keywordTension;
    }
}
