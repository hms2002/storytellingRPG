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
        public GameObject[] nodePrefab = new GameObject[5];
        [Header("맵 베이스 판")]
        public RectTransform mapParent; //맵 판
        [Header("시작 위치")]
        public RectTransform startTransform;

        [Header("배치 거리 조절(높이)")]
        [Range(0f, 300f)]
        public float distanceBetweenNodes_height_MIN = 10.0f; //높이 배치 최소 거리
        [Range(0f, 300f)]
        public float distanceBetweenNodes_height_MAX = 20.0f; //높이 배치 최대 거리
        [Header("배치 거리 조절(세로)")]
        [Range(0f, 300f)]
        public float distanceBetweenNodes_width_MIN = 5.0f; //좌우 배치 최소 거리
        [Range(0f, 300f)]
        public float distanceBetweenNodes_width_MAX = 10.0f; //좌우 배치 최대 거리

        [Header("가로 세로, 노드 최대 개수 설정")]
        public int maxHeightNodesCount = 3; //높이에서 노드 최대 개수
        public int maxWidthtNodesCount = 3; //가로에서 노드 최대 개수

        [Header("라인(길) 프리팹")]
        public Image roadPrefab;

        //노드 저장
        private List<MapNode> nodes = new List<MapNode>();
        
        //노드 한 라인마다 끝 위치 확인
        private List<int> nodesEndLineCheck = new List<int>();

        //노드 길 저장
        private List<Image> roadeLine = new List<Image>();


        public void Start()
        {
            //if 만약 만들어진 맵이없다면 혹은 클리어했다면
            GeneratorMap();
            //CreateNodeLine();
        }

        public void GeneratorMap()
        {
            int makeCount = -1; // -1부터 시작

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
            rectTransform.anchoredPosition = GetRandomPosition(_isWidth, _heiNum, _widNum) - new Vector2(800,400);
            //노드 모음에 추가
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
            int beforeNum = 0; // 뒤에 있던 열
            int nextNum = 0; // 앞에 있는 열
            int roadCount = 0;

            Debug.Log("nodesEndLineCheck: " + string.Join(", ", nodesEndLineCheck)); // nodesEndLineCheck 리스트 값 출력

            for (int i = 0; i < nodesEndLineCheck.Count - 1; i++) // 줄 세기, 조건 수정
            {
                if (i != 0)
                {
                    beforeNum = nodesEndLineCheck[i - 1]; // 이전 줄, 끝 노드 기록

                    if (i < nodesEndLineCheck.Count - 1) // 조건 수정
                    {
                        nextNum = nodesEndLineCheck[i + 1];
                        for (int nowNum = beforeNum + 1; nowNum <= nodesEndLineCheck[i]; nowNum++) // 반복문 범위 조정
                        {
                            for (int lineNum = nodesEndLineCheck[i]; lineNum < nextNum; lineNum++) // 반복문 범위 조정
                            {
                                if (nowNum < nodes.Count && lineNum < nodes.Count) // 인덱스 범위 체크
                                {
                                    nodes[nowNum].connectedNodes.Add(nodes[lineNum]);
                                    ConnectRoadLine(nodes[nowNum].GetComponent<RectTransform>(), nodes[lineNum].GetComponent<RectTransform>(), roadCount);
                                    roadCount++;
                                    Debug.Log("연결 nodes " + nowNum + "과 " + lineNum + " 연결");
                                }
                                else
                                {
                                    Debug.LogWarning("인덱스가 범위를 벗어났습니다: nowNum = " + nowNum + ", lineNum = " + lineNum);
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

            // 길 길이 설정
            RectTransform roadRectTransform = roadeLine[count].rectTransform;
            roadRectTransform.sizeDelta = new Vector2(distance, roadRectTransform.sizeDelta.y);

            // 선 이미지의 회전 설정
            float angle = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
            roadRectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
