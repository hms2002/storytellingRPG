using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossed : KeywordSup
{
    private void Awake()
    {
        keywordName = "±³Â÷µÈ";
        SetKeywordColor(BLUE);
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += caster.protect;
    }

    public override void Check(KeywordMain _keywordMain)
    {
        if(_keywordMain.keywordProtect > 0)
        {

        }
    }
}
