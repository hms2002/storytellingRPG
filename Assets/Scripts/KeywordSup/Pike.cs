using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pike : KeywordSup
{
    private void Awake()
    {
        keywordName = "°¡½Ã µ¸Àº";
        SetKeywordColor(BLUE);
        keywordDamage = 3;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.pike += keywordDamage;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
