using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evil : KeywordSup
{
    public override void Execute(KeywordMain mainKeyword, Actor caster, Actor target)
    {
        Sentence sentence;
        sentence.weaken += 1;
        target.weaken(1);
    }
}
