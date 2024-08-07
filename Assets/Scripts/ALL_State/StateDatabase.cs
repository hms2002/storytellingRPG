using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDatabase : MonoBehaviour
{
    public StateData glassPragment;
    public StateData treasureOfDragon;
    public StateData callingOfMommyDragon;
    public StateData stonePiece;
    public StateData multiplication;
    public StateData ore;
    public StateData reinforce;
    public StateData burn;
    public StateData venom;
    public StateData reduction;
    public StateData weaken;
    public StateData fear;
    public StateData addiction;
    public StateData pike;
    public StateData oneTimeProtect;
    public StateData oneTimeReinforce;
    public StateData oneTimeReduction;
    public StateData nextTurnDamage;
    public StateData tentacleAttack;
    public StateData tentacleCondolidation;
    public StateData secession;
    public StateData evasion;
    public StateData thief;

    public static StateDatabase stateDatabase;

    private void OnEnable()
    {
        if (stateDatabase != null) Destroy(this);
        stateDatabase = this;
    }
}
