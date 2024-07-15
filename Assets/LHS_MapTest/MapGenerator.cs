using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    // ���� �����ϴ� static Ŭ����
    public class MapGenerator : MonoBehaviour
    {
        [Header("��� ���� ����(������)")]
        public GameObject[] nodePrefab = new GameObject[5];
        [Header("�� ���̽� ��")]
        public RectTransform mapParent; //�� ��
        [Header("���� ��ġ")]
        public RectTransform startTransform;

        [Header("��ġ �Ÿ� ����(����)")]
        [Range(0f, 300f)]
        public float distanceBetweenNodes_height_MIN = 10.0f; //���� ��ġ �ּ� �Ÿ�
        [Range(0f, 300f)]
        public float distanceBetweenNodes_height_MAX = 20.0f; //���� ��ġ �ִ� �Ÿ�
        [Header("��ġ �Ÿ� ����(����)")]
        [Range(0f, 300f)]
        public float distanceBetweenNodes_width_MIN = 5.0f; //�¿� ��ġ �ּ� �Ÿ�
        [Range(0f, 300f)]
        public float distanceBetweenNodes_width_MAX = 10.0f; //�¿� ��ġ �ִ� �Ÿ�

        [Header("���� ����, ��� �ִ� ���� ����")]
        public int maxHeightNodesCount = 3; //���̿��� ��� �ִ� ����
        public int maxWidthtNodesCount = 3; //���ο��� ��� �ִ� ����

        [Header("����(��) ������")]
        public Image roadPrefab;

        //��� ����
        private List<MapNode> nodes = new List<MapNode>();
        
        //��� �� ���θ��� �� ��ġ Ȯ��
        private List<int> nodesEndLineCheck = new List<int>();

        //��� �� ����
        private List<Image> roadeLine = new List<Image>();


        public void Start()
        {
            //if ���� ������� ���̾��ٸ� Ȥ�� Ŭ�����ߴٸ�
            GeneratorMap();
            //CreateNodeLine();
        }

        public void GeneratorMap()
        {
            int makeCount = -1; // -1���� ����

            if (nodes != null && nodes.Count != 0)
            {
                ClearExistingNodes();
            }

            int heiNum = 0;
            int widNum = 0;

            for (widNum = 0; widNum < maxWidthtNodesCount; widNum++)
            {
                for (heiNum = 0; heiNum < Random.Range(0, maxHeightNodesCount); heiNum++)
                {
                    CreateNode(false, widNum, heiNum);
                    makeCount++;
                }
                CreateNode(true, widNum, heiNum);
                makeCount++;
                nodesEndLineCheck.Add(makeCount);
                //Debug.Log(makeCount);
            }

            int nodeCount = nodes.Count;
            Debug.Log("���� ������ ��� ����: " + nodeCount);

            CreateNodeLine();//�ӽ� ���߿� ����
        }

        private void CreateNode(bool _isWidth, int _heiNum, int _widNum) //True -> Width, False -> Height 
        {
            int probabilty = Random.Range(1, 100);
            int typeCount = 999; //null ��

            switch (probabilty)
            {
                case > 40: //60%, Nomal Enemy
                    typeCount = 0;
                    break;

                case > 30: //10% Rest Site (1���� ������)
                    typeCount = 2;
                    break;

                case > 10: //20% �̽��͸�(���)
                    typeCount = 3;
                    break;

                case > 0: //10% Shop 
                    typeCount = 4;
                    break;
            }
            //����
            GameObject nodeObject = Instantiate(nodePrefab[typeCount], mapParent);
            //��ġ ����
            RectTransform rectTransform = nodeObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GetRandomPosition(_isWidth, _heiNum, _widNum) - new Vector2(800,400);
            //��� ������ �߰�
            nodes.Add(nodeObject.GetComponent<MapNode>());
        }

        Vector2 GetRandomPosition(bool _isWidth, int _heiNum, int _widNum) //True -> Width, False -> Height
        {
            float x = Random.Range(distanceBetweenNodes_height_MIN, distanceBetweenNodes_height_MAX) + (distanceBetweenNodes_height_MAX * _heiNum);
            float y = Random.Range(distanceBetweenNodes_width_MIN, distanceBetweenNodes_width_MAX) + (distanceBetweenNodes_width_MAX * _widNum);
            
            return new Vector2(x, y);
        }

        private void ClearExistingNodes()
        {
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                Destroy(nodes[i].gameObject);
            }
            nodes.Clear();
            nodesEndLineCheck.Clear();
        }

        private void CreateNodeLine()
        {
            int beforeNum = 0; // �ڿ� �ִ� ��
            int nextNum = 0; // �տ� �ִ� ��
            int roadCount = 0;

            Debug.Log("nodesEndLineCheck: " + string.Join(", ", nodesEndLineCheck)); // nodesEndLineCheck ����Ʈ �� ���

            for (int i = 0; i < nodesEndLineCheck.Count - 1; i++) // �� ����, ���� ����
            {
                if (i != 0)
                {
                    beforeNum = nodesEndLineCheck[i - 1]; // ���� ��, �� ��� ���

                    if (i < nodesEndLineCheck.Count - 1) // ���� ����
                    {
                        nextNum = nodesEndLineCheck[i + 1];
                        for (int nowNum = beforeNum + 1; nowNum <= nodesEndLineCheck[i]; nowNum++) // �ݺ��� ���� ����
                        {
                            for (int lineNum = nodesEndLineCheck[i]; lineNum < nextNum; lineNum++) // �ݺ��� ���� ����
                            {
                                if (nowNum < nodes.Count && lineNum < nodes.Count) // �ε��� ���� üũ
                                {
                                    nodes[nowNum].connectedNodes.Add(nodes[lineNum]);
                                    ConnectRoadLine(nodes[nowNum].GetComponent<RectTransform>(), nodes[lineNum].GetComponent<RectTransform>(), roadCount);
                                    roadCount++;
                                    Debug.Log("���� nodes " + nowNum + "�� " + lineNum + " ����");
                                }
                                else
                                {
                                    Debug.LogWarning("�ε����� ������ ������ϴ�: nowNum = " + nowNum + ", lineNum = " + lineNum);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ConnectRoadLine(RectTransform startTrans, RectTransform endTrans, int count)
        {
            roadeLine.Add(Instantiate(roadPrefab, transform));

            Vector2 startPos = startTrans.anchoredPosition;
            Vector2 endPos = endTrans.anchoredPosition;
            float distance = Vector2.Distance(startPos, endPos);

            // �� ���� ����
            RectTransform roadRectTransform = roadeLine[count].rectTransform;
            roadRectTransform.sizeDelta = new Vector2(distance, roadRectTransform.sizeDelta.y);

            // �� �̹����� ȸ�� ����
            float angle = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
            roadRectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
