using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balshroom_SporeDiffusion : KeywordMain
{
    private void Awake()
    {
        keywordName = "포자 확산";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        // 버섯 생성
        Balshroom balshroom = (Balshroom)caster;
        balshroom.CreateRedMushroom(target.transform);
        // 버섯 생성
        balshroom.CreateBlueMushroom(target.transform);

        target.charactorState.AddState(StateType.redSpore, debuffStack);
        target.charactorState.AddState(StateType.blueSpore, debuffStack);

        caster.tension += keywordTension;
    }
}
