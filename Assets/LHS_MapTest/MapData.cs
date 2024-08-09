using Map;
using UnityEngine;
using System.Collections.Generic;

namespace Map
{
    /// <summary>
    /// 저장 데이터들 모음 (저장, 불러오기)
    /// </summary>
    [System.Serializable]
    public class MapData
    {
        public List<NodeData> nodes;
        public List<int> nodesEndLineCheck;
        public Vector2 lastPlayerPosition;  // 마지막 플레이어 위치 추가

        public MapData(List<NodeData> nodes, List<int> nodesEndLineCheck, Vector2 lastPlayerPosition)
        {
            this.nodes = nodes;
            this.nodesEndLineCheck = nodesEndLineCheck;
            this.lastPlayerPosition = lastPlayerPosition;
        }
    }

    [System.Serializable]
    public class NodeData
    {
        public Vector2 position;
        public NodeType nodeType;
        public NodeStates nodeState;
        public List<int> connectedNodeIndices;
    }

    [System.Serializable]
    public class MapMarkData
    {
        public Vector2 mapMarkPosition;

        public MapMarkData(Vector2 _mapMarkPosition)
        {
            mapMarkPosition = _mapMarkPosition;
        }
    }
}