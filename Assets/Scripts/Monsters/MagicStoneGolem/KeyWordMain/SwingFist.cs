using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingFist : KeywordMain
{
    [Header("돌 조각 당 긴장도 제어")]
    [SerializeField] private int stackTension = 5;
    private void Awake()
    {
        keywordName = "주먹 휘두르기";

        SetKeywordColor(R);
        keywordDamage = 5;
        keywordTension = 10;

        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += keywordDamage * caster.charactorState.GetStateStack(StateType.stonePiece);
        caster.tension += keywordTension + caster.charactorState.GetStateStack(StateType.stonePiece) * stackTension;
        caster.charactorState.ResetState(StateType.stonePiece);
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
