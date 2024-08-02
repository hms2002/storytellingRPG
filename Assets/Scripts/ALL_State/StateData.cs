using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ReductionType
{
    minus1,
    divide2,
    changeZero,
    noReduction
}
public enum ReductionTiming
{
    OnShowSupKeywords,
    OnDamaged,
    OnAttack,
    OnMyTurn,
    None
}
public enum StateProperty
{
    Buff,
    Debuff,
    None
}
[CreateAssetMenu]
public class StateData : ScriptableObject
{
    public StateType type;
    public ReductionType reductionType;
    public ReductionTiming reductionTiming;
    public StateProperty stateProperty;
    public Sprite stateImage;
    public string soundName = "";

    public int MAX_STACK;
    public int damagePerStack;
    public int tentionPerStack;

    [Multiline(5)]
    public string stateExplanation;
    [Header ("턴 시작할 때 피해를 주는가")]
    public bool effectByTurn;
}
