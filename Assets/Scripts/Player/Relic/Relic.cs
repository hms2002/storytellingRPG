using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Relic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private RelicData _relicData;
    public RelicData relicData { get => _relicData; set => _relicData = value; }

    private Image relicImage;

    private bool isPerchased = false;


    /*==================================================================================================================================*/


    private void Awake()
    {
        relicImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        relicImage.sprite = relicData.relicImage;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
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

        // PlayerRelic의 AddRelic에 접근하여 추가
        PlayerRelic.instance.AddRelic(gameObject);

        // 유물의 이미지 컴포넌트 비활성화
        //gameObject.GetComponent<Image>().enabled = false;

        // 유물 크기 조정
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);

        // 구매 여부 true
        isPerchased = true;
    }

    /// <summary>
    /// 유물의 효과를 사용합니다.
    /// </summary>
    /// <param name="player">효과를 적용할 Player를 입력합니다.</param>
    /// <param name="Monster">효과를 적용할 Monster를 리스트 형태로 입력합니다.</param>
    public virtual void ApplyEffect(Actor player, List<Monster> Monsters)
    {
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
                    foreach (Monster monster in Monsters) monster.Damaged(monster, new DamageInfo(10));
                }

                break;

            case Relics.PocketOfDesire:

                // 플레이어 소지금 2 증가
                player.gold += 2;

                break;

            case Relics.LuckyCoin:

                int frontOrBack = Random.Range(0, 1);

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
        }
    }
}
