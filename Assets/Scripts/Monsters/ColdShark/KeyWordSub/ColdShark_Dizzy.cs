using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_Dizzy : KeywordSup
{
    [Header("어지러운 키워드 일회성 약화 수치")]
    [SerializeField] private int oneTimeReductionControl = 5;

    // Start is called before the first frame update
    private void Awake()
    {
        keywordName = "어지러운";
        SetKeywordColor(Y);
        keywordTension = -10;
        effectTarget = EffectTarget.target;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.charactorState.AddState(StateDatabase.stateDatabase.oneTimeReduction, oneTimeReductionControl);
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
       
    }
}
