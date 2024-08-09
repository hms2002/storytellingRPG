using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    // 맵을 생성하는 static 클래스
    public class MapGenerator : MonoBehaviour
    {
        [Header("노드 종류 세팅(프리팹)")]
        [Header("0: NomalMonsterNode │ 1:  EliteMonsterNode │ 2: BossNode\n3: RestNode │ 4: StoreNode │ 5: TreasureNode 순서로 배치")]
        public GameObject[] nodePrefab = new GameObject[6];

        [Header("맵 베이스 판")]
        public RectTransform mapParent; //맵 판
        [Header("길 라인 보관 위치")]
        public RectTransform roadParent;

        [Header("시작 노드과 끝(보스) 노드[위치 배열 필요]")]
        public GameObject startNode;
        public GameObject endNode;

        [Header("배치 거리 조절(양옆)")]
        [Range(0f, 300f)]
        public float distanceBetweenNodes_Width_MIN = 150.0f; //양옆 배치 최소 거리
        [Range(0f, 300f)]
        public float distanceBetweenNodes_Width_MAX = 300.0f; //양옆 배치 최대 거리
        [Header("배치 거리 조절(높이)")]
        [Range(0f, 300f)]
        public float distanceBetweenNodes_Height_MIN = 170.0f; //높이 배치 최소 거리
        [Range(0f, 300f)]
        public float distanceBetweenNodes_Height_MAX = 220.0f; //높이 배치 최대 거리

        [Header("가로 세로, 노드 최대 개수 설정")]
        public int maxHeightNodesCount = 3; //높이에서 노드 최대 개수
        public int maxWidthtNodesCount = 5; //가로에서 노드 최대 개수

        [Header("라인(길) 프리팹")]
        public Image roadPrefab;

        [Header("노드 간 (라인 길) 간극 설정")] 
        public float nodeGap = 40f; // 노드와 노드 사이 간극

        //노드 저장
        public List<MapNode> nodes = new List<MapNode>();
        //노드 한 라인마다 끝 위치 확인
        public List<int> nodesEndLineCheck = new List<int>();
        //노드 길 저장
        public List<GameObject> roadeLine = new List<GameObject>();
        //맵 생성 시작 위치
        public Vector2 startVector = new Vector2(600, 400);



        virtual public void SpawnMap()
        {
            GeneratorMap();
            StartEndConnection();
        }

        public void GeneratorMap()
        {
            startNode.SetActive(true);
            endNode.SetActive(true);

            int makeCount = -1; // -1부터 시작

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
            Debug.Log("현재 생성된 노드 개수: " + nodeCount);

            CreateNodeLine();//임시 나중에 없애
        }

        private void CreateNode(bool _isWidth, int _heiNum, int _widNum) //True -> Width, False -> Height 
        {
            int probabilty = Random.Range(1, 100);
            int typeCount = 999; //null 값

            switch (probabilty)
            {
                case > 40: //60%, Nomal Enemy
                    typeCount = 0;
                    break;

                case > 30: //10% Rest Site (1번은 보스임)
                    typeCount = 2;
                    break;

                case > 10: //20% 미스터리(사건)
                    typeCount = 3;
                    break;

                case > 0: //10% Shop 
                    typeCount = 4;
                    break;
            }
            //생성
            GameObject nodeObject = Instantiate(nodePrefab[typeCount], mapParent);
            //위치 설정
            RectTransform rectTransform = nodeObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = GetRandomPosition(_isWidth, _heiNum, _widNum) - startVector;
            //노드 모음에 추가
            nodes.Add(nodeObject.GetComponent<MapNode>());
        }

        Vector2 GetRandomPosition(bool _isWidth, int _heiNum, int _widNum) //True -> Width, False -> Height
        {
            float x = Random.Range(distanceBetweenNodes_Width_MIN, distanceBetweenNodes_Width_MAX) + (distanceBetweenNodes_Width_MAX * _heiNum);
            float y = Random.Range(distanceBetweenNodes_Height_MIN, distanceBetweenNodes_Height_MAX) + (distanceBetweenNodes_Height_MAX * _widNum);

            return new Vector2(x, y);
        }

        protected void ClearExistingNodes()
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

            Debug.Log("nodesEndLineCheck: " + string.Join(", ", nodesEndLineCheck)); // nodesEndLineCheck 리스트 값 출력

            for (int i = 0; i < nodesEndLineCheck.Count - 1; i++)
            {
                int currentColumnStart = (i == 0) ? 0 : nodesEndLineCheck[i - 1] + 1;
                int currentColumnEnd = nodesEndLineCheck[i];
                int nextColumnStart = nodesEndLineCheck[i] + 1;
                int nextColumnEnd = nodesEndLineCheck[i + 1];

                // 각 노드가 적어도 하나의 연결을 가짐을 보장하기 위해 사용
                bool[] isConnected = new bool[nextColumnEnd - nextColumnStart + 1];

                // 추가 랜덤 연결
                for (int j = currentColumnStart; j <= currentColumnEnd; j++)
                {
                    int connections = GetRandomConnections(); // 확률 기반 연결 개수 결정

                    List<int> potentialNodes = new List<int>();
                    for (int k = -1; k <= 1; k++)
                    {
                        int potentialIndex = j - currentColumnStart + k;
                        if (potentialIndex >= 0 && potentialIndex < (nextColumnEnd - nextColumnStart + 1))
                        {
                            potentialNodes.Add(nextColumnStart + potentialIndex);
                        }
                    }

                    for (int k = 0; k < connections; k++)
                    {
                        if (potentialNodes.Count == 0) break;

                        int nextNode = potentialNodes[0];
                        potentialNodes.RemoveAt(0);

                        if (!nodes[j].connectedNodes.Contains(nodes[nextNode]))
                        {
                            bool hasIntersection = HasIntersectingLines(nodes[j].GetComponent<RectTransform>().anchoredPosition, nodes[nextNode].GetComponent<RectTransform>().anchoredPosition);

                            // 교차가 발생하면 위쪽 노드로 시도
                            while (hasIntersection && potentialNodes.Count > 0)
                            {
                                nextNode = potentialNodes[0];
                                potentialNodes.RemoveAt(0);
                                hasIntersection = HasIntersectingLines(nodes[j].GetComponent<RectTransform>().anchoredPosition, nodes[nextNode].GetComponent<RectTransform>().anchoredPosition);
                            }

                            // 최종 선택된 노드와 연결
                            if (!hasIntersection)
                            {
                                nodes[j].connectedNodes.Add(nodes[nextNode]);
                                ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                                isConnected[nextNode - nextColumnStart] = true;
                            }
                        }
                    }
                }

                // 연결되지 않은 노드 처리
                for (int j = currentColumnStart; j <= currentColumnEnd; j++)
                {
                    if (nodes[j].connectedNodes.Count == 0)
                    {
                        bool connectionMade = false;
                        for (int nextNode = nextColumnStart; nextNode <= nextColumnEnd; nextNode++)
                        {
                            if (!HasIntersectingLines(nodes[j].GetComponent<RectTransform>().anchoredPosition, nodes[nextNode].GetComponent<RectTransform>().anchoredPosition))
                            {
                                nodes[j].connectedNodes.Add(nodes[nextNode]);
                                ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                                connectionMade = true;
                                break;
                            }
                        }
                        // 만약 연결을 만들지 못한 경우, 위쪽 노드와 강제로 연결
                        if (!connectionMade)
                        {
                            for (int nextNode = nextColumnEnd; nextNode >= nextColumnStart; nextNode--)
                            {
                                if (!nodes[j].connectedNodes.Contains(nodes[nextNode]))
                                {
                                    nodes[j].connectedNodes.Add(nodes[nextNode]);
                                    ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                                    break;
                                }
                            }
                        }
                    }
                }

                // 다음 열의 연결되지 않은 노드 처리
                for (int k = 0; k < isConnected.Length; k++)
                {
                    if (!isConnected[k])
                    {
                        int nextNode = nextColumnStart + k;
                        bool connectionMade = false;
                        for (int j = currentColumnStart; j <= currentColumnEnd; j++)
                        {
                            if (!HasIntersectingLines(nodes[j].GetComponent<RectTransform>().anchoredPosition, nodes[nextNode].GetComponent<RectTransform>().anchoredPosition))
                            {
                                nodes[j].connectedNodes.Add(nodes[nextNode]);
                                ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                                connectionMade = true;
                                break;
                            }
                        }
                        // 만약 연결을 만들지 못한 경우, 위쪽 노드와 강제로 연결
                        if (!connectionMade)
                        {
                            for (int j = currentColumnEnd; j >= currentColumnStart; j--)
                            {
                                if (!nodes[j].connectedNodes.Contains(nodes[nextNode]))
                                {
                                    nodes[j].connectedNodes.Add(nodes[nextNode]);
                                    ConnectRoadLine(nodes[j].GetComponent<RectTransform>(), nodes[nextNode].GetComponent<RectTransform>());
                                    break;
                                }
                            }
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
            int randomValue = Random.Range(1, 101); // 1부터 100까지의 랜덤 값

            if (randomValue <= 60)
            {
                return 1; // 40% 확률로 1개
            }
            else // (randomValue <= 70)
            {
                return 2; // 30% 확률로 2개
            }
            /*
            else if (randomValue <= 90)
            {
                return 3; // 20% 확률로 3개
            }
            else
            {
                return maxHeightNodesCount; // 10% 확률로 maxHeightNodesCount
            }
            */
        }

        protected void ConnectRoadLine(RectTransform startTrans, RectTransform endTrans)
        {
            // 선 이미지 생성
            Image line = Instantiate(roadPrefab, roadParent);
            roadeLine.Add(line.gameObject);

            // 두 점 사이의 거리 계산
            Vector2 startPos = startTrans.anchoredPosition;
            Vector2 endPos = endTrans.anchoredPosition;
            float distance = Vector2.Distance(startPos, endPos);

            // 간극 조절
            Vector2 direction = (endPos - startPos).normalized;
            startPos += direction * nodeGap;
            endPos -= direction * nodeGap;

            // 선 이미지의 길이 설정
            RectTransform roadRectTransform = line.rectTransform;
            roadRectTransform.sizeDelta = new Vector2(distance - (2 * nodeGap), roadRectTransform.sizeDelta.y);

            // 선 이미지의 위치 설정 (두 점의 중간점)
            roadRectTransform.anchoredPosition = (startPos + endPos) / 2;

            // 선 이미지의 회전 설정
            float angle = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
            roadRectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        protected void StartEndConnection()
        {
            if (nodesEndLineCheck.Count > 0)
            {
                // 시작 지점 연결
                int firstLineEndIndex = nodesEndLineCheck[0];
                for (int i = 0; i <= firstLineEndIndex; i++)
                {
                    startNode.GetComponent<MapNode>().connectedNodes.Add(nodes[i]);
                    ConnectRoadLine(startNode.GetComponent<RectTransform>(), nodes[i].GetComponent<RectTransform>());
                }

                // 끝 지점 연결
                if (nodesEndLineCheck.Count > 1)
                {
                    int lastLineStartIndex = nodesEndLineCheck[nodesEndLineCheck.Count - 2] + 1;
                    int lastLineEndIndex = nodesEndLineCheck[nodesEndLineCheck.Count - 1];
                    for (int i = lastLineStartIndex; i <= lastLineEndIndex; i++)
                    {
                        endNode.GetComponent<MapNode>().connectedNodes.Add(nodes[i]);
                        ConnectRoadLine(endNode.GetComponent<RectTransform>(), nodes[i].GetComponent<RectTransform>());
                    }
                }
            }
        }
    }
}
