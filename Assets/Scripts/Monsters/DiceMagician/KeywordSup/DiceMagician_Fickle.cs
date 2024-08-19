using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMagician_Fickle : KeywordSup
{
    private void Awake()
    {
        keywordName = "변덕스러운";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        int randomFlag = Random.Range(0, 2);
        if (randomFlag == 0)
            caster.damage = keywordDamage;
        else
            caster.Damaged(caster, keywordDamage);

        caster.tension += keywordTension;
    }
}
