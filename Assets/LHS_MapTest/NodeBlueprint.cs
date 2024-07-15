using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

namespace Map
{
    public enum NodeType
    {
        NomalEnemy,
        BossEnemy,
        RestSite,
        Mystery,
        Shop
    }
}

namespace Map
{
    internal static class NodeTypeHelper
    {
        public static readonly int nodeTypeCount = ((NodeType[])System.Enum.GetValues(typeof(NodeType))).Length;
    }
}

namespace Map
{
    public class NodeBlueprint : ScriptableObject
    {
        [Header("노드타입에 따른 이미지 넣어야함")]
        [Tooltip("순서 0 : NomalEnemy, 1: BossEnemy, RestSite, Mystery, Shop. 개수 현재 : 5개")]
        public Sprite sprite;
        public NodeType nodeType;
    }
}

