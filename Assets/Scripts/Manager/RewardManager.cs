using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public  Actor player;
    //public Actor player
    //{
    //    get
    //    {
    //        if (_player == null)
    //        {
    //            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
    //        }
    //        return _player;
    //    }
    //}

    public static RewardManager instance;
    public GameObject rewardCanvas;

    public List<Transform> rewordPivot;

    // 정보를 채워넣을 껍데기
    [SerializeField] GameObject rewardOffset;

    [Header("키워드 프리펩 넣으면 보상으로 나오게 됨")]
    [SerializeField] List<GameObject> rewardKeywords;
    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
        rewardCanvas.SetActive(false);
    }

    // 아직은 단순히 리스트의 0~2번째 소스를 보상으로 만들었음. 랜덤으로 바꿔야 함
    public void  ShowRewards()
    {
        // 보상 UI 띄우기
        rewardCanvas.SetActive(true);
        // 보상 버튼 생성
        GameObject rewardInstance1 = Instantiate(rewardOffset, rewordPivot[0].transform.position, Quaternion.identity, rewardCanvas.transform);
        // 보상 데이터 채우기
        rewardInstance1.GetComponent<Reward>().SettingReward(rewardKeywords[0]);
        // 반복
        GameObject rewardInstance2 = Instantiate(rewardOffset, rewordPivot[1].transform.position, Quaternion.identity, rewardCanvas.transform);
        rewardInstance2.GetComponent<Reward>().SettingReward(rewardKeywords[1]);
        
        GameObject rewardInstance3 = Instantiate(rewardOffset, rewordPivot[2].transform.position, Quaternion.identity, rewardCanvas.transform);
        rewardInstance3.GetComponent<Reward>().SettingReward(rewardKeywords[2]);
    }

    public void AddMainKeywordToDeck(GameObject _keywordmain)
    {
        player.AddMainKeywordToOriginalDeck(_keywordmain);
        rewardCanvas.SetActive(false);
        GameManager.instance.EndSelectReward();
    }

    public void AddSupKeywordToDeck(GameObject _keywordSup)
    {
        player.AddSupKeywordToOriginalDeck(_keywordSup);
        rewardCanvas.SetActive(false);
        GameManager.instance.EndSelectReward();
    }
}
