using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Relics
{
    EquivalentExchange,
    DoOrDie,
    PocketOfDesire,
    LuckyCoin,
    BerserkerHelmet,
    SmallBottle,
    OverflowingPocket,
    BrokenMirrorFragment,
    AbsorbingHandMirror,
    SecretOfWealth,
    HeartOfWarrior,
    JewelryCore,
    IgnitingDice,
    BurningRose,
    PassionateSoul
}

[CreateAssetMenu(fileName = "RelicData", menuName = "Scriptable object/Relic Data")]
public class RelicData : ScriptableObject
{
    [Header("유물")]
    [SerializeField] private Relics relic;

    [Header("유물 이미지")]
    [SerializeField] private Sprite relicImage;

    [Header("유물 이름")]
    [SerializeField] private string relicName;
    public string RelicName { get => relicName; }

    [Header("유물 설명")]
    [Multiline(5)]
    [SerializeField] private string relicDescription;
}
