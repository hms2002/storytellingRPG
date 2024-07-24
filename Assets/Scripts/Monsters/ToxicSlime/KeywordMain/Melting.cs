using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melting : KeywordMain
{
    [Header("녹이기 키워드 수치 제어")]
    [SerializeField] private const int _damagePerAddictionStack = 4;

    public int damagePerAddictionStack { get { return _damagePerAddictionStack; } }


    private void Awake()
    {
        keywordName = "녹이기";
        SetKeywordColor(B);
        keywordTension = -10;
    }

    public override void Execute(Actor caster, Actor target)
    {
        int addictionStack = target.charactorState.GetStateStack(StateType.addiction);
        caster.damage += addictionStack * damagePerAddictionStack;

        caster.tension += addictionStack * keywordTension;

        target.charactorState.ResetState(StateType.addiction);
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
