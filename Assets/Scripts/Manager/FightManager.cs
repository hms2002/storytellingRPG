using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager fightManager { get; private set; }
    public FightManagerUI fightManagerUI;

    [Header("Actor 오브젝트")]
    [SerializeField] private Actor player;
    [SerializeField] private Actor monster;

    private int preparedActorCount = 0;

    private Actor whoPlaying;
    private KeywordSup keywordSup;
    private KeywordMain keywordMain;


    /*==================================================================================================================================*/


    void Awake()
    {
        if (fightManager != null && fightManager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        fightManager = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        fightManagerUI = FightManagerUI.fightManagerUI;
        Flow();
    }

    public void GetKeywordSup(KeywordSup _keywordSup)
    {
        if (_keywordSup == null) return;

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
        // 
        if(preparedActorCount == 0)
        {
            player.BeforeAction();
            monster.BeforeAction();
        }

        // 
        switch(preparedActorCount)
        {
            case 0:
                whoPlaying = monster;
                monster.StartTurn();
                fightManagerUI.ChangeTurnText("몬스터");
                return;
                
            case 1:
                whoPlaying = player;
                player.StartTurn();
                fightManagerUI.ChangeTurnText("플레이어");
                return;

            case 2:
                preparedActorCount = 0;
                break;

        }

        // 
        player.Action(monster);
        monster.Action(player);

        Flow();
    }
}
