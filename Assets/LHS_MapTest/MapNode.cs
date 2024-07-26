using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Map
{
    public enum NodeStates
    {
        Locked,
        Visited,
        Attainable
    }

    public class MapNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        public NodeStates nodeStates;
        [SerializeField]
        public NodeBlueprint nodeBlueprint;
        [SerializeField]
        private bool isEndNode = false;  // endNode 플래그 추가

        [SerializeField]
        private float initialScale; // 아이콘 기본 크기
        [SerializeField]
        private float HoveScaleFactor = 1.2f; // (마우스 가져다 됐을 시 아이콘 확대 배율 

        //연결되는 노드
        public List<MapNode> connectedNodes = new List<MapNode>();

        //임시 start
        private void Start()
        {
            SetUp();
            SetStage();
        }

        public void SetUp()
        {
            if (nodeBlueprint != null)
            {
                image.sprite = nodeBlueprint.sprite;
            }
        }

        public void SetStage()
        {
            switch (nodeStates)
            {
                case NodeStates.Locked: //잠김
                    image.DOKill();
                    image.color = Color.gray;
                    break;

                case NodeStates.Attainable: // 선택가능
                    image.DOKill();
                    image.DOColor(Color.white, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                    break;

                case NodeStates.Visited: // 방문함
                    image.DOKill();
                    image.color = Color.white;
                    break;
            }
        }

        //마우스 관련 코드
        public void OnPointerEnter(PointerEventData eventData)
        {
            //마우스가 오브젝트에 들어왔을 때 코드
            image.transform.DOKill();
            image.transform.DOScale(initialScale * HoveScaleFactor, 0.3f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //마우스가 오브젝트에 벗어났을 때 코드
            image.transform.DOKill();
            image.transform.DOScale(initialScale, 0.3f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //마우스 버튼이 오브젝트를 눌렀을 때 코드
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //마우스 버튼이 오브젝트에 누르고 땠을 때 코드
            if (nodeStates == NodeStates.Attainable)
            {
                nodeStates = NodeStates.Visited;
                SetStage();

                if (isEndNode)
                {
                    // 특정 로직 실행
                    ExecuteEndNodeLogic();
                }
                else
                {
                    UpdateAllNodes();
                }

                // 노드 상태 변경 후 맵 저장
                MapState.InstanceMap.SaveMapData(Application.persistentDataPath + "/mapData.json");
            }
        }

        private void UpdateAllNodes()
        {
            // MapState의 인스턴스에서 모든 노드들을 가져옴
            var allNodes = MapState.InstanceMap.nodes;
            var endNode = MapState.InstanceMap.endNode;  // 끝 노드를 가져옴

            // 클릭된 노드를 제외한 모든 노드를 Locked 상태로 변경
            foreach (var node in allNodes)
            {
                if (node.nodeStates == NodeStates.Attainable)
                {
                    node.nodeStates = NodeStates.Locked;
                    node.SetStage();
                }
            }

            // 연결된 노드를 Attainable 상태로 변경
            foreach (var connectedNode in connectedNodes)
            {
                if (connectedNode.nodeStates == NodeStates.Locked)
                {
                    connectedNode.nodeStates = NodeStates.Attainable;
                    connectedNode.SetStage();
                }
            }

            // 현재 노드가 마지막 열에 속하는지 확인하여 끝 노드를 Attainable 상태로 변경
            if (connectedNodes.Count == 0)
            {
                endNode.GetComponent<MapNode>().nodeStates = NodeStates.Attainable;
                endNode.GetComponent<MapNode>().SetStage();
            }
        }

        private void ExecuteEndNodeLogic()
        {
            // EndNode 클릭 시 실행할 로직
            Debug.Log("EndNode 클릭됨 - 특정 로직 실행");
        }
    }
}