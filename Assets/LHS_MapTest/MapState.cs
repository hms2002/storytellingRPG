using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Map
{
    public class MapState : MapGenerator
    {
        [Header("맵 전체(지도 페이지 포함")]
        public GameObject mapObj;
        [Header("지도 내 플레이어 마크")]
        public GameObject mapMark;  // 플레이어 마크 추가

        [Header("현재 움직여도 되나요?")]
        public bool noClickNode = false;

        private static MapState instance;
        public static MapState InstanceMap
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<MapState>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject("MapState");
                        instance = obj.AddComponent<MapState>();
                    }
                }
                return instance;
            }
        }

        //맵 생성
        override public void SpawnMap()
        {
            base.SpawnMap();
            MapStateSetting();
            
            // 맵 생성 후 저장
            SaveMapData(Application.persistentDataPath + "/mapData.json");

            //맵에서 플레이어 위치 저장파일 삭제 
            if (File.Exists(Application.persistentDataPath + "/mapMarkData.json"))
            {
                File.Delete(Application.persistentDataPath + "/mapMarkData.json");
                mapMark.GetComponent<RectTransform>().anchoredPosition = startNode.GetComponent<RectTransform>().anchoredPosition;
            }
        }

        //맵 상태 세팅
        private void MapStateSetting()
        {
            for (int i = 0; i <= nodesEndLineCheck[0]; i++)
            {
                nodes[i].nodeStates = NodeStates.Attainable;
            }

            for (int i = nodesEndLineCheck[0] + 1; i <= nodesEndLineCheck[nodesEndLineCheck.Count - 1]; i++)
            {
                nodes[i].nodeStates = NodeStates.Locked;
            }
        }

        //맵 저장
        public void SaveMapData(string filePath)
        {
            List<NodeData> nodeDataList = new List<NodeData>();

            for (int i = 0; i < nodes.Count; i++)
            {
                MapNode mapNode = nodes[i];
                NodeData nodeData = new NodeData
                {
                    position = mapNode.GetComponent<RectTransform>().anchoredPosition,
                    nodeType = mapNode.nodeBlueprint.nodeType,
                    nodeState = mapNode.nodeStates,
                    connectedNodeIndices = new List<int>()
                };

                foreach (var connectedNode in mapNode.connectedNodes)
                {
                    nodeData.connectedNodeIndices.Add(nodes.IndexOf(connectedNode));
                }

                nodeDataList.Add(nodeData);
            }

            MapData mapData = new MapData(nodeDataList, new List<int>(nodesEndLineCheck), mapMark.GetComponent<RectTransform>().anchoredPosition);
            string json = JsonUtility.ToJson(mapData, true);
            File.WriteAllText(filePath, json);
            Debug.Log("Map data saved to " + filePath);
        }

        //저장된 맵 로드
        public void LoadMapData(string filePath)
        {
            startNode.SetActive(true);
            endNode.SetActive(true);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                MapData mapData = JsonUtility.FromJson<MapData>(json);

                ClearExistingNodes();
                nodesEndLineCheck = new List<int>(mapData.nodesEndLineCheck); // nodesEndLineCheck 리스트 로드

                for (int i = 0; i < mapData.nodes.Count; i++)
                {
                    var nodeData = mapData.nodes[i];

                    GameObject nodeObject = Instantiate(nodePrefab[(int)nodeData.nodeType], mapParent);
                    RectTransform rectTransform = nodeObject.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = nodeData.position;

                    MapNode mapNode = nodeObject.GetComponent<MapNode>();
                    mapNode.nodeBlueprint = ScriptableObject.CreateInstance<NodeBlueprint>();
                    mapNode.nodeBlueprint.nodeType = nodeData.nodeType;
                    mapNode.nodeBlueprint.sprite = nodePrefab[(int)nodeData.nodeType].GetComponent<MapNode>().nodeBlueprint.sprite;
                    mapNode.SetUp();
                    mapNode.nodeStates = nodeData.nodeState;
                    mapNode.SetStage();

                    nodes.Add(mapNode);
                }

                Debug.Log("nodesEndLineCheck: " + string.Join(", ", nodesEndLineCheck)); // 디버그 메시지

                for (int i = 0; i < mapData.nodes.Count; i++)
                {
                    foreach (var connectedNodeIndex in mapData.nodes[i].connectedNodeIndices)
                    {
                        if (connectedNodeIndex >= 0 && connectedNodeIndex < nodes.Count)
                        {
                            nodes[i].connectedNodes.Add(nodes[connectedNodeIndex]);
                            ConnectRoadLine(nodes[i].GetComponent<RectTransform>(), nodes[connectedNodeIndex].GetComponent<RectTransform>());
                        }
                        else
                        {
                            Debug.LogWarning("노드 연결 오류: " + connectedNodeIndex);
                        }
                    }
                }

                // 플레이어 마크 위치 복원
                mapMark.GetComponent<RectTransform>().anchoredPosition = mapData.lastPlayerPosition;

                // 시작 노드와 끝 노드를 연결
                StartEndConnection();

                Debug.Log("맵 불러오기 : " + filePath);
            }
            else
            {
                Debug.LogError("저장 파일 없음 : " + filePath);
            }
        }

        //맵 속 플레이어 위치 저장
        public void SavePlayerMarkPosition(string filePath)
        {
            MapMarkData mapMarkData = new MapMarkData(mapMark.GetComponent<RectTransform>().anchoredPosition);
            string json = JsonUtility.ToJson(mapMarkData);
            File.WriteAllText(filePath, json);
        }
    }
}