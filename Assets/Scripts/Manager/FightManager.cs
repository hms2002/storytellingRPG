using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager fightManager;
    public FightManagerUI fightManagerUI;

    [Header("Actor 오브젝트")]
    [SerializeField] private Actor player;
    [SerializeField] private List<Actor> monsterList;

    private int preparedActorCount = 0;

    private Actor whoPlaying;
    private KeywordSup keywordSup;
    private KeywordMain keywordMain;


    /*==================================================================================================================================*/


    void Awake()
    {
        // 싱글톤 구조 보강
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
        FightStart();
        Flow();
    }

    public void GetKeywordSup(KeywordSup _keywordSup)
    {
        if (_keywordSup == null) return;
        if (!_keywordSup.ActivationConditionCheck())

        whoPlaying.GetKeywordSup(_keywordSup);
    }

    public void GetKeywordMain(KeywordMain _keywordMain)
    {
        if (_keywordMain == null) return;
        whoPlaying.GetKeywordMain(_keywordMain);
        preparedActorCount++;
        Flow();
    }

    void FightStart()
    {
        monsterList = MonsterSetDatabase.monsterSetDatabase.GetSet3();
        MonsterTargetter.monsterTargetter.target = monsterList[0];
        int pos = 5;
        foreach(Actor monster in monsterList)
        {
            monster.transform.position = new Vector3(pos, 0.07f, 0);
            pos -= 2;
        }
    }

    public void Flow()
    {
        // 
        if (preparedActorCount == 0)
        {
            player.BeforeAction();
            foreach (Actor monster in monsterList)
                monster.BeforeAction();
        }

        // 
        if (preparedActorCount < monsterList.Count)
        {
            whoPlaying = monsterList[preparedActorCount];
            monsterList[preparedActorCount].StartTurn();
            monsterList[preparedActorCount].SelectKeyword();
            fightManagerUI.ChangeTurnText("몬스터 : " + monsterList[preparedActorCount].gameObject.name);
            return;
        }
        else if (preparedActorCount == monsterList.Count)
        {
            whoPlaying = player;
            player.StartTurn();
            player.SelectKeyword();
            fightManagerUI.ChangeTurnText("플레이어");
            return;
        }
        else
            preparedActorCount = 0;

        player.Action(MonsterTargetter.monsterTargetter.target);

        foreach (Actor monster in monsterList)
            monster.Action(player);

        Flow();
    }
}
