using DG.Tweening;
using Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor.Build;
#endif
using UnityEngine;
using UnityEngine.UI;

public enum CursorType
{
    Nib,
    Eraser
}

/// <summary>
/// 게임 운영에 필요한 UI 관리를 담당
/// <para> 맵 UI, 덱 세팅 UI, 전투 UI 제어 </para>
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [Header("키워드 세팅 윈도우")]
    [SerializeField] private GameObject keywordSettingWindow;       // 키워드 세팅 윈도우 객체

    [Header("전투 기능 및 UI")]
    [SerializeField] private List<GameObject> combatFunctionAndUI;  // 전투 관련 모든 UI를 담는 리스트
    [SerializeField] private GameObject combatKeywordUI;

    [Header("옵션")]
    [SerializeField] private GameObject optionUI;

    [Header("전투 백그라운드")]
    [SerializeField] private GameObject[] combatBackground;           //

    [Header("상점 UI")]
    [SerializeField] private GameObject ShopUI;                     //

    [Header("쉬는 곳 UI")]
    [SerializeField] private List<GameObject> RestUI;
    [Header("쉬는 곳 기능 세팅")]
    [SerializeField] private List<GameObject> RestButton;

    [Header("이벤트 UI")]
    [SerializeField] private List<GameObject> EventUI;

    [Header("섬")]
    [SerializeField] private GameObject island;

    [Header("아이콘")]
    [SerializeField] private GameObject theEndIcon;                 //게임 오버 아이콘

    private bool _isBattleOver = false;                             // 전투 종료 여부
    public bool isBattleOver { get => _isBattleOver; set => _isBattleOver = value; }

    bool isRunDelayShowDamage = false;

    #region 데미지 텍스트 띄우기
    [Header("데미지 텍스트")]
    public GameObject damageTextPrefab;
    public GameObject parentCanvas;
    FloatingDamageInfo[] damageTextArr = new FloatingDamageInfo[20];
    int idxCnt = 0;
    float delay = 0.1f;
    float textShowTime = 0;
    #endregion


    /*==================================================================================================================================*/


    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        if(damageTextPrefab != null && parentCanvas != null)
        {
            for(int i = 0; i < damageTextArr.Length; i++)
            {
                damageTextArr[i] = Instantiate(damageTextPrefab, parentCanvas.transform).GetComponent<FloatingDamageInfo>();
                damageTextArr[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 버튼 클릭 불가 연출을 재생한다.
    /// </summary>
    /// <param name="gameObject">적용시킬 오브젝트를 넘겨준다.</param>
    public void ButtonClicklessFeedback(GameObject gameObject)
    {
        // 버튼 컴포넌트 비활성화
        gameObject.GetComponent<Button>().enabled = false;

        // 0.3초 뒤 버튼 컴포넌트 활성화
        DOVirtual.DelayedCall(0.3f, () => gameObject.GetComponent<Button>().enabled = true);

        // 좌우 횡이동 반복 연출 표현
        gameObject.transform.DOPunchPosition(new Vector3(10, 0, 0), 0.3f, 10, 1);
    }

    /// <summary>
    /// 키워드를 보이지 않게 만든다.
    /// </summary>
    /// <param name="keyword">안보이게 만들 키워드를 넘겨준다.</param>
    public void MakeKeywordInvisible(GameObject keyword)
    {
        // 키워드의 버튼 컴포넌트 비활성화
        keyword.GetComponent<Button>().enabled = false;

        // 키워드의 이미지 컴포넌트 비활성화
        keyword.GetComponent<Image>().enabled = false;

        // 키워드 하위 오브젝트 전체 비활성화
        for (int i = 0; i < keyword.transform.childCount; i++)
        {
            keyword.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 키워드를 보이지 않게 만든다.
    /// </summary>
    /// <param name="keyword">안보이게 만들 키워드를 넘겨준다.</param>
    /// <param name="replaceImage">키워드 박스 대신 교체할 이미지를 넘겨준다.</param>
    public void MakeKeywordInvisible(GameObject keyword, Sprite replaceSprite)
    {
        // 키워드의 버튼 컴포넌트 비활성화
        keyword.GetComponent<Button>().enabled = false;

        // 키워드의 이미지 컴포넌트 교체
        keyword.GetComponent<Image>().sprite = replaceSprite;

        // 키워드 하위 오브젝트 전체 비활성화
        for (int i = 0; i < keyword.transform.childCount; i++)
        {
            keyword.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // UI Active 함수들 ================================

    /// <summary>
    /// 맵 UI 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable"></param>
    public void ActiveMapUI(bool enableOrDisable)
    {
        MapState.InstanceMap.mapObj.SetActive(enableOrDisable);
    }

    /// <summary>
    /// 전투 관련 기능 및 UI 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable">UI 캔버스 SetActive() 여부</param>
    public void ActiveCombatFunctionAndUI(bool enableOrDisable)
    {
        for (int i = 0; i < combatFunctionAndUI.Count; i++)
        {
            combatFunctionAndUI[i].SetActive(enableOrDisable);
        }

        for (int i = 0; i < combatBackground.Length; i++)
        {
            combatBackground[i].SetActive(false);
        }

        if (enableOrDisable == true)
        {
            switch (StageManager.instance.nowStageState)
            {
                case StageState.Forest:
                    combatBackground[0].SetActive(true);
                    break;
                case StageState.Cave:
                    combatBackground[1].SetActive(true);
                    break;
                case StageState.Sea:
                    combatBackground[2].SetActive(true);
                    break;
                case StageState.MagicTower:
                    combatBackground[3].SetActive(true);
                    break;
            }
        }
    }

    /// <summary>
    /// 전투중 키워드 UI 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable">전투 키워드UI SetActive() 여부</param>
    public void ActiveCombatKeywordUI(bool enableOrDisable)
    {
        combatKeywordUI.SetActive(enableOrDisable);
    }

    public void ActiveOptionUI(bool enableOrDisable)
    {
        optionUI.SetActive(enableOrDisable);
    }

    /// <summary>
    /// 키워드 세팅 창 UI 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable">UI 캔버스 SetActive() 여부</param>
    public void ActiveKeywordSettingUI(bool enableOrDisable)
    {
        keywordSettingWindow.SetActive(enableOrDisable);
    }

    /// <summary>
    /// The End 아이콘 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable">아이콘 SetActive() 여부</param>
    public void ActiveTheEndIcon(bool enableOrDisable)
    {
        theEndIcon.SetActive(enableOrDisable);
    }

    /// <summary>
    /// 상점 UI의 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable">상점UI SetActive() 여부</param>
    public void ActiveShopUI(bool enableOrDisable)
    {
        ShopUI.SetActive(enableOrDisable);
    }
    //===================== 휴식 노드 ====================
    /// <summary>
    /// 휴식 노드 UI의 활성화 상태를 일괄 관리한다.
    /// </summary>
    /// <param name="enableOrDisable"></param>
    public void ActiveRestUI(bool enableOrDisable)
    {
        if (enableOrDisable == false)
        {
            string[] endText = { "당신의 몸은 활력을 가득찼다." };
            TextManager.instance.OnlyTextPlay(endText, 1);
            
            ActiveRestButton(enableOrDisable);

            DOVirtual.DelayedCall(2, () =>
            {
                for (int i = 0; i < RestUI.Count; i++)
                {
                    RestUI[i].SetActive(enableOrDisable);
                }

                ActiveCombatFunctionAndUI(enableOrDisable);
                Book.instance.bookAnimator.SetTrigger("turnPageToRight");
                DOVirtual.DelayedCall(1, () => ActiveMapUI(true));
            });

            return;
        }

        for (int i = 0; i < RestUI.Count; i++)
        {
            RestUI[i].SetActive(enableOrDisable);
        }
        
        for(int i=0; i < combatBackground.Length; i++)
        {
            combatBackground[i].SetActive(false);
        }

        if (enableOrDisable == true)
        {
            switch (StageManager.instance.nowStageState)
            {
                case StageState.Forest:
                    combatBackground[0].SetActive(true);
                    break;
                case StageState.Cave:
                    combatBackground[1].SetActive(true);
                    break;
                case StageState.Sea:
                    combatBackground[2].SetActive(true);
                    break;
                case StageState.MagicTower:
                    combatBackground[3].SetActive(true);
                    break;
            }
        }

        string[] restText = { "고된 여정 중 당신은 캠핑하기 좋은 곳을 발견하였습니다.", "당신은...\n\n" };

        DOVirtual.DelayedCall(0, () => TextManager.instance.OnlyTextPlay(restText, 1));
    }

    public void ActiveRestButton(bool enableorDisable)
    {
        for(int i = 0; i< RestButton.Count; i++)
        {
            RestButton[i].SetActive(enableorDisable);
        }
    }

    public void ActiveIsland(bool enableorDisable)
    {
        island.SetActive(enableorDisable);
    }

    //=====================================================

    public void NextStagePage(StageState stageState)
    {

    }

    public void ActiveEventUI(bool enableOrDisable)
    {
        for (int i = 0; i < EventUI.Count; i++)
        {
            EventUI[i].SetActive(enableOrDisable);
        }
        ActiveCombatKeywordUI(enableOrDisable);
        ActiveCombatFunctionAndUI(enableOrDisable);

        for (int i = 0; i < combatBackground.Length; i++)
        {
            combatBackground[i].SetActive(false);
        }

        if (enableOrDisable == true)
        {
            switch (StageManager.instance.nowStageState)
            {
                case StageState.Forest:
                    combatBackground[0].SetActive(true);
                    break;
                case StageState.Cave:
                    combatBackground[1].SetActive(true);
                    break;
                case StageState.Sea:
                    combatBackground[2].SetActive(true);
                    break;
                case StageState.MagicTower:
                    combatBackground[3].SetActive(true);
                    break;
            }
        }
    }

    public void ActiveDamageText(Vector3 pos, int damage, Color color)
    {
        if (damageTextPrefab == null && parentCanvas == null)
        {
            Debug.LogError("피해 텍스트 프리펩 안 넣음");
            return;
        }
        //damageTextArr[idxCnt].gameObject.SetActive(true);
        damageTextArr[idxCnt].transform.position = pos;
        damageTextArr[idxCnt].Init(damage, color);
        //damageTextArr[idxCnt].gameObject.SetActive(false);
        ++idxCnt;
        idxCnt = idxCnt % damageTextArr.Length;
        if(!isRunDelayShowDamage)
            StartCoroutine("DelayShowDamage");
    }

    public void ActiveDamageText(Vector3 pos, string text, Color color)
    {
        if (damageTextPrefab == null && parentCanvas == null)
        {
            Debug.LogError("피해 텍스트 프리펩 안 넣음");
            return;
        }
        //damageTextArr[idxCnt].gameObject.SetActive(true);
        damageTextArr[idxCnt].transform.position = pos;
        damageTextArr[idxCnt].Init(text, color);
        //damageTextArr[idxCnt].gameObject.SetActive(false);
        ++idxCnt;
        idxCnt = idxCnt % damageTextArr.Length;
        if (!isRunDelayShowDamage)
            StartCoroutine("DelayShowDamage");
    }

    IEnumerator DelayShowDamage()
    {
        isRunDelayShowDamage = true;
        bool find = true;
        while(find)
        {
            while (textShowTime + delay > Time.time)
                yield return null;

            find = false;

            for (int i = 0; i < damageTextArr.Length; i++)
            {
                if (damageTextArr[i].isUsing == false) continue;
                if (damageTextArr[i].gameObject.activeSelf == true) continue;
                damageTextArr[i].gameObject.SetActive(true);
                textShowTime = Time.time;

                find = true;
                break;
            }
        }

        isRunDelayShowDamage = false;
    }
};
