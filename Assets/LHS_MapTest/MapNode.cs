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
        private bool isEndNode = false;  // endNode �÷��� �߰�

        [SerializeField]
        private float initialScale; // ������ �⺻ ũ��
        [SerializeField]
        private float HoveScaleFactor = 1.2f; // (���콺 ������ ���� �� ������ Ȯ�� ���� 

        //����Ǵ� ���
        public List<MapNode> connectedNodes = new List<MapNode>();

        //�ӽ� start
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
                case NodeStates.Locked: //���
                    image.DOKill();
                    image.color = Color.gray;
                    break;

                case NodeStates.Attainable: // ���ð���
                    image.DOKill();
                    image.DOColor(Color.white, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                    break;

                case NodeStates.Visited: // �湮��
                    image.DOKill();
                    image.color = Color.white;
                    break;
            }
        }

        //���콺 ���� �ڵ�
        public void OnPointerEnter(PointerEventData eventData)
        {
            //���콺�� ������Ʈ�� ������ �� �ڵ�
            image.transform.DOKill();
            image.transform.DOScale(initialScale * HoveScaleFactor, 0.3f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //���콺�� ������Ʈ�� ����� �� �ڵ�
            image.transform.DOKill();
            image.transform.DOScale(initialScale, 0.3f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //���콺 ��ư�� ������Ʈ�� ������ �� �ڵ�
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //���콺 ��ư�� ������Ʈ�� ������ ���� �� �ڵ�
            if (nodeStates == NodeStates.Attainable)
            {
                nodeStates = NodeStates.Visited;
                SetStage();

                if (isEndNode)
                {
                    // Ư�� ���� ����
                    ExecuteEndNodeLogic();
                }
                else
                {
                    UpdateAllNodes();
                }

                // ��� ���� ���� �� �� ����
                MapState.InstanceMap.SaveMapData(Application.persistentDataPath + "/mapData.json");
            }
        }

        private void UpdateAllNodes()
        {
            // MapState�� �ν��Ͻ����� ��� ������ ������
            var allNodes = MapState.InstanceMap.nodes;
            var endNode = MapState.InstanceMap.endNode;  // �� ��带 ������

            // Ŭ���� ��带 ������ ��� ��带 Locked ���·� ����
            foreach (var node in allNodes)
            {
                if (node.nodeStates == NodeStates.Attainable)
                {
                    node.nodeStates = NodeStates.Locked;
                    node.SetStage();
                }
            }

            // ����� ��带 Attainable ���·� ����
            foreach (var connectedNode in connectedNodes)
            {
                if (connectedNode.nodeStates == NodeStates.Locked)
                {
                    connectedNode.nodeStates = NodeStates.Attainable;
                    connectedNode.SetStage();
                }
            }

            // ���� ��尡 ������ ���� ���ϴ��� Ȯ���Ͽ� �� ��带 Attainable ���·� ����
            if (connectedNodes.Count == 0)
            {
                endNode.GetComponent<MapNode>().nodeStates = NodeStates.Attainable;
                endNode.GetComponent<MapNode>().SetStage();
            }
        }

        private void ExecuteEndNodeLogic()
        {
            // EndNode Ŭ�� �� ������ ����
            Debug.Log("EndNode Ŭ���� - Ư�� ���� ����");
        }
    }
}