using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    /// <summary>
    /// 보호
    /// </summary>
    protect,
    /// <summary>
    /// 유리파편
    /// </summary>
    glassPragment,
    /// <summary>
    /// 용의 보물
    /// </summary>
    treasureOfDragon,
    /// <summary>
    /// 어미 용의 부름
    /// </summary>
    callingOfMommyDragon,
    /// <summary>
    /// 돌 조각
    /// </summary>
    stonePiece,
    /// <summary>
    /// 증식
    /// </summary>
    multiplication,
    /// <summary>
    /// 광석
    /// </summary>
    ore,
    /// <summary>
    /// 강화
    /// </summary>
    reinforce,
    /// <summary>
    /// 화상
    /// </summary>
    burn,
    /// <summary>
    /// 맹독, 스택 당 2피해
    /// </summary>
    venom,
    /// <summary>
    /// 약화, 공격 데미지 감소
    /// </summary>
    reduction,
    /// <summary>
    /// 취약
    /// </summary>
    weaken,
    /// <summary>
    /// 공포, 스택 * 10% 입히는 데미지 감소
    /// </summary>
    fear,
    /// <summary>
    /// 중독, 스택 당 5 피해와 5 긴장도, 최대 8스택
    /// </summary>
    addiction,
    /// <summary>
    /// 가시돋은
    /// </summary>
    pike,
    /// <summary>
    /// 일회성 보호
    /// </summary>
    oneTimeProtect,
    /// <summary>
    /// 일회성 강화
    /// </summary>
    oneTimeReinforce,
    /// <summary>
    /// 일회성 약화, 공격 데미지 감소
    /// </summary>
    oneTimeReduction,
    /// <summary>
    /// 다음 턴 데미지 증가
    /// </summary>
    nextTurnDamage,
    /// <summary>
    /// 뿌리 공격
    /// </summary>
    tentacleAttack,
    /// <summary>
    /// 뿌리 강화
    /// </summary>
    tentacleCondolidation,
    /// <summary>
    /// 도둑이야!
    /// </summary>
    thief,
    /// <summary>
    /// 기절 (공격 및 선택 불가)
    /// </summary>
    faint,
    /// <summary>
    /// 이탈(날치기)
    /// </summary>
    secession,
    /// <summary>
    /// 회피(공격 50퍼로 회피)
    /// </summary>
    evasion,
    /// <summary>
    /// 자가수복(보석정령)
    /// </summary>
    selfRepair,
    /// <summary>
    /// 코어과부하(보석정령)
    /// </summary>
    coreOverload,
    /// <summary>
    /// 상태 목록 갯수
    /// </summary>
    Size
}

public class CharactorState
{
    ActorStateUIControler stateUIController;

    public State[] allStateList = new State[(int)StateType.Size];

    public void Init(ActorStateUIControler _stateUIController)
    {
        stateUIController = _stateUIController;
        foreach (StateType type in Enum.GetValues(typeof(StateType)))
            ResetState(type);
    }

    public void AddState(StateData data, int val)
    {
        if (allStateList[(int)data.type] == null)
        {
            allStateList[(int)data.type] = new State(data, val);
            Debug.Log(stateUIController);
            stateUIController.UpdateUI(allStateList[(int)data.type]);
            return;
        }
        allStateList[(int)data.type].AddState(val);// 재귀 아님, state클래스 내부 함수임
        stateUIController.UpdateUI(allStateList[(int)data.type]);
    }
    public void AddState(StateType type, int val)
    {
        StateDatabase stateDB = StateDatabase.stateDatabase;
        switch (type)
        {
            case StateType.glassPragment:
                AddState(stateDB.glassPragment, val);
                break;

            case StateType.treasureOfDragon:
                AddState(stateDB.treasureOfDragon, val);
                break;
            case StateType.callingOfMommyDragon:
                AddState(stateDB.callingOfMommyDragon, val);
                break;
            case StateType.stonePiece:
                AddState(stateDB.stonePiece, val);
                break;
            case StateType.multiplication:
                AddState(stateDB.multiplication, val);
                break;
            case StateType.ore:
                AddState(stateDB.ore, val);
                break;
            case StateType.reinforce:
                AddState(stateDB.reinforce, val);
                break;
            case StateType.burn:
                AddState(stateDB.burn, val);
                break;
            case StateType.venom:
                AddState(stateDB.venom, val);
                break;
            case StateType.reduction:
                AddState(stateDB.reduction, val);
                break;
            case StateType.weaken:
                AddState(stateDB.weaken, val);
                break;
            case StateType.fear:
                AddState(stateDB.fear, val);
                break;
            case StateType.addiction:
                AddState(stateDB.addiction, val);
                break;
            case StateType.pike:
                AddState(stateDB.pike, val);
                break;
            case StateType.oneTimeProtect:
                AddState(stateDB.oneTimeProtect, val);
                break;
            case StateType.oneTimeReinforce:
                AddState(stateDB.oneTimeReinforce, val);
                break;
            case StateType.oneTimeReduction:
                AddState(stateDB.oneTimeReduction, val);
                break;
            case StateType.nextTurnDamage:
                AddState(stateDB.nextTurnDamage, val);
                break;
            case StateType.tentacleAttack:
                AddState(stateDB.tentacleAttack, val);
                break;
            case StateType.tentacleCondolidation:
                AddState(stateDB.tentacleCondolidation, val);
                break;
            case StateType.faint:
                AddState(stateDB.faint, val);
                break;
            case StateType.secession:
                AddState(stateDB.secession, val);
                break;
            case StateType.evasion:
                AddState(stateDB.evasion, val);
                break;
            case StateType.thief:
                AddState(stateDB.thief, val);
                break;
            case StateType.selfRepair:
                AddState(stateDB.selfRepair, val);
                break;
            case StateType.coreOverload:
                AddState(stateDB.coreOverload, val);
                break;
            default:
                Debug.LogError("추가되지 않은 상태 입력");
                break;
        }
    }
    public int GetStateStack(StateType type)
    {
        if (allStateList[(int)type] == null) return 0;

        return allStateList[(int)type].stack;
    }

    /// <summary>
    /// 모든 디버프 제거
    /// </summary>
    public int DeleteAllDebuff()
    {
        int deletedDebuff = 0;
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0
                || i.stateData.stateProperty != StateProperty.Debuff)
                continue;
            i.ResetStack();
            stateUIController.UpdateUI(i);
            deletedDebuff++;
        }
        return deletedDebuff;
    }
    public int AllDebuffCount()
    {
        int debuffCnt = 0;
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0
                || i.stateData.stateProperty != StateProperty.Debuff)
                continue;
            debuffCnt++;
        }
        return debuffCnt;
    }

    public void DeleteAllBuff()
    {
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0
                || i.stateData.stateProperty != StateProperty.Buff)
                continue;
            i.ResetStack();
            stateUIController.UpdateUI(i);
        }
    }
    /// <summary>
    /// 키워드 선택 전 상태이상 데미지
    /// </summary>
    /// <param name="actor"></param>
    public void StartTurnDamage(Actor actor)
    {
        foreach (State i in allStateList)
        {
            if (i == null || i.stateData.effectByTurn == false
                || i.stack <= 0 || i.stateData.damagePerStack == 0)
                continue;
            AudioManager.instance.PlaySound("Debuff", i.stateData.soundName);
            int stackDamage = i.stateData.damagePerStack;
            if(i.oneTimeMultiplication)
            {
                stackDamage *= 2;
                i.oneTimeMultiplication = false;
            }
            if(i.oneTimeRepeat)
            {
                actor.Damaged(actor, i.stack * stackDamage);
                if (i.stateData.reductionTiming == ReductionTiming.OnAttack)
                {
                    i.Reduction();
                }
                i.oneTimeRepeat = false;
            }
            if(i.stack != 0)
            {
                actor.Damaged(actor, i.stack * stackDamage);
            }
            stateUIController.UpdateUI(i);
        }
    }

    public void StackDamageMultiplication(StateType type)
    {
        allStateList[(int)type].oneTimeMultiplication = true;
    }
    public void StackDamageRepeat(StateType type)
    {
        allStateList[(int)type].oneTimeRepeat = true;
    }

    public void StartTurnEffect(Actor actor)
    {
        OreEffect(actor);
    }

    private void OreEffect(Actor actor)
    {
        if (allStateList[(int)StateType.ore] == null || allStateList[(int)StateType.ore].stack == 0) return;
        // 광석 스택 특수효과
        int oreStack = allStateList[(int)StateType.ore].stack;
        if (oreStack > actor.protect)
            actor.protect = oreStack;
    }

    internal void ReductionOnBeforeAttack()
    {
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0
                || i.stateData.reductionTiming != ReductionTiming.OnBeforeAttack)
                continue;
            i.Reduction();
            stateUIController.UpdateUI(i);
        }
    }

    public void ReductionOnStartTurn()
    {
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0
                || i.stateData.reductionTiming != ReductionTiming.OnShowSupKeywords)
                continue;
            i.Reduction();
            stateUIController.UpdateUI(i);
        }
    }

    public void ReductionOnAttack()
    {
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0
                || i.stateData.reductionTiming != ReductionTiming.OnAttack)
                continue;
            i.Reduction();
            stateUIController.UpdateUI(i);
        }
    }

    public void ReductionOnDamaged()
    {
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0
                || i.stateData.reductionTiming != ReductionTiming.OnDamaged)
                continue;
            i.Reduction();
            stateUIController.UpdateUI(i);
        }
    }

    public void ReductionOnMyTurn()
    {
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0
                || i.stateData.reductionTiming != ReductionTiming.OnMyTurn)
                continue;
            i.Reduction();
            stateUIController.UpdateUI(i);
        }
    }

    public void ReductionByValue(StateType type, int val)
    {
        if (allStateList[(int)type] == null) return;
        allStateList[(int)type].ReductionByValue(val);
        stateUIController.UpdateUI(allStateList[(int)type]);
    }

    public int BuffCount()
    {
        int buffCount = 0;
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0) continue;
            buffCount++;
        }
        return buffCount;
    }

    public void AddAllActiveState(int val)
    {
        foreach (State i in allStateList)
        {
            if (i == null || i.stack <= 0) continue;
            i.stack += val;
            stateUIController.UpdateUI(i);
        }
    }

    public void ResetState(StateType type)
    {
        if (type == StateType.Size ||  allStateList[(int)type] == null) return;
        allStateList[(int)type].ResetStack();
        stateUIController.UpdateUI(allStateList[(int)type]);
    }

    internal void DestroySelf()
    {
    }
}


    //public int pike
    //{
    //    get { return buffList[(int)BuffType.pike]; }
    //    set
    //    {
    //        _pike = value;
    //        buffList[(int)BuffType.pike] = _pike;
    //        allStateList[(int)StateType.pike] = _pike;
    //    }
    //}

    //public int burnStack
    //{
    //    get { return debuffList[(int)DebuffType.burn]; }
    //    set
    //    {
    //        _burnStack = value;
    //        debuffList[(int)DebuffType.burn] = _burnStack;
    //        allStateList[(int)StateType.burn] = _burnStack;
    //        if (_burnStack < 0)
    //        {
    //            _burnStack = 0;
    //        }
    //        stateUIController.BurnOn(_burnStack);
    //    }
    //}


    //public int venomStack
    //{
    //    get { return debuffList[(int)DebuffType.venom]; }
    //    set
    //    {
    //        debuffList[(int)DebuffType.venom] = _venomStack;
    //        allStateList[(int)StateType.venom] = _venomStack;
    //        if (_venomStack < 0)
    //        {
    //            _venomStack = 0;
    //        }
    //        _venomStack = value;
    //    }
    //}


    //public int weakenStack
    //{
    //    get { return debuffList[(int)DebuffType.weaken]; }
    //    set
    //    {

    //        debuffList[(int)DebuffType.weaken] = _weakenStack;
    //        allStateList[(int)StateType.weaken] = _weakenStack;
    //        if (_weakenStack < 0)
    //        {
    //            _weakenStack = 0;
    //        }
    //        _weakenStack = value;

    //        stateUIController.WeakenOn(_weakenStack);
    //        Debug.Log("취약 스택" + _weakenStack);
    //    }
    //}


    //public int reductionStack
    //{
    //    get { return debuffList[(int)DebuffType.reduction]; }
    //    set
    //    {
    //        debuffList[(int)DebuffType.reduction] = _reductionStack;
    //        allStateList[(int)StateType.reduction] = _reductionStack;
    //        if (_reductionStack < 0)
    //        {
    //            _reductionStack = 0;
    //        }
    //        _reductionStack = value;

    //        stateUIController.ReductionOn(_reductionStack);
    //    }
    //}

    //public int fearStack
    //{
    //    get { return debuffList[(int)DebuffType.fear]; }
    //    set
    //    {
    //        debuffList[(int)DebuffType.fear] = _fearStack;
    //        allStateList[(int)StateType.fear] = _fearStack;
    //        _fearStack = value;

    //    }
    //}

    //public int addictionStack
    //{
    //    get { return debuffList[(int)DebuffType.addiction]; }
    //    set
    //    {
    //        debuffList[(int)DebuffType.addiction] = _addictionStack;
    //        allStateList[(int)StateType.addiction] = _addictionStack;
    //        _addictionStack = value;

    //        //stateUIController.AddictionOn(_addictionStack);
    //    }
    //}

    //public int nextTurnDamage
    //{
    //    get { return buffList[(int)BuffType.nextTurnDamage]; }
    //    set
    //    {
    //        buffList[(int)BuffType.nextTurnDamage] = _nextTurnDamage;
    //        allStateList[(int)StateType.nextTurnDamage] = _nextTurnDamage;
    //        _nextTurnDamage = value;
    //    }
    //}

    //public int oneTimeReinforce
    //{
    //    get { return buffList[(int)BuffType.oneTimeReinforce]; }
    //    set
    //    {
    //        buffList[(int)BuffType.oneTimeReinforce] = _oneTimeReinforce;
    //        allStateList[(int)StateType.oneTimeReinforce] = _oneTimeReinforce;
    //        _oneTimeReinforce = value;
    //    }
    //}

    //public int oneTimeProtect
    //{
    //    get { return (int)BuffType.oneTimeProtect; }
    //    set
    //    {
    //        buffList[(int)BuffType.oneTimeProtect] = _oneTimeProtect;
    //        allStateList[(int)StateType.oneTimeProtect] = _oneTimeProtect;
    //        _oneTimeProtect = value;

    //    }
    //}

    //public int oneTimeReduction
    //{
    //    get { return debuffList[(int)DebuffType.oneTimeReduction]; }
    //    set
    //    {
    //        debuffList[(int)DebuffType.oneTimeReduction] = _oneTimeReduction;
    //        allStateList[(int)StateType.oneTimeReduction] = _oneTimeReduction;
    //        _oneTimeReduction = value;
    //    }
    //}