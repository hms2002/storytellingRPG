using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    int preparedActorCount = 0;
    public Actor player;
    public Actor monster;
    Actor whoPlaying;
    KeywordSup keywordSup;
    KeywordMain keywordMain;
    // Start is called before the first frame update
    void Start()
    {
        Flow();
    }

    // Update is called once per frame
    void Update()
    {

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

        // 몬스터 부터 차례로 키워드 선택
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

        // 플레이어 부터 키워드 실행
        player.Action(monster);
        monster.Action(player);
        Flow();
    }
}
