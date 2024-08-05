using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 전투 기능 및 흐름을 담당
/// <para> player 데이터, monster 리스트 데이터, 키워드 제어 </para>
/// </summary>
public class FightManager : MonoBehaviour
{
    public static FightManager fightManager;
    public FightManagerUI fightManagerUI;

    [Header("Actor 오브젝트")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Actor player;
    [SerializeField] private List<Monster> monsterList;

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
    }

    public void GetKeywordSup(KeywordSup _keywordSup)
    {
        if (_keywordSup == null) return;
        
        //if (!_keywordSup.ActivationConditionCheck())
        if (whoPlaying == player)
            _keywordSup.CanUseCheck(player, MonsterTargetter.monsterTargetter.target);
        else if (whoPlaying != player)
            _keywordSup.CanUseCheck(whoPlaying, player);
        if (!_keywordSup.isCanUse)
        {
            _keywordSup.CantUseEffect();
            return;
        }
        whoPlaying.GetKeywordSup(_keywordSup);
        _keywordSup.PlayClickSound();
    }

    public void GetKeywordMain(KeywordMain _keywordMain)
    {
        if (_keywordMain == null) return;
        whoPlaying.GetKeywordMain(_keywordMain);
        if (whoPlaying == player)
            _keywordMain.CanUseCheck(player, MonsterTargetter.monsterTargetter.target);
        else if (whoPlaying != player)
            _keywordMain.CanUseCheck(whoPlaying, player);

        //if (!_keywordMain.isCanUse)
        //{
        //    _keywordMain.CantUseEffect();
        //    return;
        //}

        _keywordMain.PlayClickSound();
        preparedActorCount++;
        Invoke("Flow",2);
    }

    public void FightStart()
    {
        // 플레이어 프리팹 생성 및 Actor 할당
        //player = Instantiate(playerPrefab).GetComponent<Actor>();
        player.gameObject.SetActive(true);

        // 몬스터 가져오기
        monsterList = MonsterSetDatabase.monsterSetDatabase.GetSelectedSet();
        if (monsterList == null) Debug.LogError("몬스터 리스트 NULL 리턴");
        MonsterTargetter.monsterTargetter.target = monsterList[0];
        RePositionMonsters();
        TextManager.instance.EncounterTextPlay(monsterList[monsterList.Count - 1]);

        DOVirtual.DelayedCall(5f, () => UIManager.instance.ActiveCombatKeywordUI(true));
        DOVirtual.DelayedCall(5f, Flow);
    }
    /// <summary>
    /// 몬스터 월드 포지션 위치 재정렬
    /// </summary>
    private void RePositionMonsters()
    {
        float pos = 3.87f;
        foreach (Actor monster in monsterList)
        {
            monster.transform.position = new Vector3(pos, -1.17f, 0);
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

        if (!CheckMonsterSurvive())
        {
            // 전투 승리 문구 출력
            PlayerWin();
            MonsterTargetter.monsterTargetter.TargetUIOff();
            return;
        }

        // 
        if (preparedActorCount < monsterList.Count)
        {
            whoPlaying = monsterList[preparedActorCount];
            monsterList[preparedActorCount].StartTurn();
            monsterList[preparedActorCount].ShowSupKeywords();
            return;
        }
        else if (preparedActorCount == monsterList.Count)
        {
            whoPlaying = player;
            player.StartTurn();
            player.ShowSupKeywords();

            TextManager.instance.KeywordTextPlay(player);
            
            return;
        }
        else
            preparedActorCount = 0;

        StartCoroutine(ActorAction());
    }

    private IEnumerator ActorAction()
    {
        player.Action(MonsterTargetter.monsterTargetter.target);

        int dir = 3;
        Vector3 originPos = player.transform.position;
        Vector3 objectPos = player.transform.position + new Vector3(dir, 0, 0);
        const float ACTION_TIME = 0.2f;
        float curTime = 0;
        
        while (curTime < ACTION_TIME)
        {
            curTime += Time.deltaTime;
            if (player.damage != 0)
            {
                player.transform.position = Vector3.Lerp(originPos, objectPos, curTime / ACTION_TIME);
            }
            TextManager.instance.MainKeywordTextPlay(player, 0.5f);

            yield return null;
        }
        if (player.damage != 0)
        {
            AudioManager.instance.PlaySound("Character", player.attackSound);
        }

        curTime = 0;
        while (curTime < ACTION_TIME)
        {
            curTime += Time.deltaTime;
            if (player.damage != 0)
            {
                player.transform.position = Vector3.Lerp(objectPos, originPos, curTime / ACTION_TIME);
            }
            yield return null;
        }
        yield return new WaitForSeconds(2);

        // 남은 몬스터 있는지 확인.
        if(!CheckMonsterSurvive())
        {
            // 전투 승리 문구 출력
            PlayerWin();

            yield break;
        }

        foreach (Actor monster in monsterList)
        {
            monster.Action(player);
            dir = -3;
            originPos = monster.transform.position;
            objectPos = monster.transform.position + new Vector3(dir, 0, 0);
            curTime = 0;

            while (curTime < ACTION_TIME)
            {
                curTime += Time.deltaTime;
                if (monster.damage != 0)
                {
                    monster.transform.position = Vector3.Lerp(originPos, objectPos, curTime / ACTION_TIME);
                }
                TextManager.instance.MainKeywordTextPlay(monster, 0.5f);

                yield return null;
            }

            if (monster.damage != 0)
            {
                AudioManager.instance.PlaySound("Character", monster.attackSound);
            }

            curTime = 0;
            while (curTime < ACTION_TIME)
            {
                curTime += Time.deltaTime;
                if (monster.damage != 0)
                {
                    monster.transform.position = Vector3.Lerp(objectPos, originPos, curTime / ACTION_TIME);
                }
                yield return null;
            }
            yield return new WaitForSeconds(2);
        }

        CheckPlayerSurvive();
    }

    private bool CheckMonsterSurvive()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (monsterList[i].hp <= 0)
            {
                monsterList[i].DestroySelf();
                monsterList.RemoveAt(i);
                MonsterTargetter.monsterTargetter.ReAimTarget(monsterList);
            }
        }

        if (monsterList.Count == 0)
        {
            return false;
        }
        else
            return true;
    }

    private void CheckPlayerSurvive()
    {
        if (player.hp <= 0 || TensionManager.tensionManagerUI.tension <= 0)
        {
            // 플레이어 사망 애니메이션 재생

            // 플레이어 사망 문구 출력
            TextManager.instance.PrintPlayerDie();

            // The End 아이콘 활성화
            UIManager.instance.ActiveTheEndIcon(true);

            // 게임 종료 (씬 전환?)
        }
        else
            Flow();
    }

    private void PlayerWin()
    {
        player.gameObject.SetActive(false);
        TextManager.instance.PrintVictory();
        GameManager.instance.WinFight();
    }

    public void AddMonster(Monster monster)
    {
        monsterList.Add(monster);
        RePositionMonsters();
    }
}


