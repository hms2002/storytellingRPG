using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : StateInterface
{
    public StateData stateData;
    public int stack;

    public State(StateData _stateData, int _stack)
    {
        stateData = _stateData;
        stack = _stack;
    }

    public void AddState(int value)
    {
        stack += value;
        if (stack > stateData.MAX_STACK && stateData.MAX_STACK != 0) stack = stateData.MAX_STACK;
    }

    public void ResetStack()
    {
        stack = 0;
    }

    /// <summary>
    /// stack이 0보다 작거나 같으면 true 반환함
    /// </summary>
    public bool Reduction()
    {
        switch(stateData.reductionType)
        {
            case ReductionType.minus1:
                stack--;
                break;
            case ReductionType.divide2:
                stack /= 2;
                break;
            case ReductionType.changeZero:
                stack = 0;
                break;
        }
        if(stack <= 0)
        {
            stack = 0;
            return true;
        }
        return false;
    }
    public void ReductionByValue(int val)
    {
        stack -= val;
        if (stack <= 0)
        {
            stack = 0;
        }
    }
}
