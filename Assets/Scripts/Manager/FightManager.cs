using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager fightManager;

    private int preparedActorCount = 0;

    [Header("플레이어블 오브젝트")]
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
    //    // 플레이어 생성
    //    // 몬스터 생성
    //    // 리스트에 집어넣기

    //}

    public void Flow()
    {
        // 키워드 선택 전, 버프 디버프 적용
        if(preparedActorCount == 0)
        {
            player.BeforeAction();
            monster.BeforeAction();
        }

        // 몬스터부터 차례로 키워드 선택
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

        // 플레이어, 몬스터 순서로 키워드 실행
        player.Action(monster);
        monster.Action(player);
    }
}
