using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public enum NodeStates
    {
        Locked,
        Visited,
        Attainable
    }
}

namespace Map
{
    public class MapNode : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer render;
        [SerializeField]
        private Image image;
        [SerializeField]
        private NodeBlueprint nodeBlueprint;

        public void SetUp(NodeBlueprint _nodeBlueprint)
        {
            nodeBlueprint = _nodeBlueprint;

            if(render != null) render.sprite = nodeBlueprint.sprite;
            if(render != null) image.sprite = nodeBlueprint.sprite;
        }
    }
}