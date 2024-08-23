using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Relics
{
    /// <summary> 등가교환
    /// <para>전투 시작 시 체력을 3 잃지만, 강화 효과를 3 얻습니다.</para>
    /// </summary>
    EquivalentExchange,

    /// <summary> 이판사판
    /// <para>5턴마다 자신과 적에게 10의 피해를 줍니다.</para>
    /// </summary>
    DoOrDie,

    /// <summary> 욕망 주머니
    /// <para>매 턴마다 소지금이 2 증가합니다.</para>
    /// </summary>
    PocketOfDesire,

    /// <summary> 행운의 동전
    /// <para>매 턴마다 무작위로 강화 효과를 2 얻거나 취약 효과를 2 얻습니다.</para>
    /// </summary>
    LuckyCoin,

    /// <summary> 광전사의 투구
    /// <para>체력이 40 이하라면 매 턴마다 강화 효과를 2 얻습니다.</para>
    /// </summary>
    BerserkerHelmet,

    /// <summary>작은 병
    /// <para>전투 시작 시 체력을 4 회복합니다.</para>
    /// </summary>
    SmallBottle,

    /// <summary>넘쳐흐른 주머니
    /// <para>매 턴마다 소지금 100G당 일회성 강화를 얻습니다.</para>
    /// </summary>
    OverflowingPocket,

    /// <summary>깨진 거울 파편
    /// <para>전투 시작 시 유리 파편 효과를 2 얻습니다.</para>
    /// </summary>
    BrokenMirrorFragment,

    /// <summary>흡수하는 손거울
    /// <para>몬스터에게 피해를 주었을 때 체력을 2 회복합니다.</para>
    /// </summary>
    AbsorbingHandMirror,

    /// <summary>부의 비밀
    /// <para>전투 승리 시 100 골드를 추가 획득합니다.</para>
    /// </summary>
    SecretOfWealth,

    /// <summary>전사의 심장
    /// <para>전투 시작 시 일회성 강화 효과를 3 획득합니다.</para>
    /// </summary>
    HeartOfWarrior,

    /// <summary>보석 코어
    /// <para>전투 시작 시 자신에게 보호 효과를 5 부여합니다.</para>
    /// </summary>
    JewelryCore,

    /// <summary>발화하는 주사위
    /// <para>적에게 두 턴마다 화염 효과를 1 ~ 4 랜덤 부여합니다.</para>
    /// </summary>
    IgnitingDice,

    /// <summary>불타는 장미
    /// <para>전투 시작 시 화염 효과를 4 부여합니다.</para>
    /// </summary>
    BurningRose,

    /// <summary>열정적인 영혼
    /// <para>대상의 화염 수치 5마다 매 턴 강화 효과 1을 얻습니다.</para>
    /// </summary>
    PassionateSoul
}

public class RelicDatabase : MonoBehaviour
{
    public static RelicDatabase instance;

    [Header("유물 데이터 스크립터블 오브젝트")]
    public List<RelicData> relicsData;


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
        DontDestroyOnLoad(this.gameObject);
    }
}
