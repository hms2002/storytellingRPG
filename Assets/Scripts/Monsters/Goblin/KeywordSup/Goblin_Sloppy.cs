using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Sloppy : KeywordSup
{
    [Header("부여되는 일회성 약화 수치")]
    [SerializeField] private int oneTimeReductionControl = 1;


    private void Awake()
    {
        keywordName = "엉성한";
        SetKeywordColor(R);
        keywordTension = -18;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, oneTimeReductionControl);

        target.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
