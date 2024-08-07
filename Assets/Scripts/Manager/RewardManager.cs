using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public  Actor player;

    public static RewardManager instance;
    public GameObject rewardCanvas;

    public List<Transform> rewordPivot;
    List<GameObject> btnList = new List<GameObject>();

    // 정보를 채워넣을 껍데기
    [SerializeField] GameObject rewardOffset_keyword;
    
    [SerializeField] GameObject rewardOffset_relic;
    
    [SerializeField] GameObject rewardOffset_gold;
    
    [SerializeField] GameObject rewardOffset_None;

    bool _isMonsterFlee = false;
    public bool isMonsterFlee
    {
        get { return _isMonsterFlee; }
        set { _isMonsterFlee = value; }
    }


    int rewardCnt = 0;

    bool _dropRelic = false;
    bool dropRelic
    {
        get { return _dropRelic; }
        set { _dropRelic = value; }
    }

    int _rewardGold = 0;
    public int rewardGold 
    { get { return _rewardGold; }
        set { _rewardGold = value; }
    }

    [Header("키워드 프리펩 넣으면 보상으로 나오게 됨")]
    [SerializeField] List<GameObject> rewardKeywords;
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
        rewardCanvas.SetActive(false);
    }

    // 아직은 단순히 리스트의 0~2번째 소스를 보상으로 만들었음. 랜덤으로 바꿔야 함
    public void  ShowRewards_Keyword()
    {
        if (isMonsterFlee)
        {
            ShowRewards_Relic_Gold();
            return;
        }

        // 보상 UI 띄우기
        rewardCanvas.SetActive(true);
        // 보상 버튼 생성
        GameObject rewardInstance1 = Instantiate(rewardOffset_keyword, rewordPivot[0].transform.position, Quaternion.identity, rewardCanvas.transform);
        // 보상 데이터 채우기
        rewardInstance1.GetComponent<Reward>().SettingReward_Keyword(rewardKeywords[0]);
        // 반복
        GameObject rewardInstance2 = Instantiate(rewardOffset_keyword, rewordPivot[1].transform.position, Quaternion.identity, rewardCanvas.transform);
        rewardInstance2.GetComponent<Reward>().SettingReward_Keyword(rewardKeywords[1]);
        
        GameObject rewardInstance3 = Instantiate(rewardOffset_keyword, rewordPivot[2].transform.position, Quaternion.identity, rewardCanvas.transform);
        rewardInstance3.GetComponent<Reward>().SettingReward_Keyword(rewardKeywords[2]);
        
        btnList.Add(rewardInstance1);
        btnList.Add(rewardInstance2);
        btnList.Add(rewardInstance3);
    }
    public void ShowRewards_Relic_Gold()
    {
        // 보상 UI 띄우기
        rewardCanvas.SetActive(true);

        if (_rewardGold > 0)
            rewardCnt++;
        if(dropRelic)
            rewardCnt++;
        if(rewardCnt == 0)
            ShowNoReward();

        if (dropRelic)
        {
            GameObject rewardInstance_relic
                = Instantiate(rewardOffset_relic, rewordPivot[0].transform.position, Quaternion.identity, rewardCanvas.transform);
            rewardInstance_relic.GetComponent<Reward>().SettingReward_Relic();

            btnList.Add(rewardInstance_relic);

            GameObject rewardInstance_gold
                = Instantiate(rewardOffset_gold, rewordPivot[2].transform.position, Quaternion.identity, rewardCanvas.transform);
            rewardInstance_gold.GetComponent<Reward>().SettingReward_Gold(rewardInstance_gold, rewardGold);

            btnList.Add(rewardInstance_gold);
        }
        else
        {
            GameObject rewardInstance_gold
                = Instantiate(rewardOffset_gold, rewordPivot[1].transform.position, Quaternion.identity, rewardCanvas.transform);
            rewardInstance_gold.GetComponent<Reward>().SettingReward_Gold(rewardInstance_gold, rewardGold);
            btnList.Add(rewardInstance_gold);
        }
    }
    
    private void ShowNoReward()
    {
        rewardCanvas.SetActive(true);
        GameObject rewardInstance_none
    = Instantiate(rewardOffset_None, rewordPivot[1].transform.position, Quaternion.identity, rewardCanvas.transform);
        btnList.Add(rewardInstance_none);
    }

    public void AddMainKeywordToDeck(GameObject _keywordmain)
    {
        player.AddMainKeywordToOriginalDeck(_keywordmain);
        rewardCanvas.SetActive(false);
        foreach (GameObject g in btnList)
            Destroy(g);
        btnList.Clear();
        ShowRewards_Relic_Gold();
    }

    public void AddSupKeywordToDeck(GameObject _keywordSup)
    {
        player.AddSupKeywordToOriginalDeck(_keywordSup);
        rewardCanvas.SetActive(false);
        foreach (GameObject g in btnList)
            Destroy(g);
        btnList.Clear();
        ShowRewards_Relic_Gold();
        //GameManager.instance.EndSelectReward();
    }
    public void AddGoldToPlayer()
    {
        player.gold += rewardGold;
        rewardGold = 0;
        rewardCnt--;
        if (rewardCnt == 0)
        {
            rewardCanvas.SetActive(false);
            GameManager.instance.EndSelectReward();
            foreach (GameObject g in btnList)
                Destroy(g);
            btnList.Clear();
        }
    }
    public void AddRelicToPlayer()
    {

        dropRelic = false;
        rewardCnt--;
        if (rewardCnt == 0)
        {
            rewardCanvas.SetActive(false);
            GameManager.instance.EndSelectReward();
            foreach (GameObject g in btnList)
                Destroy(g);
            btnList.Clear();
        }
    }

    public void ClickNoReward()
    {
        rewardCanvas.SetActive(false);
        GameManager.instance.EndSelectReward();
        foreach (GameObject g in btnList)
            Destroy(g);
        btnList.Clear();
    }
}
