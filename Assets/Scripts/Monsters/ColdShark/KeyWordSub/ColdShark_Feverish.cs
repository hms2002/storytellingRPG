using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_Feverish : KeywordSup
{
    [Header("열이 나는 키워드 자해 데미지 수치")]
    [SerializeField] private int selfDamage = 10;

    // Start is called before the first frame update
    private void Awake()
    {
        keywordName = "열이 나는";
        SetKeywordColor(R);
        keywordTension = -5;
        keywordDamage = selfDamage;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        target.damage += selfDamage;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
