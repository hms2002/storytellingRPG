using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
{
    private string _encounterText = " ";

    public string encounterText
    {
        get { return _encounterText; }
        set 
        { 
            _encounterText = value;
           /* if(_encounterText == " ")
            {
                _encounterText = "전투가 시작됐다.";
            }*/
        }
    }

    public override void Action (Actor target)
    {
        base.Action(target);
    }

    public void DestroySelf()
    {
        stateUIController.DestroySelf();
        charactorState.DestroySelf();
    }
}
