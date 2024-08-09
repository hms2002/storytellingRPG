using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public static InfoManager instance { get; private set; }

    public InfoUI infoUI { get; private set; }

    private GameObject _root = null;
    private Canvas _rootCanvas = null;

    private void Awake()
    {
        // Singleton 패턴 구현
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 유지
            Init();
        }
        else
        {
            Destroy(gameObject);  // 중복된 인스턴스가 있다면 파괴
        }
    }

    private void Init()
    {
        // Root 및 RootCanvas 초기화
        if (_root == null)
        {
            _root = new GameObject(typeof(InfoManager).Name);
            _root.transform.SetParent(transform);  // InfoManager의 Transform에 붙임
        }

        if (_rootCanvas == null)
        {
            _rootCanvas = new GameObject("Canvas").AddComponent<Canvas>();
            _rootCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _rootCanvas.transform.SetParent(_root.transform);
        }

        // InfoUI 초기화
        infoUI = Object.Instantiate(Resources.Load<InfoUI>("UI/Tip"), _rootCanvas.transform);
        InitUIParent();
    }

    public void ShowTipUI(string title, Color titleColor, string tension, string content, Transform parent = null)
    {
        infoUI.gameObject.SetActive(true);
        infoUI.ShowTipUI(title, titleColor, tension, content, parent);
    }
    public void ShowTipUI(string title, Color titleColor, string content, Transform parent = null)
    {
        infoUI.gameObject.SetActive(true);
        infoUI.ShowTipUI(title, titleColor, content, parent);
    }

    public void HideTipUI()
    {
        infoUI.gameObject.SetActive(false);
        infoUI.transform.SetParent(_rootCanvas.transform);
    }

    public void InitUIParent()
    {
        infoUI.transform.SetParent(_rootCanvas.transform);
        infoUI.gameObject.SetActive(false);
    }
}