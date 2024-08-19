using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMagician_Chaotic : KeywordSup
{
    private void Awake()
    {
        keywordName = "혼돈의";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        int randomFlag = Random.Range(0, 2);
        if (randomFlag == 0)
            caster.protect += keywordProtect;
        else
            target.protect += keywordProtect;

        caster.tension += keywordTension;
    }
}
