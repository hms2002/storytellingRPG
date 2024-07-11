using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager fightManager;

    private int preparedActorCount = 0;

    [Header("�÷��̾�� ������Ʈ")]
    [SerializeField] private Actor player;
    [SerializeField] private Actor monster;

    private Actor whoPlaying;
    private KeywordSup keywordSup;
    private KeywordMain keywordMain;


    void Awake()
    {
        if (fightManager != null)
            return;
        fightManager = this;
    }

    private void Start()
    {
        while (true)
        {
            Flow();
        }
    }

    public void GetKeywordSup(KeywordSup _keywordSup)
    {
        if (_keywordSup == null)
            return;
        whoPlaying.GetKeywordSup(_keywordSup);
    }

    public void GetKeywordMain(KeywordMain _keywordMain)
    {
        if (_keywordMain == null)
            return;
        whoPlaying.GetKeywordMain(_keywordMain);
        preparedActorCount++;
        Flow();
    }

    //void FightStart()
    //{
    //    // �÷��̾� ����
    //    // ���� ����
    //    // ����Ʈ�� ����ֱ�

    //}

    public void Flow()
    {
        // Ű���� ���� ��, ���� ����� ����
        if(preparedActorCount == 0)
        {
            player.BeforeAction();
            monster.BeforeAction();
        }

        // ���ͺ��� ���ʷ� Ű���� ����
        switch(preparedActorCount)
        {
            case 0:
                whoPlaying = monster;
                monster.StartTurn();
                return;
                
            case 1:
                whoPlaying = player;
                player.StartTurn();
                return;

            case 2:
                preparedActorCount = 0;
                break;

        }

        // �÷��̾�, ���� ������ Ű���� ����
        player.Action(monster);
        monster.Action(player);
    }
}
