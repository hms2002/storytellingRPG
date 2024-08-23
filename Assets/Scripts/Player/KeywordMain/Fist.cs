using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : KeywordMain
{
    private void Awake()
    {
        isPlayerKeyword = true;
        SetKeywordColor(R);
        Init();
        keywordDamage = keywordDamage + GameManager.instance.killCnt_bossMonster + GameManager.instance.killCnt_eleteMonster + GameManager.instance.killCnt_nomalMonster;
}

    public override void Execute(Actor caster, Actor target)
    {
        caster.dmgList.Add(keywordDamage);
    }

    public override void Check(KeywordSup _keywordSup) { }
}
