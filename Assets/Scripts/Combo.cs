using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : KeywordSup
{
    public override void Execute(KeywordMain mainKeyword, Actor caster, Actor target)
    {
        mainKeyword.UpRepeatCount(1);
    }
}
