using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balshroom_Delicate : KeywordSup
{
    private void Awake()
    {
        keywordName = "은은한";

        SetKeywordColor(Y);
        Init();
    }
    public override void Check(KeywordMain _keywordMain)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        Balshroom balshroom = (Balshroom)caster;
        int randInt = Random.Range(0, 2);
        switch (randInt)
        {
            case 0:
                // 버섯 생성
                balshroom.CreateBlueMushroom(target.transform);

                target.charactorState.AddState(StateType.blueSpore, debuffStack);
                break;
            case 1:
                // 버섯 생성
                balshroom.CreateRedMushroom(target.transform);

                target.charactorState.AddState(StateType.redSpore, debuffStack);
                break;
            default:
                Debug.LogWarning("랜덤이 이상해~~!!!");
                break;
        }
        caster.tension += keywordTension;
    }
}
