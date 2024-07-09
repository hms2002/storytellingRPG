using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defensive : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(BLUE);
    }
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        Debug.Log("방어적인 발동");

        sentence.ProtectControl(4);
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
