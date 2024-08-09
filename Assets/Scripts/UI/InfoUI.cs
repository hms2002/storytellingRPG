using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum ETipPos
{
    Up, Down, Right, Left, UpRight, UpLeft, DownRight, DownLeft
}

public class InfoUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _tipBot, _tipMid, _tipTop;
    [SerializeField]
    private Transform tipParent;
    [SerializeField]
    private TextMeshProUGUI tipText;

    [SerializeField]
    private float _width = 400f;
    [SerializeField]
    private float _heightInRow = 50f;

    private List<GameObject> _tipObjects = new List<GameObject>();
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }


    public void ShowTipUI(string title, Color titleColor, string tension, string content, Transform parent)
    {
        InitializeTipObjects(content);

        // 타이틀, 텐션, 콘텐츠 텍스트 설정 (리치 텍스트 사용)
        string richText = $"<color=#{ColorUtility.ToHtmlStringRGB(titleColor)}>{title}</color>" + "  " + $"<color=#BF00FF>{tension}</color>\n\n" + // 보라색 텍스트
                          $"{content}";

        tipText.text = richText;

        // 포지셔닝
        Positioning(parent);
    }

    public void ShowTipUI(string title, Color titleColor, string content, Transform parent)
    {
        InitializeTipObjects(content);

        // 타이틀, 텐션, 콘텐츠 텍스트 설정 (리치 텍스트 사용)
        string richText = $"<color=#{ColorUtility.ToHtmlStringRGB(titleColor)}>{title}</color>\n\n" + // 보라색 텍스트
                          $"{content}";

        tipText.text = richText;

        // 포지셔닝
        Positioning(parent);
    }

    private void InitializeTipObjects(string content)
    {
        // 기존 팁 오브젝트들 제거
        _tipObjects.ForEach(elem => Destroy(elem));
        _tipObjects.Clear();

        // 개행수 + 2
        int rowCount = CheckContent(content) + 2;

        // 팁 UI 크기 설정
        _rectTransform.sizeDelta = new Vector2(_width, _heightInRow * rowCount);

        // 팁 오브젝트 생성 및 관리
        _tipObjects.Add(Instantiate(_tipTop, tipParent));
        for (int i = 0; i < rowCount; i++)
        {
            _tipObjects.Add(Instantiate(_tipMid, tipParent));
        }
        _tipObjects.Add(Instantiate(_tipBot, tipParent));

        // 팁 오브젝트 크기 설정
        for (int i = 0; i < _tipObjects.Count; i++)
        {
            _tipObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(_width, _heightInRow * rowCount);
        }
    }

    private void Positioning(Transform parent)
    {
        // 기본 위치 설정
        transform.SetParent(parent);
        _rectTransform.anchorMin = new Vector2(1f, 1f);
        _rectTransform.anchorMax = new Vector2(1f, 1f);
        _rectTransform.anchoredPosition = new Vector2(_width / 2, _heightInRow / 2);

        // 화면 경계 체크 및 위치 조정
        AdjustPositionToFitScreen(parent);
    }


/*    private void Positioning(Vector2 pos)
    {
        // 기본 위치 설정
        _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        transform.position = pos + new Vector2(_width / 2, _heightInRow / 2);

        // 위치 보정
        AdjustPositionToFitScreen();
    }
*/
    
    private void AdjustPositionToFitScreen(Transform parent)
    {
        float width = _rectTransform.sizeDelta.x;
        float height = _rectTransform.sizeDelta.y;

        // 기본 위치 (UpRight)
        transform.position = parent.position + new Vector3(width / 2, height / 2);

        // 화면 경계 체크
        Vector3[] corners = new Vector3[4];
        _rectTransform.GetWorldCorners(corners);

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        bool overflowsRight = corners[2].x > screenWidth;  // 오른쪽 경계 체크
        bool overflowsUp = corners[2].y > screenHeight;    // 위쪽 경계 체크

        ETipPos tipPos = ETipPos.UpRight;

        // 오른쪽과 위쪽 경계 체크에 따른 위치 조정
        if (overflowsUp && overflowsRight)
        {
            tipPos = ETipPos.DownLeft;
        }
        else if (overflowsUp)
        {
            tipPos = ETipPos.DownRight;
        }
        else if (overflowsRight)
        {
            tipPos = ETipPos.UpLeft;
        }

        // 최종 위치 설정
        SetPosition(parent.position, tipPos, width, height);

        // 위치 보정 후, Canvas의 자식으로 설정하여 화면 상단에 표시
        transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
        transform.SetAsLastSibling();
    }

    private void SetPosition(Vector2 pos, ETipPos tipPos, float width, float height)
    {
        switch (tipPos)
        {
            case ETipPos.UpRight:
                transform.position = pos + new Vector2(width / 2, height / 2);
                break;
            case ETipPos.DownRight:
                transform.position = pos + new Vector2(width / 2, -height / 2);
                break;
            case ETipPos.UpLeft:
                transform.position = pos + new Vector2(-width / 2, height / 2);
                break;
            case ETipPos.DownLeft:
                transform.position = pos + new Vector2(-width / 2, -height / 2);
                break;
        }
    }

    private int CheckContent(string content)
    {
        int rowCount = 1;
        for (int i = 0; i < content.Length; i++)
        {
            if (content[i] == '\n')
                rowCount++;
        }
        return rowCount;
    }
}