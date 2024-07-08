using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : KeywordSup
{
    public override void Execute(KeywordMain mainKeyword)
    {
        mainKeyword.UpRepeatCount(1);
    }
}
