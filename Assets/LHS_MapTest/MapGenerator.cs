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

        [Header("���� ���� �� ���")]
        public GameObject startNode;
        public GameObject endNode;

        [Header("��ġ �Ÿ� ����(����)")]
        [Range(0f, 300f)]
        public float distanceBetweenNodes_height_MIN = 150.0f; //���� ��ġ �ּ� �Ÿ�
        [Range(0f, 300f)]
        public float distanceBetweenNodes_height_MAX = 300.0f; //���� ��ġ �ִ� �Ÿ�
        [Header("��ġ �Ÿ� ����(����)")]
        [Range(0f, 300f)]
        public float distanceBetweenNodes_width_MIN = 170.0f; //�¿� ��ġ �ּ� �Ÿ�
        [Range(0f, 300f)]
        public float distanceBetweenNodes_width_MAX = 220.0f; //�¿� ��ġ �ִ� �Ÿ�

        [Header("���� ����, ��� �ִ� ���� ����")]
        public int maxHeightNodesCount = 3; //���̿��� ��� �ִ� ����
        public int maxWidthtNodesCount = 5; //���ο��� ��� �ִ� ����

        [Header("����(��) ������")]
        public Image roadPrefab;

        [Header("��� �� (���� ��) ���� ����")] 
        public float nodeGap = 40f; // ���� ��� ���� ����

        //��� ����
        public List<MapNode> nodes = new List<MapNode>();

        //��� �� ���θ��� �� ��ġ Ȯ��
        public List<int> nodesEndLineCheck = new List<int>();

        //��� �� ����
        public List<GameObject> roadeLine = new List<GameObject>();

        virtual public void SpawnMap()
        {
            GeneratorMap();
            StartEndConnection();
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
                for (heiNum = 0; heiNum < Random.Range(1, maxHeightNodesCount); heiNum++)
                {
                    CreateNode(false, widNum, heiNum);
                    makeCount++;
                }
                CreateNode(true, widNum, heiNum);
                makeCount++;
                nodesEndLineCheck.Add(makeCount);
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
                        if (!nodes[j].connectedNodes.Contains(nodes[nextNode]) &&
                            !HasIntersectingLines(nodes[j].GetComponent<RectTransform>().anchoredPosition, nodes[nextNode].GetComponent<RectTransform>().anchoredPosition))
                        {
                            nodes[j].connectedNodes.Add(nodes[nextNode]);
                            ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                            isConnected[nextNode - nextColumnStart] = true;
                        }
                        else
                        {
                            // ���� ��ģ�ٸ� ���� ���� �ٸ� ���� ���� �õ�
                            for (int l = j - 1; l >= currentColumnStart; l--)
                            {
                                if (!HasIntersectingLines(nodes[l].GetComponent<RectTransform>().anchoredPosition, nodes[nextNode].GetComponent<RectTransform>().anchoredPosition))
                                {
                                    nodes[l].connectedNodes.Add(nodes[nextNode]);
                                    ConnectRoadLine(nodes[l].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                                    isConnected[nextNode - nextColumnStart] = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                // ������� ���� ��� ó��
                for (int j = currentColumnStart; j <= currentColumnEnd; j++)
                {
                    if (nodes[j].connectedNodes.Count == 0)
                    {
                        bool connectionMade = false;
                        foreach (int nextNode in availableNodes)
                        {
                            if (!HasIntersectingLines(nodes[j].GetComponent<RectTransform>().anchoredPosition, nodes[nextNode].GetComponent<RectTransform>().anchoredPosition))
                            {
                                nodes[j].connectedNodes.Add(nodes[nextNode]);
                                ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                                isConnected[nextNode - nextColumnStart] = true;
                                connectionMade = true;
                                break;
                            }
                        }
                        // ���� ������ ������ ���� ���, ������ ������ ����
                        if (!connectionMade)
                        {
                            int nextNode = availableNodes[0];
                            nodes[j].connectedNodes.Add(nodes[nextNode]);
                            ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                            isConnected[nextNode - nextColumnStart] = true;
                        }
                    }
                }

                // ���� ���� ������� ���� ��� ó��
                for (int k = 0; k < isConnected.Length; k++)
                {
                    if (!isConnected[k])
                    {
                        int nextNode = nextColumnStart + k;
                        bool connectionMade = false;
                        foreach (int j in Enumerable.Range(currentColumnStart, currentColumnEnd - currentColumnStart + 1))
                        {
                            if (!HasIntersectingLines(nodes[j].GetComponent<RectTransform>().anchoredPosition, nodes[nextNode].GetComponent<RectTransform>().anchoredPosition))
                            {
                                nodes[j].connectedNodes.Add(nodes[nextNode]);
                                ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                                connectionMade = true;
                                break;
                            }
                        }
                        // ���� ������ ������ ���� ���, ������ ������ ����
                        if (!connectionMade)
                        {
                            int randomNode = Random.Range(currentColumnStart, currentColumnEnd + 1);
                            nodes[randomNode].connectedNodes.Add(nodes[nextNode]);
                            ConnectRoadLine(nodes[randomNode].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                        }
                    }
                }
            }
        }

        private bool HasIntersectingLines(Vector2 startPos, Vector2 endPos)
        {
            foreach (var line in roadeLine)
            {
                RectTransform lineRectTransform = line.GetComponent<Image>().rectTransform;
                Vector2 lineStartPos = lineRectTransform.anchoredPosition - new Vector2(lineRectTransform.sizeDelta.x / 2, 0);
                Vector2 lineEndPos = lineRectTransform.anchoredPosition + new Vector2(lineRectTransform.sizeDelta.x / 2, 0);

                if (LinesIntersect(startPos, endPos, lineStartPos, lineEndPos))
                {
                    return true;
                }
            }
            return false;
        }

        private bool LinesIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            float d1 = Direction(p3, p4, p1);
            float d2 = Direction(p3, p4, p2);
            float d3 = Direction(p1, p2, p3);
            float d4 = Direction(p1, p2, p4);

            if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) &&
                ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0)))
            {
                return true;
            }

            return (d1 == 0 && OnSegment(p3, p4, p1)) ||
                   (d2 == 0 && OnSegment(p3, p4, p2)) ||
                   (d3 == 0 && OnSegment(p1, p2, p3)) ||
                   (d4 == 0 && OnSegment(p1, p2, p4));
        }

        private float Direction(Vector2 pi, Vector2 pj, Vector2 pk)
        {
            return (pk.x - pi.x) * (pj.y - pi.y) - (pk.y - pi.y) * (pj.x - pi.x);
        }

        private bool OnSegment(Vector2 pi, Vector2 pj, Vector2 pk)
        {
            return Mathf.Min(pi.x, pj.x) <= pk.x && pk.x <= Mathf.Max(pi.x, pj.x) &&
                   Mathf.Min(pi.y, pj.y) <= pk.y && pk.y <= Mathf.Max(pi.y, pj.y);
        }

        private int GetRandomConnections()
        {
            int randomValue = Random.Range(1, 101); // 1���� 100������ ���� ��

            if (randomValue <= 40)
            {
                return 1; // 40% Ȯ���� 1��
            }
            else if (randomValue <= 70)
            {
                return 2; // 30% Ȯ���� 2��
            }
            else if (randomValue <= 90)
            {
                return 3; // 20% Ȯ���� 3��
            }
            else
            {
                return maxHeightNodesCount; // 10% Ȯ���� maxHeightNodesCount
            }
        }

        private void ConnectRoadLine(RectTransform startTrans, RectTransform endTrans)
        {
            // �� �̹��� ����
            Image line = Instantiate(roadPrefab, roadParent);
            roadeLine.Add(line.gameObject);

            // �� �� ������ �Ÿ� ���
            Vector2 startPos = startTrans.anchoredPosition;
            Vector2 endPos = endTrans.anchoredPosition;
            float distance = Vector2.Distance(startPos, endPos);

            // ���� ����
            Vector2 direction = (endPos - startPos).normalized;
            startPos += direction * nodeGap;
            endPos -= direction * nodeGap;

            // �� �̹����� ���� ����
            RectTransform roadRectTransform = line.rectTransform;
            roadRectTransform.sizeDelta = new Vector2(distance - (2 * nodeGap), roadRectTransform.sizeDelta.y);

            // �� �̹����� ��ġ ���� (�� ���� �߰���)
            roadRectTransform.anchoredPosition = (startPos + endPos) / 2;

            // �� �̹����� ȸ�� ����
            float angle = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
            roadRectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void StartEndConnection()
        {
            //���� ���� ����
            for (int i = 0; i <= nodesEndLineCheck[0]; i++)
            {
                startNode.GetComponent<MapNode>().connectedNodes.Add(nodes[i]);
                ConnectRoadLine(startNode.GetComponent<RectTransform>(), nodes[i].GetComponent<RectTransform>());
            }
            //�� ���� ����
            for (int i = nodesEndLineCheck[nodesEndLineCheck.Count - 2]+1; i <= nodesEndLineCheck[nodesEndLineCheck.Count-1]; i++)
            {
                endNode.GetComponent<MapNode>().connectedNodes.Add(nodes[i]);
                ConnectRoadLine(endNode.GetComponent<RectTransform>(), nodes[i].GetComponent<RectTransform>());
            }
        }
    }
}
