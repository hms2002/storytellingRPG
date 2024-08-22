using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RelicData", menuName = "Scriptable object/New Relic Data")]
public class RelicData : ScriptableObject
{
    [Header("유물 티커")]
    [SerializeField] private Relics _relicTicker;
    public Relics relicTicker { get => _relicTicker; }

    [Header("유물 이미지")]
    [SerializeField] private Sprite _relicImage;
    public Sprite relicImage { get => _relicImage; }

    [Header("유물 이름")]
    [SerializeField] private string relicName;
    public string RelicName { get => relicName; }

    [Header("유물 설명")]
    [Multiline(5)]
    [SerializeField] private string relicDescription;
    public string RelicDescription { get => relicDescription; }

    [Header("유물 발동 타입")]
    public RelicType relicType;
    public enum RelicType { OnStartBattle, OnVictory, OnStartTurn }
    
}
