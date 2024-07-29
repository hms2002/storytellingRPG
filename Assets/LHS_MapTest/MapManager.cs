using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        private string filePath;

        void Start()
        {
            filePath = Application.persistentDataPath + "/mapData.json";

            // 맵 데이터 불러오기
            MapState.InstanceMap.LoadMapData(filePath);
        }
    }
}