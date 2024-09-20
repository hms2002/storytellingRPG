using UnityEngine;
using DG.Tweening;
using Map;
using System.IO;
using System.Data.SqlTypes;

public enum StageState
{
    Forest,
    Cave,
    Sea,
    MagicTower,
    Ending
}

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    [SerializeField]
    private StageState _nowStageState;

    [Header("0:숲 │ 1:동굴 │ 2: 바다 │ 3: 마탑")]
    [SerializeField] private GameObject[] mapBackGrounds = new GameObject[3];
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/StageData.json";

        // 싱글톤 인스턴스 설정
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public StageState nowStageState
    {
        get { return _nowStageState; }
        set 
        { 
            _nowStageState = value;
            AudioManager.instance.UpdateBGM();
        }
    }

    private void Start()
    {
        LoadStageState();
    }

    //보스를 잡고 스테이지를 넘어가는 모든 장면 연출.
    public void NextStage()
    {
        DOVirtual.DelayedCall(0, () =>
        {
            GameManager.instance.gameState = GameState.EndBattle;

            // 전투 관련 캔버스 끄기
            UIManager.instance.ActiveCombatFunctionAndUI(false);
            UIManager.instance.ActiveCombatKeywordUI(false);

/*            // 책 페이지 오른쪽으로 넘기도록 트리거 발동
            Book.instance.bookAnimator.SetTrigger("turnPageToRight");
*/
            // 2초 뒤 맵 북마크로 이동
            DOVirtual.DelayedCall(2.0f, Book.instance.EnterMap);

            GameManager.instance.eventIndex = 0;
            switch (nowStageState)
            {
                case StageState.Forest:
                    nowStageState = StageState.Cave;
                    break;
                case StageState.Cave:
                    nowStageState = StageState.Sea;
                    break;
                case StageState.Sea:
                    nowStageState = StageState.MagicTower;
                    break;
                case StageState.MagicTower:
                    nowStageState = StageState.Ending;
                    GameManager.instance.LoadScene(2);
                    break;
                case StageState.Ending:
                    break;
            }

            AudioManager.instance.UpdateBGM();

            GameManager.instance.FullRecovery();

            MonsterSetDatabase.monsterSetDatabase.MonsterSetting();
            BackgroundSetting(nowStageState);
            MapState.InstanceMap.SpawnMap();
           // MapState.InstanceMap.MapMarkReBatch();
        });
    }

    //맵 배경 변경 함수
    public void BackgroundSetting(StageState stageState)
    {
        for (int i = 0; i < mapBackGrounds.Length; i++)
        {
            mapBackGrounds[i].SetActive(false);
        }

        switch (stageState)
        {
            case StageState.Forest:
                mapBackGrounds[0].SetActive(true);
                break;
            case StageState.Sea:
                mapBackGrounds[1].SetActive(true);
                break;
            case StageState.Cave:
                mapBackGrounds[2].SetActive(true);
                break;
            case StageState.MagicTower:
                mapBackGrounds[3].SetActive(true);
                break;
            default:
                Debug.LogWarning("스테이지 변경 오류");
                break;
        }

        SaveStageState();
    }

    public void SaveStageState()
    {
        StageState stageState = nowStageState;
        string jsonData = JsonUtility.ToJson(stageState);
        File.WriteAllText(saveFilePath, jsonData);
    }

    public void LoadStageState()
    {
        if (File.Exists(saveFilePath))
        {
            string jsonData = File.ReadAllText(saveFilePath);
            nowStageState = JsonUtility.FromJson<StageState>(jsonData);
            Debug.Log("StageState 로드 완료: " + nowStageState);

            // 로드된 상태에 맞게 배경 설정
            BackgroundSetting(nowStageState);
        }
        else
        {
            Debug.Log("StageData 파일이 존재하지 않음. 기본 상태로 설정.");
            nowStageState = StageState.Forest; // 기본 스테이지 설정
            BackgroundSetting(nowStageState);
        }
    }

    public void StageReset()
    {
        string jsonData = JsonUtility.ToJson(StageState.Cave);
        nowStageState = StageState.Forest;
        File.WriteAllText(saveFilePath, jsonData);

        BackgroundSetting(StageState.Forest);
    }
}