using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relic : MonoBehaviour
{
    [SerializeField] private RelicData relicData;
    public RelicData RelicData { get => relicData; }


    /*==================================================================================================================================*/


    /// <summary>
    /// 유물의 효과를 적용합니다.
    /// </summary>
    /// <param name="player">효과를 적용할 Player를 입력합니다.</param>
    /// <param name="Monster">효과를 적용할 Monster를 리스트 형태로 입력합니다.</param>
    public virtual void ApplyEffect(Actor player, List<Monster> Monster) { }
}
