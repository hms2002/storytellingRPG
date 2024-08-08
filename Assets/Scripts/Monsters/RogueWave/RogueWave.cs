using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueWave : Monster
{
    public enum stateList
    {
        glassFragment = StateType.glassPragment,
        weaken = StateType.weaken,
        protect = StateType.protect,
        reduction = StateType.reduction,
        reinforce = StateType.reinforce
    }
    public List<GameObject> additionalDeck;

    private void Awake()
    {
        MAX_HP = 272;
        hp = MAX_HP;
        encounterText = "잠잠한 파도만을 상대하면 그것은 모험이라고 부를 수 없을 것이다.";
    }

    public void AddKeyword()
    {
        int rand = Random.Range(0, additionalDeck.Count);
        deck.AddMainKeywordOnDeck(additionalDeck[rand]);
    }
}
