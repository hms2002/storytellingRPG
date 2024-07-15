using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map
{
    public class MapView : MonoBehaviour
    {
        [Tooltip("잠긴 노드 색깔(Locked)")]
        public Color32 lockedColor = Color.gray;
        [Tooltip("방문한 노드 표시(Visited) or 선택 가능 번쩍색깔(Attainable) 노드")]
        public Color32 visitedColor = Color.white;

        private static MapView instance;

        private void Awake()
        {
            instance = this;
        }
    }
}
