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
        if (_keywordMain.GetKeywordColor() == RED) // ���� Ű������ ���� �������̸�
        {
            damage = 3;
        }
        else // ���� Ű������ ���� �������� �ƴϸ�
        {
            damage = 0;
        }
    }
}
