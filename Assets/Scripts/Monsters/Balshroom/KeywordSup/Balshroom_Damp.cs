using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balshroom_Damp : KeywordSup
{
    private void Awake()
    {
        keywordName = "습기 찬";

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
        balshroom.CreateBlueMushroom(target.transform);

        target.charactorState.AddState(StateType.blueSpore, debuffStack);
        caster.tension += keywordTension;
    }
}
