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

        //�� ���� ����
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

        //�� Ŭ���� ����
        public void MapRenewal()
        {
            // nodesEndLineCheck ����Ʈ�� ��� �ִ��� Ȯ��
            if (nodesEndLineCheck == null || nodesEndLineCheck.Count == 0)
            {
                Debug.Log(nodesEndLineCheck[0]);
                Debug.LogError("nodesEndLineCheck ����Ʈ�� ��� �ֽ��ϴ�.");
                return;
            }

            // nodes ����Ʈ�� ��� �ִ��� Ȯ��
            if (nodes == null || nodes.Count == 0)
            {
                Debug.LogError("nodes ����Ʈ�� ��� �ֽ��ϴ�.");
                return;
            }

            for (int i = 0; i <= nodesEndLineCheck[nodesEndLineCheck.Count - 1]; i++)
            {
                // i�� nodes ����Ʈ�� ������ ����� �ʵ��� üũ
                if (i >= nodes.Count)
                {
                    Debug.LogError($"i({i})�� nodes ����Ʈ�� ������ ������ϴ�.");
                    break;
                }

                if (nodes[i].nodeStates == NodeStates.Attainable)
                {
                    for (int j = 0; j < nodesEndLineCheck.Count; j++)
                    {
                        if (i <= nodesEndLineCheck[j])
                        {
                            // ���� ���� �����ϴ� ���
                            for (int k = (j == 0 ? 0 : nodesEndLineCheck[j - 1] + 1); k <= nodesEndLineCheck[j]; k++)
                            {
                                // k�� nodes ����Ʈ�� ������ ����� �ʵ��� üũ
                                if (k >= nodes.Count)
                                {
                                    Debug.LogError($"k({k})�� nodes ����Ʈ�� ������ ������ϴ�.");
                                    break;
                                }
                                nodes[k].nodeStates = NodeStates.Locked;
                            }

                            // ���� ���� �����ϴ� ���
                            if (j + 1 < nodesEndLineCheck.Count)
                            {
                                for (int k = nodesEndLineCheck[j] + 1; k <= nodesEndLineCheck[j + 1]; k++)
                                {
                                    // k�� nodes ����Ʈ�� ������ ����� �ʵ��� üũ
                                    if (k >= nodes.Count)
                                    {
                                        Debug.LogError($"k({k})�� nodes ����Ʈ�� ������ ������ϴ�.");
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