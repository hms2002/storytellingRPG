using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reward : MonoBehaviour
{
    public GameObject keywordPrefab;
    public TextMeshProUGUI rewardNameText;
    public Button button;
    public void SettingReward(GameObject _keywordPrefab)
    {
        keywordPrefab = _keywordPrefab;

        // 정보를 가져오기 위해 인스턴스화
        GameObject temp = Instantiate(keywordPrefab);
        temp.SetActive(false);

        // 텍스트 정보, 키워드 프리펩 정보 가져오기
        rewardNameText.text = temp.transform.GetComponentInChildren<TextMeshProUGUI>().text;
        
            // 메인 키워드면 AddThisToMainDeck()를 본인 버튼 이벤트에 추가
        if (temp.GetComponent<KeywordMain>() != null)
            button.onClick.AddListener(AddThisToMainDeck);
            // 서포트 키워드면 AddThisToSupDeck()를 본인 버튼 이벤트에 추가
        else if (temp.GetComponent<KeywordSup>() != null)
            button.onClick.AddListener(AddThisToSupDeck);
        
        // 정보 취하고 삭제
        Destroy(temp);
    }
    public void AddThisToMainDeck()
    {
        RewardManager.instance.AddMainKeywordToDeck(keywordPrefab);
    }
    public void AddThisToSupDeck()
    {
        RewardManager.instance.AddSupKeywordToDeck(keywordPrefab);
    }
}
