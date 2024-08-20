using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Firm : KeywordSup
{
    private void Awake()
    {
        isPlayerKeyword = true;
        keywordName = "굳건한";
        SetKeywordColor(B);
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        int add = 0;
        if (caster.protect >= 7)
            add = 3;
        caster.protect += keywordProtect + add;
    }

    public override void Check(KeywordMain _keywordMain) { }
}
