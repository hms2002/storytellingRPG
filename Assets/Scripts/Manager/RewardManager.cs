using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private Actor player;
    public static RewardManager instance;
    List<List<GameObject>> rewardPrefabSets = new List<List<GameObject>>();
    [SerializeField] List<GameObject> rewardSet1;
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    public void  SwowRewards()
    {

    }

    public void AddMainKeywordToDeck(GameObject _keywordmain)
    {
        player.AddMainKeywordToOriginalDeck(_keywordmain);
    }

    public void AddSupKeywordToDeck(GameObject _keywordSup)
    {
        player.AddSupKeywordToOriginalDeck(_keywordSup);
    }
}
