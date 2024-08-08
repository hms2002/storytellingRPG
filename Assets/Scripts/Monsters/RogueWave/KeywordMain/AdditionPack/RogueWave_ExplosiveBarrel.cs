using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave_ExplosiveBarrel : KeywordMain
{
    private void Awake()
    {
        keywordName = "폭발성 드럼통";

        SetKeywordColor(R);
        Init();
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.damage += keywordDamage;
        caster.tension += keywordTension;
    }
}
