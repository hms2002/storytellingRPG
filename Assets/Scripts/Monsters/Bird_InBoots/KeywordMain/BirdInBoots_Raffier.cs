using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdInBoots_Raffier : KeywordMain
{
    private void Awake()
    {
        keywordName = "깃털 레이피어";

        SetKeywordColor(R);
        Init();
    }
    public override void Check(KeywordSup _keywordSup)
    {
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.repeatStack += 2;
        caster.dmgList.Add(keywordDamage);
        caster.tension += keywordTension;
    }
}
