using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardeningRock : KeywordMain
{
    MagicStoneGolem magicStoneGolem;
    private void Awake()
    {
        keywordName = "바위 굳히기";

        SetKeywordColor(Y);
        keywordTension = -8;
        effectTarget = EffectTarget.caster;
        effectType = EffectManager.EffectType.ItemUse;
        Init();
    }

    public override void Execute(Actor caster, Actor target)
    {
        magicStoneGolem = caster as MagicStoneGolem;
        magicStoneGolem.stonePiece += 1;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordSup _keywordSup)
    {

    }
}
