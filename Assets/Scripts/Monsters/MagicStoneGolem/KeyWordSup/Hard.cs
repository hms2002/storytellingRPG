using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hard : KeywordSup
{
    private bool isHardKeyword = false;
    private void Awake()
    {
        keywordName = "단단한";
        SetKeywordColor(R);
        keywordTension = 8;
        keywordDamage = 5;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage;
        if(isHardKeyword)
        {
            caster.charactorState.AddState(StateDatabase.stateDatabase.stonePiece, 1);
        }
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
        if(_keywordMain.keywordName == "바위 굳히기")
        {
            isHardKeyword = true;
        }
    }
}
