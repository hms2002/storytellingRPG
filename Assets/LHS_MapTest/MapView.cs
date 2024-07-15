using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map
{
    public class MapView : MonoBehaviour
    {
        [Tooltip("��� ��� ����(Locked)")]
        public Color32 lockedColor = Color.gray;
        [Tooltip("�湮�� ��� ǥ��(Visited) or ���� ���� ��½����(Attainable) ���")]
        public Color32 visitedColor = Color.white;

        private static MapView instance;

        private void Awake()
        {
            instance = this;
        }
    }
}
