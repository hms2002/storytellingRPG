using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flawless : KeywordSup
{
    [Header("부여되는 강화 수치 제어")]
    [SerializeField] private int amountOfReinforce = 3;


    private void Awake()
    {
        keywordName = "쪼개진";
        SetKeywordColor(R);
        keywordTension = 8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        // 강화 수치 3 증가
        caster.charactorState.AddState(StateDatabase.stateDatabase.reinforce, amountOfReinforce);

        target.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
