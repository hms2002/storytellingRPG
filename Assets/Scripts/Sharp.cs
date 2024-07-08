using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharp : KeywordSup
{
    public override void Execute(KeywordMain mainKeyword )
    {
        Debug.Log("Sharp ¹ßµ¿");
        mainKeyword.AddDamage(2);
    }
}
