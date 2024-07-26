using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Map
{
    public class MapState : MapGenerator
    {
        public GameObject mapObj;

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

        override public void SpawnMap()
        {
            base.SpawnMap();
            MapStateSetting();
            SaveMapData(Application.persistentDataPath + "/mapData.json"); // �� ���� �� ����
        }

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

        public void SaveMapData(string filePath)
        {
            MapData mapData = new MapData
            {
                nodes = new List<NodeData>(),
                nodesEndLineCheck = new List<int>(nodesEndLineCheck) // nodesEndLineCheck ����Ʈ ����
            };

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

                mapData.nodes.Add(nodeData);
            }

            string json = JsonUtility.ToJson(mapData, true);
            File.WriteAllText(filePath, json);
            Debug.Log("Map data saved to " + filePath);
        }

        public void LoadMapData(string filePath)
        {
            startNode.SetActive(true);
            endNode.SetActive(true);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                MapData mapData = JsonUtility.FromJson<MapData>(json);

                ClearExistingNodes();
                nodesEndLineCheck = new List<int>(mapData.nodesEndLineCheck); // nodesEndLineCheck ����Ʈ �ε�

                for (int i = 0; i < mapData.nodes.Count; i++)
                {
                    var nodeData = mapData.nodes[i];

                    GameObject nodeObject = Instantiate(nodePrefab[(int)nodeData.nodeType], mapParent);
                    RectTransform rectTransform = nodeObject.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = nodeData.position;

                    MapNode mapNode = nodeObject.GetComponent<MapNode>();
                    mapNode.nodeBlueprint = new NodeBlueprint { nodeType = nodeData.nodeType };
                    mapNode.nodeBlueprint.sprite = nodePrefab[(int)nodeData.nodeType].GetComponent<MapNode>().nodeBlueprint.sprite;
                    mapNode.SetUp();
                    mapNode.nodeStates = nodeData.nodeState;
                    mapNode.SetStage();

                    nodes.Add(mapNode);
                }

                Debug.Log("nodesEndLineCheck: " + string.Join(", ", nodesEndLineCheck)); // ����� �޽���

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
                            Debug.LogWarning("��� ���� ����: " + connectedNodeIndex);
                        }
                    }
                }

                // ���� ���� �� ��带 ����
                StartEndConnection();

                Debug.Log("�� �ҷ����� : " + filePath);
            }
            else
            {
                Debug.LogError("���� ���� ���� : " + filePath);
            }
        }

        public void OnOffMap()
        {
            if(mapObj.activeSelf)
            {
                CloseMap();
            }
            else
            {
                OpenMap();
            }
        }

        public void OpenMap()
        {
            mapObj.SetActive(true);
        }

        public void CloseMap()
        {
            mapObj.SetActive(false);
        }
    }
}