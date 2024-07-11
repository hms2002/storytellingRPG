using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager fightManager;
    public FightManagerUI fightManagerUI;

    int preparedActorCount = 0;

    public Actor player;
    public Actor monster;
    Actor whoPlaying;
    KeywordSup keywordSup;
    KeywordMain keywordMain;
    // Start is called before the first frame update
    void Awake()
    {
        if (fightManager != null)
            return;
        fightManager = this;
    }

    private void Start()
    {
        fightManagerUI = FightManagerUI.fightManagerUI;
        Flow();
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

        // ���� ���� ���ʷ� Ű���� ����
        switch(preparedActorCount)
        {
            case 0:
                whoPlaying = monster;
                monster.StartTurn();
                fightManagerUI.ChangeTurnText("����");
                return;
                
            case 1:
                whoPlaying = player;
                player.StartTurn();
                fightManagerUI.ChangeTurnText("�÷��̾�");
                return;

            case 2:
                preparedActorCount = 0;
                break;

        }

        // �÷��̾� ���� Ű���� ����
        player.Action(monster);
        monster.Action(player);
        Flow();
    }
}
