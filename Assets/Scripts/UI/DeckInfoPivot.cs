using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeckInfoPivot : MonoBehaviour
{
    private List<GameObject> _deckInfo = new List<GameObject>();
    public IReadOnlyList<GameObject> deckInfo => _deckInfo;


    //=============================================================================================================


    /// <summary>
    /// DeckInfoPivot 내부의 deckInfo 리스트에 키워드 프리팹 정보를 받아온다.
    /// </summary>
    /// <param name="deckPrefabList">덱 프리팹 리스트</param>
    public void RecieveDeckInfo(IReadOnlyList<GameObject> deckPrefabList)
    {
        for (int i = 0; i < deckPrefabList.Count; i++)
        {
            _deckInfo.Add(deckPrefabList[i]);
        }
    }

    /// <summary>
    /// 턴이 종료되면 deckInfo 리스트를 초기화한다.
    /// </summary>
    public void ClearDeckInfo()
    {
        _deckInfo = new List<GameObject>();
    }
}
