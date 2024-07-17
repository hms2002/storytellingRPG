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
        SetKeywordColor(RED);
        keywordTension = -18;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.oneTimeReduction += oneTimeReductionControl;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
