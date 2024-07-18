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
        [Header("�� ���� ���� ��ġ")]
        public RectTransform roadParent;
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
        int roadCount = 0;

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
            rectTransform.anchoredPosition = GetRandomPosition(_isWidth, _heiNum, _widNum) - new Vector2(800, 400);
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
            if (roadeLine != null && roadeLine.Count != 0)
            {
                for (int i = roadeLine.Count - 1; i >= 0; i--)
                {
                    Destroy(roadeLine[i].gameObject);
                }
                roadeLine.Clear();
            }

            int roadCount = 0;

            Debug.Log("nodesEndLineCheck: " + string.Join(", ", nodesEndLineCheck)); // nodesEndLineCheck ����Ʈ �� ���

            for (int i = 0; i < nodesEndLineCheck.Count - 1; i++)
            {
                int currentColumnStart = (i == 0) ? 0 : nodesEndLineCheck[i - 1] + 1;
                int currentColumnEnd = nodesEndLineCheck[i];
                int nextColumnStart = nodesEndLineCheck[i] + 1;
                int nextColumnEnd = nodesEndLineCheck[i + 1];

                // �� ��尡 ��� �ϳ��� ������ ������ �����ϱ� ���� ���
                bool[] isConnected = new bool[nextColumnEnd - nextColumnStart + 1];
                List<int> availableNodes = Enumerable.Range(nextColumnStart, nextColumnEnd - nextColumnStart + 1).ToList();

                // �߰� ���� ����
                for (int j = currentColumnStart; j <= currentColumnEnd; j++)
                {
                    int connections = GetRandomConnections(); // Ȯ�� ��� ���� ���� ����

                    for (int k = 0; k < connections; k++)
                    {
                        int nextNode = Random.Range(nextColumnStart, nextColumnEnd + 1);
                        if (!nodes[j].connectedNodes.Contains(nodes[nextNode]))
                        {
                            nodes[j].connectedNodes.Add(nodes[nextNode]);
                            ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>(), roadCount);
                            roadCount++;
                            isConnected[nextNode - nextColumnStart] = true;
                        }
                    }
                }

                // ������� ���� ��� ó��
                for (int k = 0; k < isConnected.Length; k++)
                {
                    if (!isConnected[k])
                    {
                        int randomNode = Random.Range(currentColumnStart, currentColumnEnd + 1);
                        int nextNode = nextColumnStart + k;
                        nodes[randomNode].connectedNodes.Add(nodes[nextNode]);
                        ConnectRoadLine(nodes[randomNode].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>(), roadCount);
                        roadCount++;
                    }
                }
            }
        }

        private int GetRandomConnections()
        {
            int randomValue = Random.Range(1, 101); // 1���� 100������ ���� ��

            if (randomValue <= 40)
            {
                return 1; // 40% Ȯ���� 1��
            }
            else if (randomValue <= 80)
            {
                return 2; // 40% Ȯ���� 2��
            }
            else
            {
                return 3; // 20% Ȯ���� 3��
            }
        }

        private void ConnectRoadLine(RectTransform startTrans, RectTransform endTrans, int count)
        {
            // �� �̹��� ����
            Image line = Instantiate(roadPrefab, roadParent);
            roadeLine.Add(line);

            // �� �� ������ �Ÿ� ���
            Vector2 startPos = startTrans.anchoredPosition;
            Vector2 endPos = endTrans.anchoredPosition;
            float distance = Vector2.Distance(startPos, endPos);

            // �� �̹����� ���� ����
            RectTransform roadRectTransform = roadeLine[count].rectTransform;
            roadRectTransform.sizeDelta = new Vector2(distance, roadRectTransform.sizeDelta.y);

            // �� �̹����� ��ġ ���� (�� ���� �߰���)
            roadRectTransform.anchoredPosition = (startPos + endPos) / 2;

            // �� �̹����� ȸ�� ����
            float angle = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
            roadRectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
