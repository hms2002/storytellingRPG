using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : KeywordSup
{
    public override void Execute(KeywordMain mainKeyword, Actor caster, Actor target)
    {
        Sentence sectence;
        sentence.vulnerable += 1;
        target.Vulnerable(1);
    }
}
