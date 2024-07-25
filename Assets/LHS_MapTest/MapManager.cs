using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        private string filePath;

        void Start()
        {
            filePath = Application.persistentDataPath + "/mapData.json";

            // �� ������ �ҷ�����
            MapState.InstanceMap.LoadMapData(filePath);
        }
    }
}