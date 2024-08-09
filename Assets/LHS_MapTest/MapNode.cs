using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Map
{
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
        //마우스가 오브젝트에 들어왔을 때
        public void OnPointerEnter(PointerEventData eventData)
        {
            image.transform.DOKill();
            image.transform.DOScale(initialScale * HoveScaleFactor, 0.3f);
        }

        //마우스가 오브젝트에 벗어났을 때
        public void OnPointerExit(PointerEventData eventData)
        {
            //마우스가 오브젝트에 벗어났을 때 코드
            image.transform.DOKill();
            image.transform.DOScale(initialScale, 0.3f);
        }

        //마우스 버튼이 오브젝트를 눌렀을 때
        public void OnPointerDown(PointerEventData eventData)
        {
            //마우스 버튼이 오브젝트를 눌렀을 때 코드
        }

        //마우스를 클릭하고 뗐을 때.
        public void OnPointerUp(PointerEventData eventData)
        {
            //마우스 버튼이 오브젝트에 누르고 땠을 때 코드
            if (MapState.InstanceMap.noMoveNode == true)
            {
                return;
            }

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
            

            switch(nodeBlueprint.nodeType)
            {
                case NodeType.NomalMonsterNode:
                    MonsterSetDatabase.monsterSetDatabase.SettingSelectedSet(1, NodeType.NomalMonsterNode);
                    GameManager.instance.EnterFightZone();
                    UIManager.instance.ActiveMapUI(false);
                    break;

                case NodeType.EliteMonsterNode:
                    break;

                case NodeType.BossNode:
                    MonsterSetDatabase.monsterSetDatabase.SettingSelectedSet(1, NodeType.BossNode);
                    GameManager.instance.EnterFightZone();
                    UIManager.instance.ActiveMapUI(false);
                    break;

                case NodeType.RestNode:
                    break;

                case NodeType.StoreNode:
                    GameManager.instance.EnterShop();
                    break;

                case NodeType.TreasureNode:
                    break;
            }

                // 노드 상태 변경 후 플레이어 이동 및 위치 저장
                MovePlayerToNode();
            }
            else
            {
                Debug.Log("노드가 이동 가능하지 않음: " + nodeStates);
            }

            //맵 저장 및 노드 표시 저장
            MapState.InstanceMap.SaveMapData(Application.persistentDataPath + "/mapData.json");
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

        //마지막 보스 버튼 코드
        private void ExecuteEndNodeLogic()
        {
            // EndNode 클릭 시 실행할 로직
            Debug.Log("EndNode 클릭됨 - 특정 로직 실행");
        }

        private void MovePlayerToNode()
        {
            MapPlayerMove playerMove = FindObjectOfType<MapPlayerMove>();
            if (playerMove != null)
            {
                // 플레이어 이동 시작 전에 위치 저장
                Debug.Log("노드로 플레이어 이동 시작: " + this.GetComponent<RectTransform>().anchoredPosition);
                playerMove.MovePlayerMark(this.GetComponent<RectTransform>().anchoredPosition);
            }
            else
            {
                Debug.LogWarning("MapPlayerMove를 찾을 수 없음");
            }
        }
    }
}
