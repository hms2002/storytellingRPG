using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInBoots_Bouncing : KeywordMain
{
    private void Awake()
    {
        keywordName = "튕겨내기";

        SetKeywordColor(B);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect += keywordProtect;
        caster.charactorState.AddState(StateType.counterAttack, buffStack);
        caster.tension += keywordTension;
    }
}
