using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RewardManager : MonoBehaviour
{
    public  Actor player;

    public static RewardManager instance;
    public GameObject rewardCanvas;

    public List<Transform> rewordPivot;
    public Transform treasurePivot;
    List<GameObject> btnList = new List<GameObject>();

    // 정보를 채워넣을 껍데기
    [SerializeField] GameObject rewardOffset_keyword;
    
    [SerializeField] GameObject rewardOffset_relic;
    
    [SerializeField] GameObject rewardOffset_gold;
    
    [SerializeField] GameObject rewardOffset_None;

    delegate void AfterClickKeyword();
    AfterClickKeyword afterClickKeywordDel;

    bool _isMonsterFlee = false;
    public bool isMonsterFlee
    {
        get { return _isMonsterFlee; }
        set { _isMonsterFlee = value; }
    }


    int rewardCnt = 0;

    [SerializeField] private bool _dropRelic = false;
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
    [Header("보물상자 키워드 프리펩 넣으면 보상으로 나오게 됨")]
    [SerializeField] List<GameObject> treasureRewardKeywords;
    [Header("보물상자 유물 프리펩 넣으면 보상으로 나오게 됨")]
    [SerializeField] List<GameObject> treasureRewardRelics;
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
        rewardCanvas.SetActive(false);
    }
    public void MakeTreasures()
    {
        // 보상 UI 띄우기
        rewardCanvas.SetActive(true);

        int keywordCounts = Random.Range(0, 4);
        int relicCounts = 3 - keywordCounts;

        for(int i = 0; i < keywordCounts; i++)
        {
            GameObject rewardInstance = Instantiate(rewardOffset_keyword, rewardCanvas.transform);
            rewardInstance.GetComponent<Reward>().SettingReward_Keyword(treasureRewardKeywords[0]);
            treasureRewardKeywords.Add(treasureRewardKeywords[0]);
            treasureRewardKeywords.RemoveAt(0);
            btnList.Add(rewardInstance);
        }
        for (int i = 0; i < relicCounts; i++)
        {
            GameObject rewardInstance = Instantiate(rewardOffset_keyword, rewardCanvas.transform);
            rewardInstance.GetComponent<Reward>().SettingReward_Keyword(treasureRewardKeywords[0]);
            treasureRewardKeywords.Add(treasureRewardKeywords[0]);
            treasureRewardKeywords.RemoveAt(0);
            btnList.Add(rewardInstance);
        }

        for(int i = 0; i < btnList.Count; i++)
        {
            btnList[i].transform.position = treasurePivot.position;
            btnList[i].transform.DOMove(rewordPivot[i].position, 1);
        }

        afterClickKeywordDel = () =>
        {
            StartCoroutine("PlayText_GetItem");
        };
    }
    string getItemName;
    IEnumerator PlayText_GetItem()
    {
        rewardCanvas.SetActive(false);
        
        float time = 3f;
        string line = "당신은 " + getItemName + " 을 손에 넣었다.";
        yield return TextManager.instance.Text.DOText(line, time).WaitForCompletion();

        // 종료

        Book.instance.EnterMap();
        player.gameObject.SetActive(false);
        foreach (GameObject g in btnList)
            Destroy(g);
        btnList.Clear();
    }
    // 아직은 단순히 리스트의 0~2번째 소스를 보상으로 만들었음. 랜덤으로 바꿔야 함
    public void  ShowFightRewards_Keyword()
    {
        if (isMonsterFlee)
        {
            ShowFightRewards_Relic_Gold();
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

        afterClickKeywordDel = ShowFightRewards_Relic_Gold;
    }
    public void ShowFightRewards_Relic_Gold()
    {
        // 보상 UI 띄우기
        rewardCanvas.SetActive(true);

        if (_rewardGold > 0)
            rewardCnt++;
        if(dropRelic)
            rewardCnt++;
        if(rewardCnt == 0)
            ShowNoReward();

        if (dropRelic) ShowFightRelic();
        
        ShowFightGold();
    }

    private void ShowFightRelic()
    {
        GameObject rewardInstance_relic = MakeRelicButton();

        btnList.Add(rewardInstance_relic);
    }
    private void ShowFightGold()
    {
        GameObject rewardInstance_gold
            = Instantiate(rewardOffset_gold, rewordPivot[2].transform.position, Quaternion.identity, rewardCanvas.transform);
        rewardInstance_gold.GetComponent<Reward>().SettingReward_Gold(rewardInstance_gold, rewardGold);

        btnList.Add(rewardInstance_gold);
    }
    private void ShowNoReward()
    {
        rewardCanvas.SetActive(true);
        GameObject rewardInstance_none
    = Instantiate(rewardOffset_None, rewordPivot[1].transform.position, Quaternion.identity, rewardCanvas.transform);
        btnList.Add(rewardInstance_none);
    }

    #region 버튼 눌렀을 때
    public void AddMainKeywordToDeck(GameObject _keywordmain)
    {
        GameObject temp = Instantiate(_keywordmain);
        getItemName = temp.GetComponent<Keyword>().nameText.text;
        Destroy(temp);
        player.AddMainKeywordToOriginalDeck(_keywordmain);
        rewardCanvas.SetActive(false);
        foreach (GameObject g in btnList)
            Destroy(g);
        btnList.Clear();
        afterClickKeywordDel();
    }

    public void AddSupKeywordToDeck(GameObject _keywordSup)
    {
        GameObject temp = Instantiate(_keywordSup);
        getItemName = temp.GetComponent<Keyword>().nameText.text;
        Destroy(temp);

        player.AddSupKeywordToOriginalDeck(_keywordSup);
        rewardCanvas.SetActive(false);
        foreach (GameObject g in btnList)
            Destroy(g);
        btnList.Clear();
        afterClickKeywordDel();
    }
    public void AddGoldToPlayer()
    {
        player.gold += rewardGold;
        rewardGold = 0;
        rewardCnt--;
        if (rewardCnt <= 0)
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

    /// <summary>
    /// 유물 보상 버튼을 인스턴스화합니다.
    /// </summary>
    /// <returns></returns>
    public GameObject MakeRelicButton()
    {
        GameObject rewardInstance_relic = Instantiate(rewardOffset_relic, rewordPivot[0].transform.position, Quaternion.identity, rewardCanvas.transform);
        rewardInstance_relic.GetComponent<Reward>().SettingReward_Relic(RelicManager.instance.GetRandomRelic(), player.GetComponent<PlayerRelic>());

        return rewardInstance_relic;
    }

    public void ClickNoReward()
    {
        rewardCanvas.SetActive(false);
        GameManager.instance.EndSelectReward();
        foreach (GameObject g in btnList)
            Destroy(g);
        btnList.Clear();
        GameManager.instance.ReturnMap();
    }
    #endregion
}
