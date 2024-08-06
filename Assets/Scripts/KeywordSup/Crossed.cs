using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossed : KeywordSup
{
    private void Awake()
    {
        keywordName = "교차된";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        Debug.Log("shiba");
        caster.damage += caster.protect;
    }

    public override void Check(KeywordMain _keywordMain)
    {
        if(_keywordMain.keywordProtect > 0)
        {

        }
    }
}
