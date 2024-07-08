using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : KeywordSup
{
    public override void Execute(KeywordMain mainKeyword, Actor caster, Actor target)
    {
        target.Burn(2);
    }
}
