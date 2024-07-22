using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapState : MapGenerator
    {
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
        }

        //맵 시작 세팅
        private void MapStateSetting()
        {
            for (int i = 0; i <= nodesEndLineCheck[0]; i++)
            {
                nodes[i].nodeStates = NodeStates.Attainable;
            }

            for (int i = nodesEndLineCheck[0]+1; i <= nodesEndLineCheck[nodesEndLineCheck.Count - 1]; i++)
            {
                nodes[i].nodeStates = NodeStates.Locked;
            }
        }

        //맵 클릭시 갱신
        public void MapRenewal()
        {
            // nodesEndLineCheck 리스트가 비어 있는지 확인
            if (nodesEndLineCheck == null || nodesEndLineCheck.Count == 0)
            {
                Debug.Log(nodesEndLineCheck[0]);
                Debug.LogError("nodesEndLineCheck 리스트가 비어 있습니다.");
                return;
            }

            // nodes 리스트가 비어 있는지 확인
            if (nodes == null || nodes.Count == 0)
            {
                Debug.LogError("nodes 리스트가 비어 있습니다.");
                return;
            }

            for (int i = 0; i <= nodesEndLineCheck[nodesEndLineCheck.Count - 1]; i++)
            {
                // i가 nodes 리스트의 범위를 벗어나지 않도록 체크
                if (i >= nodes.Count)
                {
                    Debug.LogError($"i({i})가 nodes 리스트의 범위를 벗어났습니다.");
                    break;
                }

                if (nodes[i].nodeStates == NodeStates.Attainable)
                {
                    for (int j = 0; j < nodesEndLineCheck.Count; j++)
                    {
                        if (i <= nodesEndLineCheck[j])
                        {
                            // 같은 열에 존재하는 노드
                            for (int k = (j == 0 ? 0 : nodesEndLineCheck[j - 1] + 1); k <= nodesEndLineCheck[j]; k++)
                            {
                                // k가 nodes 리스트의 범위를 벗어나지 않도록 체크
                                if (k >= nodes.Count)
                                {
                                    Debug.LogError($"k({k})가 nodes 리스트의 범위를 벗어났습니다.");
                                    break;
                                }
                                nodes[k].nodeStates = NodeStates.Locked;
                            }

                            // 다음 열에 존재하는 노드
                            if (j + 1 < nodesEndLineCheck.Count)
                            {
                                for (int k = nodesEndLineCheck[j] + 1; k <= nodesEndLineCheck[j + 1]; k++)
                                {
                                    // k가 nodes 리스트의 범위를 벗어나지 않도록 체크
                                    if (k >= nodes.Count)
                                    {
                                        Debug.LogError($"k({k})가 nodes 리스트의 범위를 벗어났습니다.");
                                        break;
                                    }
                                    nodes[k].nodeStates = NodeStates.Attainable;
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void SaveAllNodeState()
        {

        }
    }
}