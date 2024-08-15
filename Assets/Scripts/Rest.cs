using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour
{
    Actor player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
    }

    //안정된 휴식
    public void StableHealing()
    {
        player.hp += 30;
    }

    //효율적인 휴식
    public void EfficientHealing()
    {
        player.tension += 50;
    }

    //조화로운 휴식
    public void HarmoniousHealinga()
    {
        player.hp += 15;
        player.tension += 25;
    }
}
