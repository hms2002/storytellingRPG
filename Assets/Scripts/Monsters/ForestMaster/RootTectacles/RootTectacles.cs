using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTectacles : Monster
{
    private ForestMaster _forestMaster;

    public ForestMaster forestMaster
    {
        get
        {
            return _forestMaster;
        }
        private set
        {
            _forestMaster = value;
        }
    }

    private int _repeatCnt = 0;
    public int repeatCnt { get { return _repeatCnt; } set { _repeatCnt = value; } }
    private int _tentacleAttackDamage = 2;
    public int tentacleAttackDamage { get { return _tentacleAttackDamage; } set { _tentacleAttackDamage = value; } }

    public void Init(ForestMaster _forestMaster)
    {
        forestMaster = _forestMaster;
    }
    public override void Action(Actor target)
    {
        base.Action(target);

        TentacleAttack(target);
    }

    private void TentacleAttack(Actor target)
    {
        if (charactorState.GetStateStack(StateType.tentacleCondolidation) > 0) 
            tentacleAttackDamage += 2;

        if (forestMaster != null)
            repeatCnt += forestMaster.charactorState.GetStateStack(StateType.multiplication);

        for(int i = 0; i <= repeatCnt; i++)
        {
            target.Damaged(this, tentacleAttackDamage);
        }
        repeatCnt = 0;
        tentacleAttackDamage = 2;
    }
}
