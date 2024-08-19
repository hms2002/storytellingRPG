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
        public Vector2 mapMark;  // 마지막 플레이어 위치 추가
        
        public MapData(List<NodeData> nodes, List<int> nodesEndLineCheck, Vector2 mapMark)
        {
            this.nodes = nodes;
            this.nodesEndLineCheck = nodesEndLineCheck;
            this.mapMark = mapMark;
        }

        public MapData(Vector2 mapMark)
        {
            this.mapMark = mapMark;
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
}