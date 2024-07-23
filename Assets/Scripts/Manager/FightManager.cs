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
        //if (!_keywordSup.ActivationConditionCheck())

        whoPlaying.GetKeywordSup(_keywordSup);
    }

    public void GetKeywordMain(KeywordMain _keywordMain)
    {
        if (_keywordMain == null) return;
        whoPlaying.GetKeywordMain(_keywordMain);
        preparedActorCount++;
        Invoke("Flow",2);
    }

    void FightStart()
    {
        monsterList = MonsterSetDatabase.monsterSetDatabase.GetSet4();
        MonsterTargetter.monsterTargetter.target = monsterList[0];
        float pos = 4.45f;
        foreach(Actor monster in monsterList)
        {
            monster.transform.position = new Vector3(pos, 0.42f, 0);
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

        StartCoroutine(ActorAction());
        //player.Action(MonsterTargetter.monsterTargetter.target);

        //foreach (Actor monster in monsterList)
        //    monster.Action(player);

        //Flow();
    }

    IEnumerator ActorAction()
    {
        player.Action(MonsterTargetter.monsterTargetter.target);
        fightManagerUI.ChangeActionText("플레이어 ");

        int dir = 3;
        Vector3 originPos = player.transform.position;
        Vector3 objectPos = player.transform.position + new Vector3(dir, 0, 0);
        const float ACTION_TIME = 0.2f;
        float curTime = 0;

        while (curTime < ACTION_TIME)
        {
            curTime += Time.deltaTime;
            player.transform.position = Vector3.Lerp(originPos, objectPos, curTime / ACTION_TIME);
            yield return null;
        }

        AudioManager.instance.PlaySound("Character", player.attackSound);

        curTime = 0;
        while (curTime < ACTION_TIME)
        {
            curTime += Time.deltaTime;
            player.transform.position = Vector3.Lerp(objectPos, originPos, curTime / ACTION_TIME);
            yield return null;
        }
        yield return new WaitForSeconds(2);

        foreach (Actor monster in monsterList)
        {
            monster.Action(player);
            fightManagerUI.ChangeActionText("몬스터 : " + monsterList[preparedActorCount].gameObject.name);
            dir = -3;
            originPos = monster.transform.position;
            objectPos = monster.transform.position + new Vector3(dir, 0, 0);
            curTime = 0;

            while (curTime < ACTION_TIME)
            {
                curTime += Time.deltaTime;
                monster.transform.position = new Vector3(10, 10, 0);
                monster.transform.position = Vector3.Lerp(originPos, objectPos, curTime / ACTION_TIME);
                yield return null;
            }

            AudioManager.instance.PlaySound("Character", monster.attackSound);


            curTime = 0;
            while (curTime < ACTION_TIME)
            {
                curTime += Time.deltaTime;
                monster.transform.position = Vector3.Lerp(objectPos, originPos, curTime / ACTION_TIME);
                yield return null;
            }
            yield return new WaitForSeconds(2);
        }

        Flow();
    }
}


