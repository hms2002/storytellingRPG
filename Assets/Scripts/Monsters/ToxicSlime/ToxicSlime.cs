using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicSlime : Actor
{
    [Header("유독성 슬라임 제원 제어")]
    [SerializeField] private const int _addictionDamage = 5;            // 중독 스택 당 데미지
    [SerializeField] private const int _maxAddictionStack = 8;          // 최대 중독 스택 중첩량
    [SerializeField] private const int _tensionPerAddictionStack = 5;   // 중독 스택 당 긴장도 증가 수치

    #region 유독성 슬라임 제원 변수 프로퍼티
    public int addictionDamage { get { return _addictionDamage; } }
    public int maxAddictionStack { get { return _maxAddictionStack; } }
    public int tensionPerAddictionStack { get { return _tensionPerAddictionStack; } }
    #endregion


    /*==================================================================================================================================*/


    private void Start()
    {
        MAX_HP = 70;
        hp = MAX_HP;
    }
};
