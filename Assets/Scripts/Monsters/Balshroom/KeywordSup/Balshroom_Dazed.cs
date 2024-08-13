using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balshroom_Dazed : KeywordSup
{
    private void Awake()
    {
        keywordName = "몽롱한";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        // 버섯 생성
        Balshroom balshroom = (Balshroom)caster;
        balshroom.CreateRedMushroom(target.transform);

        target.charactorState.AddState(StateType.redSpore, debuffStack);
        caster.tension += keywordTension;
    }
}

