using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingFist : KeywordMain
{
    MagicStoneGolem magicStoneGolem;
    [Header("돌 조각 당 긴장도 제어")]
    [SerializeField] private int stackTension = 5;
    private void Awake()
    {
        keywordName = "주먹 휘두르기";

        SetKeywordColor(RED);
        keywordDamage = 5;
        keywordTension = 10;

    }

    public override void Execute(Actor caster, Actor target)
    {
        magicStoneGolem = caster as MagicStoneGolem;
        caster.damage += keywordDamage * magicStoneGolem.stonePiece;
        caster.tension += keywordTension + magicStoneGolem.stonePiece * stackTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
