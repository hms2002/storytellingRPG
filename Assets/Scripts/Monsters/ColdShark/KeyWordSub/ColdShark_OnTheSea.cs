using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdShark_OnTheSea : KeywordSup
{
    [Header("바다 위의 키워드 ")]
    [SerializeField] private int protecet = 5;

    // Start is called before the first frame update
    private void Awake()
    {
        keywordName = "바다 위의";
        SetKeywordColor(B);
        keywordTension = -5;
        keywordProtect = protecet;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.protect = keywordProtect;
        caster.tension = keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {

    }
}
