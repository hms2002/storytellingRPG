using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelSpirit : Monster
{
    [Header("초기 보호수치")]
    [SerializeField] private int biginProtect = 70;
    private void Start()
    {
        charactorState.AddState(StateType.selfRepair, 1);
        protect = biginProtect;
    }

    private void Awake()
    {
        MAX_HP = 1;
        hp = MAX_HP;
        encounterText = "보석 덩어리에서 강한 마력이 느껴지기 시작했다.";
    }

    [Header("과부하 스프라이트")]
    [SerializeField] private Sprite overLoadSprite;
    [Header("과부하 키워드 덱")]
    [SerializeField] private List<GameObject> overLoadKeyword;
    [Header ("자가수복 보호부여 수치")]
    [SerializeField] private int selfRepairProtect;

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
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = overLoadSprite;
            charactorState.AddState(StateType.coreOverload, 1);
            charactorState.ResetState(StateType.selfRepair);
        }
    }

    public override void Action(Actor target)
    {
        base.Action(target);
        OverLoading();
        if(charactorState.GetStateStack(StateType.selfRepair) > 0)
        {
            protect += 15;
        }
        if(protect <= 0)
        {
            hp = 0;
        }
    }

    protected override int CalculateProtect(int totalDamage)
    {
        if (protect > 0)
        {
            if (protect < totalDamage)
            {
                totalDamage -= protect;
                protect = 0;
                hp = 0;
            }
            else
            {
                protect -= totalDamage;
                totalDamage = 0;
            }
        }
        return totalDamage;
    }
}