using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGrip_CurseBomb : KeywordMain
{
    private void Awake()
    {
        keywordName = "저주 폭탄";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += target.charactorState.AllDebuffCount();
        target.charactorState.DeleteAllDebuff();
        caster.tension += keywordTension;
    }
}
