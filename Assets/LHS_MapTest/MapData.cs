using Map;
using UnityEngine;
using System.Collections.Generic;

namespace Map
{
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
    public class PlayerData
    {
        public Vector2 mapPosition;

        public PlayerData(Vector2 _mapPosition)
        {
            mapPosition = _mapPosition;
        }
    }
}