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
    public StateData faint;
    public StateData secession;
    public StateData evasion;
    public StateData thief;
    public StateData selfRepair;
    public StateData coreOverload;
    public StateData mana;
    public StateData absorption;
    public StateData blueSpore;
    public StateData redSpore;
    public StateData counterAttack;
    public StateData ammunition;
    public StateData end;
    public StateData potionGlub_State1;
    public StateData potionGlub_State2;
    public StateData potionGlub_State3;
    public StateData potionGlub_State4;
    public StateData potionGlub_State5;
    public StateData potionGlub_State6;
    public StateData potionGlub_State7;
    public static StateDatabase stateDatabase;

    private void OnEnable()
    {
        if (stateDatabase != null) Destroy(this);
        stateDatabase = this;
    }
}
