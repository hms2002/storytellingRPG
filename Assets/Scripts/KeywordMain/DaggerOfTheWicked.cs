using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerOfTheWicked : KeywordMain
{
    [SerializeField]
    private int extraDamage = 3;

    private void Awake()
    {
        keywordName = "������ �ܰ�";
        keywordDamage = 2;
        keywordProtect = 0;
    }

    public override void Execute(Actor caster, Actor target, Sentence sentence)
    {
        if (target.weakenStack > 0) // Ÿ���� ��� ���¶��
        {
            sentence.DamageControl(keywordDamage + extraDamage); // Ű���� �������� �߰������� ����
        }
        else // Ÿ���� ��� ���°� �ƴ϶��
        {
            sentence.DamageControl(keywordDamage); // Ű���� �������� ����
        }
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
