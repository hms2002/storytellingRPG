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
        sentence.damage += (damage);
    }

    public override void Check(KeywordMain _keywordMain)
    {
        if (_keywordMain.GetKeywordColor() == RED) // ���� Ű������ ���� �������̸�
        {
            keywordDamage = 3;
        }
        else // ���� Ű������ ���� �������� �ƴϸ�
        {
            keywordDamage = 0;
        }
    }
}
