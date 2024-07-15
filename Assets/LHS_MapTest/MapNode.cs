using DG.Tweening;
using Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.VersionControl.Asset;

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
    public class MapNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private NodeStates nodeStates;
        [SerializeField]
        private NodeBlueprint nodeBlueprint;

        [SerializeField]
        private float initialScale; // 아이콘 기본 크기
        [SerializeField]
        private float HoveScaleFactor = 1.2f; // (마우스 가져다 됐을 시 아이콘 확대 배율 

        public string test = "테스트작동";

        //연결되는 노드
        public List<MapNode> connectedNodes = new List<MapNode>();

        //임시 start
        void Start()
        {
            SetUp();
            SetStage(nodeStates);
        }

        //이거 일단 보류 하고 있어 어떻게 할지
        public void SetUp()
        {
            image.sprite = nodeBlueprint.sprite;
        }

        //상태에 따라 노드 색깔 변화
        public void SetStage(NodeStates states)
        {
            switch(states)
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
            image.color = Color.white;
            nodeStates = NodeStates.Visited;
            SetStage(nodeStates);

            //여기에 진입 넣으면 될 듯.
            switch(nodeBlueprint.nodeType)
            {
                case NodeType.NomalEnemy:
                    break;
                case NodeType.BossEnemy:
                    break;
                case NodeType.Mystery:
                    break;
                case NodeType.RestSite:
                    break;
                case NodeType.Shop:
                    break;
                default:
                    Debug.Log("해당 노드 타입 진입 미설정");
                    break;
            }
        }
    }
}