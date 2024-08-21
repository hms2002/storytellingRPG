using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Strong : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "든든한";
        SetKeywordColor(R);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (caster.protect >= 3)
            caster.dmgList.Plus(keywordDamage);
    }

    public override void Check(KeywordMain _keywordMain) { }
}
