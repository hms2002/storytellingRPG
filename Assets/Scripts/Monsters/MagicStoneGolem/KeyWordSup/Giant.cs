using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : KeywordSup
{
    MagicStoneGolem magicStoneGolem;
    private void Awake()
    {
        keywordName = "거대한";
        SetKeywordColor(BLUE);
        keywordTension = -8;
    }

    public override void Execute(Actor caster, Actor target)
    {
        magicStoneGolem = caster as MagicStoneGolem;
        magicStoneGolem.stonePiece += 2;
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordMain)
    {
    }
}
