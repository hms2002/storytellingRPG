using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shirimpper : Monster
{
    [Header("초기 탄약수치")]
    [SerializeField] private int beginAmmo = 5;
    private List<GameObject> supDeck;
    private bool ammoDeckCheck = false;

    private void Start()
    {
        supDeck = new List<GameObject>(deck.SupportDeck);
        charactorState.AddState(StateType.ammunition, beginAmmo);
    }

    private void Awake()
    {
        MAX_HP = 50;
        hp = MAX_HP;
        encounterText = "탕! 탕! 큰 총소리에 비해 조그마한 새우가 튀어 올라왔다.";
    }

    [Header("탄약 없을 때 덱")]
    [SerializeField] private List<GameObject> NoAmmoKeywords;

    private void NoAmmo()
    {
        if (charactorState.GetStateStack(StateType.ammunition) <= 0)
        {
            deck.ClearDeckList(WhatDeck.SupportDeck);

            foreach (GameObject i in NoAmmoKeywords)
            {
                deck.AddSupKeywordOnDeck(i);
            }

            garbageField.ClearDeckList(WhatDeck.SupportDeck);
            ammoDeckCheck = true;
        }
    }

    private void YesAmmo()
    {
        if (charactorState.GetStateStack(StateType.ammunition) > 0 && ammoDeckCheck)
        {
            deck.ClearDeckList(WhatDeck.SupportDeck);

            foreach (GameObject i in supDeck)
            {
                deck.AddSupKeywordOnDeck(i);
            }

            garbageField.ClearDeckList(WhatDeck.SupportDeck);
            ammoDeckCheck = false;
        }
    }

    public override void Action(Actor target)
    {
        base.Action(target);
        NoAmmo();
        YesAmmo();
    }
}