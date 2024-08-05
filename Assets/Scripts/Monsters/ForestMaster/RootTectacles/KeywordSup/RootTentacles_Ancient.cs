using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTentacles_Ancient : KeywordSup
{
    RootTectacles rootTectacles;
    private void Awake()
    {
        keywordName = "고대의";

        SetKeywordColor(B);
        keywordTension = -10;
        keywordProtect = 3;
        Init();
    }
    public override void Execute(Actor caster, Actor target)
    {
        if (caster is RootTectacles)
        {
            rootTectacles = (RootTectacles)caster;

            if (rootTectacles.forestMaster != null)
                rootTectacles.forestMaster.protect += keywordProtect;
        }
        caster.tension += keywordTension;
    }

    public override void Check(KeywordMain _keywordSup)
    {

    }

    public override void CanUseCheck(Actor caster, Actor target)
    {
        if(caster is RootTectacles)
        {
            rootTectacles = (RootTectacles)caster;

            if (rootTectacles.forestMaster != null)
                isCanUse = true;            
            else
                isCanUse = false;
        }
    }
}
