using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAtTheStake : KeywordMain
{
    
    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        target.Damaged(caster, target.burnStack , DamageType.Beat);
        sentence.damage += target.burnStack;
    }
    public override void Check(KeywordSup _keywordSup)
    {

    }
}
