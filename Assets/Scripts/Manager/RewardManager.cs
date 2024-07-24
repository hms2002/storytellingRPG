using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private Actor player;
    public static RewardManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
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
