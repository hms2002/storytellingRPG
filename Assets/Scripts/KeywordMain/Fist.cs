using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : KeywordMain
{
    [Header("¡÷∏‘ µ•πÃ¡ˆ")]
    [SerializeField] private int fistDamage = 1;


    private void Awake()
    {
        keywordName = "¡÷∏‘";
        SetKeywordColor(R);
        keywordDamage = 1;
    }

    public override void Execute(Actor caster, Actor target)
    {
        caster.damage += fistDamage;
    }

    public override void Check(KeywordSup _keywordSup)
    {
    }
}
