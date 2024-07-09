using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeywordSup : MonoBehaviour
{
    public abstract void Execute(Actor caster, Actor target, Sentence sentence);
    public abstract void Check(KeywordMain _keywordMain);
}
