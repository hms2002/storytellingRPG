using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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

    public override void ShowSupKeywords()
    {
        deckInfoPivot.arePlayerChoosingKeyword = false;
        garbageFieldInfoPivot.arePlayerChoosingKeyword = false;

        FillSupHandInfo();
        hand.SubstantiateSupKeywordData();
    }

    protected override void ShowMainKeywords()
    {
        FillMainHandInfo();
        hand.SubstantiateMainKeywordData();
    }

    protected override void FillSupHandInfo()
    {
        // Keyword 드로우 3번 반복
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            // Support덱이 비어있다면
            if (deck.IsSupDeckEmpty())
            {
                // 무덤덱에서 카드를 꺼내와 Support덱을 초기화
                for (int j = 0; j < garbageField.SupportDeck.Count; j++)
                {
                    deck.AddSupKeywordOnDeck(garbageField.DrawSupKeyword());
                }
            }

            // Support덱에서 1장 랜덤 드로우
            hand.SetSupPrefabInfo(deck.DrawSupKeyword());
        }
    }

    protected override void FillMainHandInfo()
    {
        // Keyword 드로우 3번 반복
        for (int i = 0; i < hand.HANDSIZE; i++)
        {
            // Main덱이 비어있다면
            if (deck.IsMainDeckEmpty())
            {
                // 무덤덱에서 카드를 꺼내와 Main덱을 초기화
                for (int j = 0; j < garbageField.MainDeck.Count; j++)
                {
                    deck.AddMainKeywordOnDeck(garbageField.DrawMainKeyword());
                }
            }

            // Main덱에서 각각 1장 랜덤 드로우                
            hand.SetMainPrefabInfo(deck.DrawMainKeyword());
        }
    }

    public void DestroySelf()
    {
        stateUIController.DestroySelf();
        charactorState.DestroySelf();
        RewardManager.instance.rewardGold += gold;
        Destroy(gameObject);
    }
}
