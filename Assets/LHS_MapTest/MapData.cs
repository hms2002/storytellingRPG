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