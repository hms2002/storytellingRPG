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
        private float initialScale; // ������ �⺻ ũ��
        [SerializeField]
        private float HoveScaleFactor = 1.2f; // (���콺 ������ ���� �� ������ Ȯ�� ���� 

        public string test = "�׽�Ʈ�۵�";

        //����Ǵ� ���
        public List<MapNode> connectedNodes = new List<MapNode>();

        //�ӽ� start
        void Start()
        {
            SetUp();
            SetStage(nodeStates);
        }

        //�̰� �ϴ� ���� �ϰ� �־� ��� ����
        public void SetUp()
        {
            image.sprite = nodeBlueprint.sprite;
        }

        //���¿� ���� ��� ���� ��ȭ
        public void SetStage(NodeStates states)
        {
            switch(states)
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
            image.color = Color.white;
            nodeStates = NodeStates.Visited;
            SetStage(nodeStates);

            //���⿡ ���� ������ �� ��.
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
                    Debug.Log("�ش� ��� Ÿ�� ���� �̼���");
                    break;
            }
        }
    }
}