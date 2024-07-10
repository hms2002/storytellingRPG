using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : KeywordSup
{
    private void Awake()
    {
        SetKeywordColor(RED);
        keywordDamage = 0;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.DamageControl(keywordDamage);
    }

    public override void Check(KeywordMain _keywordMain)
    {
        if (_keywordMain.GetKeywordColor() == RED) // 메인 키워드의 색이 빨간색이면
        {
            keywordDamage = 3;
        }
        else // 메인 키워드의 색이 빨간색이 아니면
        {
            keywordDamage = 0;
        }
    }
}
