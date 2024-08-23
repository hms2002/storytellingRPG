using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using static TreeEditor.TreeEditorHelper;
#endif
namespace Map
{
    /// <summary>
    /// 노드 상태
    /// </summary>
    public enum NodeStates
    {
        Locked,
        Visited,
        Attainable
    }

    /// <summary>
    /// 노드 타입
    /// </summary>
    public enum NodeType
    {
        NomalMonsterNode,
        EliteMonsterNode,
        RestNode,
        StoreNode,
        TreasureNode,
        EventNode,
        BossNode,
        None
    }
}

/*
namespace Map
{
    internal static class NodeTypeHelper
    {
        public static readonly int nodeTypeCount = ((NodeType[])System.Enum.GetValues(typeof(NodeType))).Length;
    }
}
*/

namespace Map
{
    public class NodeBlueprint : ScriptableObject
    {
        [Header("노드 타입 설정")]
        public NodeType nodeType;
        [Header("노드 이미지 설정")]
        public Sprite sprite;
    }
}

