using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Relic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private RelicData _relicData;
    public RelicData relicData { get => _relicData; set => _relicData = value; }

    private Image relicImage;

    private int _price = 0;
    public int price { get => _price; set => _price = value; }

    [Header("유물 가격표")]
    [SerializeField] private GameObject priceTag;

    private bool _isPerchased = false;
    public bool isPerchased { get => _isPerchased; set => _isPerchased = value; }


    /*==================================================================================================================================*/


    private void Awake()
    {
        relicImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        relicImage.sprite = relicData.relicImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoManager.instance.ShowTipUI(relicData.RelicName, Color.black, relicData.RelicDescription, transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoManager.instance.HideTipUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 게임 상태가 Shop이 아니거나 구매한 상태라면 반환
        if (GameManager.instance.gameState != GameState.Shop || isPerchased) return;

        if (ShopManager.instance.player.gold < price)
        {
            // 좌우 횡이동 반복 연출 표현
            gameObject.transform.DOPunchPosition(new Vector3(10, 0, 0), 0.3f, 10, 1);

            return;
        }

        ShopManager.instance.UpdateGoldHUD(price * -1);
        Destroy(priceTag);

        AudioManager.instance.PlaySound("Shop", "동전");

        // PlayerRelic의 AddRelic에 접근하여 추가
        PlayerRelic.instance.AddRelic(gameObject);

        // 유물의 이미지 컴포넌트 비활성화
        gameObject.GetComponent<Image>().enabled = false;

        // 구매 여부 true
        isPerchased = true;
    }

    /// <summary>
    /// 유물의 효과를 사용합니다.
    /// </summary>
    /// <param name="player">효과를 적용할 Player를 입력합니다.</param>
    /// <param name="monsters">효과를 적용할 Monster를 리스트 형태로 입력합니다.</param>
    public virtual void ApplyEffect(Actor player, List<Monster> monsters)
    {
        int totalQuantity = 0;      // 부여 스택 연산용 변수
        int pickOne = 0;


        switch (relicData.relicTicker)
        {
            case Relics.EquivalentExchange:

                // 플레이어에게 3 데미지
                player.Damaged(player, new DamageInfo(3));

                // 플레이어에게 강화 3 부여
                player.charactorState.AddState(StateType.reinforce, 3);

                break;

            case Relics.DoOrDie:

                // 5턴째라면
                if (FightManager.currentTurn % 5 == 0)
                {
                    // 플레이어에게 10 데미지
                    player.Damaged(player, new DamageInfo(10));

                    // 몬스터 전체에게 10 데미지
                    foreach (Monster monster in monsters) monster.Damaged(monster, new DamageInfo(10));
                }

                break;

            case Relics.PocketOfDesire:

                // 플레이어 소지금 2 증가
                player.gold += 2;

                break;

            case Relics.LuckyCoin:

                int frontOrBack = Random.Range(0, 2);

                if (frontOrBack == 0) player.charactorState.AddState(StateType.reinforce, 2);
                else if (frontOrBack == 1) player.charactorState.AddState(StateType.weaken, 2);

                break;

            case Relics.BerserkerHelmet:

                // 
                if (player.hp <= 40) player.charactorState.AddState(StateType.reinforce, 2);

                break;

            case Relics.SmallBottle:

                player.hp += 4;

                break;

            case Relics.OverflowingPocket:

                if (player.gold >= 100)
                    player.charactorState.AddState(StateType.oneTimeReinforce, player.gold / 100);

                break;

            case Relics.BrokenMirrorFragment:

                player.hp += 2;

                break;

            case Relics.SecretOfWealth:

                player.gold += 100;

                break;

            case Relics.HeartOfWarrior:

                player.charactorState.AddState(StateType.oneTimeReinforce, 3);

                break;

            case Relics.JewelryCore:
                
                player.protect += 5;

                break;

            case Relics.IgnitingDice:

                if (FightManager.currentTurn % 2 == 0)
                {
                    int howMany;

                    foreach (Monster monster in monsters)
                    {
                        howMany = Random.Range(1, 5);

                        monster.charactorState.AddState(StateType.burn, howMany);
                    }
                }

                break;

            case Relics.BurningRose:

                foreach (Monster monster in monsters)
                {
                    monster.charactorState.AddState(StateType.burn, 4);
                }

                break;

            case Relics.PassionateSoul:

                totalQuantity = 0;

                foreach (Monster monster in monsters)
                {
                    totalQuantity += monster.charactorState.GetStateStack(StateType.burn) / 5;
                }

                if(totalQuantity > 0)
                {
                    player.charactorState.AddState(StateType.oneTimeReinforce, totalQuantity);
                }

                break;

            case Relics.BrokenBlade:

                player.charactorState.AddState(StateType.oneTimeReinforce, 1);

                break;

            case Relics.IndomitableHeart:

                totalQuantity = 0;

                totalQuantity += player.charactorState.BuffCount();
                totalQuantity += player.charactorState.AllDebuffCount();

                if (totalQuantity > 0)
                {
                    player.charactorState.AddState(StateType.oneTimeReinforce, totalQuantity);
                }
                break;

            case Relics.BlueWish:

                player.protect += 11;

                break;

            case Relics.ImprintedRing:

                if (FightManager.currentTurn % 2 == 0 && player.protect >= 10)
                {
                    player.protect -= 2;

                    foreach (Monster monster in monsters) monster.Damaged(monster, new DamageInfo(6));
                }

                break;

            case Relics.Anguish:

                if (player.protect >= 12)
                {
                    player.charactorState.AddState(StateType.oneTimeReinforce, 3);
                }

                break;

            case Relics.GreenJelly:

                foreach (Monster monster in monsters) monster.charactorState.AddState(StateType.venom, 3);

                break;

            case Relics.WeaknessMagnifier:

                foreach (Monster monster in monsters) monster.charactorState.AddState(StateType.weaken, 3);

                break;

            case Relics.RefractivePolyhedron:

                if (FightManager.currentTurn % 2 == 0)
                {
                    player.charactorState.AddState(StateType.oneTimeProtect, 3);
                }

                break;

            case Relics.HeartOfTheSea:

                pickOne = Random.Range(1, 4);

                if (FightManager.currentTurn % 3 == 0)
                {
                    switch (pickOne)
                    {
                        case 1:

                            player.protect += 2;

                            break;

                        case 2:

                            player.charactorState.AddState(StateType.oneTimeReinforce, 2);

                            break;

                        case 3:

                            player.charactorState.AddState(StateType.counterAttack, 2);

                            break;
                    }
                }

                break;
        }
    }
}
