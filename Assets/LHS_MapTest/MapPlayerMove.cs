using DG.Tweening;
using System;
using System.IO;
using UnityEngine;

namespace Map
{
    public class MapPlayeMove : MonoBehaviour
    {
        private GameObject mapMark;

        private void Start()
        {
            // 시작 시 플레이어 마크 참조
            if (MapState.InstanceMap != null && MapState.InstanceMap.mapMark != null)
            {
                mapMark = MapState.InstanceMap.mapMark;
            }

            // 플레이어 마크 초기 위치 설정
            SetInitialPlayerPosition();
        }

        public void MoveMapMark(Vector2 targetPosition)
        {
            if (mapMark != null && MapState.InstanceMap.noClickNode == false) // 이미 이동 중인 경우에는 무시
            {
                MapState.InstanceMap.noClickNode = true;
                // 플레이어 마크 이동
                mapMark.GetComponent<RectTransform>().DOAnchorPos(targetPosition, 1f).OnComplete(() =>
                {
                    MapState.InstanceMap.noClickNode = false;
                    Debug.Log("플레이어 마크 이동 완료: " + targetPosition);
                    MapState.InstanceMap.SavePlayerMarkPosition(Application.persistentDataPath + "/mapMarkData.json");
                });
            }
            else
            {
                Debug.LogWarning("플레이어 마크를 찾을 수 없거나 이미 이동 중임");
            }
        }

        private void SetInitialPlayerPosition() //초기 위치 세팅
        {
            string filePath = Application.persistentDataPath + "/mapMarkData.json";

            if (mapMark != null)
            {
                if (File.Exists(Application.persistentDataPath + "/mapMarkData.json"))
                {
                    try
                    {
                        string jsonData = File.ReadAllText(filePath);
                        MapMarkData mapMarkData = JsonUtility.FromJson<MapMarkData>(jsonData);
                        mapMark.GetComponent<RectTransform>().anchoredPosition = mapMarkData.mapMarkPosition;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("플레이어 데이터를 로드하는 중 오류 발생: " + e.Message);
                        mapMark.GetComponent<RectTransform>().anchoredPosition = MapState.InstanceMap.startNode.GetComponent<RectTransform>().anchoredPosition;
                    }
                }
                else
                {
                    mapMark.GetComponent<RectTransform>().anchoredPosition = MapState.InstanceMap.startNode.GetComponent<RectTransform>().anchoredPosition;
                }
            }
            else
            {
                Debug.LogWarning("맵 플레이어 마크 오브젝트 or 설정 안됨 없음");
            }
        }

        public void DeletePlayerPositionData()
        {
            // 파일 경로 설정
            string filePath = Application.persistentDataPath + "/playerData.json";

            // 파일이 존재하는지 확인
            if (File.Exists(filePath))
            {
                try
                {
                    // 파일 삭제
                    File.Delete(filePath);
                    Debug.Log("플레이어 데이터 파일이 삭제되었습니다.");
                }
                catch (Exception e)
                {
                    // 삭제 중 예외 처리
                    Debug.LogError("플레이어 데이터를 삭제하는 중 오류 발생: " + e.Message);
                }
            }
            else
            {
                Debug.LogWarning("삭제할 플레이어 데이터 파일이 존재하지 않습니다.");
            }
        }
    }
}