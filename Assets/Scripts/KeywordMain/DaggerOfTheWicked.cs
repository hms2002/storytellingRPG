using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerOfTheWicked : KeywordMain
{
    [Header("������ �ܰ� �߰� ������")]
    [SerializeField] private int extraDamage = 3;


    private void Awake()
    {
        keywordName = "������ �ܰ�";
        keywordDamage = 2;
        keywordProtect = 0;
    }

    public override void Execute(Actor caster, Actor target)
    {
        if (target.weakenStack > 0)     // Ÿ���� ��� ���¶��
        {
            caster.damage += keywordDamage + extraDamage;   // Ű���� �������� �߰������� ����
        }
        else                            // Ÿ���� ��� ���°� �ƴ϶��
        {
            caster.damage += keywordDamage;                 // Ű���� �������� ����
        }
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
