using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public GameObject keywardPrefab;


    public void AddThisToMainDeck()
    {
        RewardManager.instance.AddMainKeywordToDeck(keywardPrefab);
    }
    public void AddThisToSupDeck()
    {
        RewardManager.instance.AddSupKeywordToDeck(keywardPrefab);
    }
}
