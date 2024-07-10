using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : KeywordSup
{
    int damage = 0;

    private void Awake()
    {
        SetKeywordColor(RED);
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        sentence.damage += (damage);
    }

    public override void Check(KeywordMain _keywordMain)
    {
        if (_keywordMain.GetKeywordColor() == RED) // 메인 키워드의 색이 빨간색이면
        {
            damage = 3;
        }
        else // 메인 키워드의 색이 빨간색이 아니면
        {
            damage = 0;
        }
    }
}
