using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum DamageType
{
    Burn,
    Beat
}

public class Actor : MonoBehaviour
{
    public ActorStateUIControler stateUIController;
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    [Header("덱 매니저 오브젝트")]
    [SerializeField]
    public GameObject deck;
    public GameObject supCanvas;
    public GameObject mainCanvas;

    KeywordSup keywordSup;
    KeywordMain keywordMain;

    private List<GameObject> supportHand = new List<GameObject>();
    private List<GameObject> mainHand = new List<GameObject>();

    [Header("손 패 사이즈")]
    [SerializeField]
    private int handSize = 3;
    private bool hasActorDrawnKeywords = false; // 액터가 모든 키워드를 다 드로우했는가

    #region 캐릭터 능력치 관련 변수, 함수
    private const int _MAX_HP = 100;
    private int _hp = 100;

    public int MAX_HP
    {
        get { return _MAX_HP; }
    }
    public int hp
    {
        get { return _hp; }
        set { _hp = value; }
    }
    public int _protect = 0;
    public int protect
    {
        get { return _protect; }
        set 
        { 
            _protect = value;
            stateUIController.ProtectOn(_protect);
        }
    }

    private int _pike = 0;
    public int pike
    {
        get { return _pike; }
        set { _pike = value; }
    }

    private int _burnStack = 0;
    public int burnStack
    {
        get { return _burnStack; }
        set
        {
            _burnStack = value;
            stateUIController.BurnOn(_burnStack);
        }
    }
    private int _weakenStack = 0;
    public int weakenStack
    {
        get { return _weakenStack; }
        set
        {
            _weakenStack = value;
            stateUIController.WeakenOn(_weakenStack);
            Debug.Log("취약 스택" + _weakenStack);
        }
    }
    private int _reductionStack = 0;
    public int reductionStack
    {
        get { return _reductionStack; }
        set
        {
            _reductionStack = value;
            stateUIController.ReductionOn(_reductionStack);
        }
    }
    private int _additionalStack = 0;
    public int additionalStack
    {
        get { return _additionalStack; }
        set
        {
            _additionalStack = value;
            stateUIController.ReductionOn(_additionalStack);
        }
    }

    #endregion
    public bool AttackCount = false;

    public virtual void BeforeAction()
    {
        if (hasActorDrawnKeywords == false) // 액터가 키워드를 안 뽑았다면
        {
            for (int i = 0; i < handSize; i++) // 키워드 드로우 3번 반복
            {
                supportHand.Add(deck.GetComponent<Deck>().DrawSupportKeyword()); // 서포트 키워드 덱에서 1장 랜덤 드로우
                mainHand.Add(deck.GetComponent<Deck>().DrawMainKeyword()); // 서포트 키워드 덱에서 1장 랜덤 드로우
            }

            hasActorDrawnKeywords = true;
        } // 액터가 키워드를 모두 소진했을 시 hasActorDrawnKeywords = false; 해줘야 함

        if (burnStack > 0)
        {
            Damaged(burnStack * 2, DamageType.Burn);
            burnStack -= 1;
        }
    }

    internal void GetKeywordSup(KeywordSup _keywordSup)
    {
        keywordSup = _keywordSup;
        ShowKeywordMain();
    }

    internal void GetKeywordMain(KeywordMain _keywordMain)
    {
        keywordMain = _keywordMain;
        mainKeywords.SetActive(false);
    }

    internal void Action(Actor target)
    {
        Sentence sentence = new Sentence();

        keywordSup.Check(keywordMain);
        keywordMain.Check(keywordSup);

        protect = 0;

        keywordSup.Execute(this, target, sentence);
        keywordMain.Execute(this, target, sentence);
        sentence.execute(this, target);
    }

    public virtual void Damaged(int _damage, DamageType _type)
    {
        if (_damage <= 0)
            return;
        int totalDamage = _damage;
        switch(_type)
        {
            case DamageType.Burn:
                Debug.Log(gameObject.name + "화염 피해" + _damage);
                break;

            case DamageType.Beat:
                Debug.Log(gameObject.name + "타격 피해" + _damage);
                if (totalDamage > 0)
                {
                    AttackCount = true;
                }
                if (weakenStack > 0)
                {
                    totalDamage += weakenStack;
                    weakenStack -= 1;
                }
                if(reductionStack > 0)
                {
                    if(totalDamage < reductionStack)
                    {
                        totalDamage = 0;
                        reductionStack -= 1;
                    }
                    else
                    {
                        totalDamage -= reductionStack;
                        reductionStack -= 1;
                    }
                }
                break;
        }
        if (protect > 0)
        {
            if (protect < totalDamage)
            {
                totalDamage -= protect;
                protect = 0;
            }
            else
            {
                protect -= totalDamage;
                totalDamage = 0;
            }
        }
        hp -= totalDamage;

        // 체력 UI 조정
        hpSlider.value = hp/(float)MAX_HP;
        hpText.text = hp + " / " + MAX_HP;
    }

    public void StartTurn()
    {
        ShowKeywordSup();
    }

    private void ShowKeywordMain()
    {
        supCanvas.SetActive(false);
        mainCanvas.SetActive(true);

        for (int i = 0; i < handSize; i++)
        {
            supportHand[i].SetActive(true);
        }
    }

    private void ShowKeywordSup()
    {
        supCanvas.SetActive(true);

        for (int i = 0; i < handSize; i++)
        {
            supportHand[i].SetActive(true);
        }
    }
}
