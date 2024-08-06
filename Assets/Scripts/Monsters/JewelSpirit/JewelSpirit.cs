using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelSpirit : Monster
{
    private void Awake()
    {
        MAX_HP = 1;
        protect = 70;
        hp = MAX_HP;
        charactorState.AddState(StateType.selfRepair, 1);
        encounterText = "보석 덩어리에서 강한 마력이 느껴지기 시작했다.";
    }

    [SerializeField] private Sprite overLoadSprite;
    [SerializeField] private List<GameObject> overLoadKeyword;

    private void OverLoading()
    {
        if (protect >= 90)
        {
            deck.ClearDeckList(WhatDeck.MainDeck);

            foreach (GameObject i in overLoadKeyword)
            {
                deck.AddMainKeywordOnDeck(i);
            }

            garbageField.ClearDeckList(WhatDeck.MainDeck);
        }
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = overLoadSprite;
        charactorState.AddState(StateType.coreOverload, 1);
        charactorState.ResetState(StateType.selfRepair);
    }

    public override void Action(Actor target)
    {
        base.Action(target);
        OverLoading();
    }
}