using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;
    public Button GameExit;
    public GameObject optionPanel;
    void Start()
    {
        // 슬라이더 초기화
        BGMSlider.minValue = 0;
        BGMSlider.maxValue = 100;
        BGMSlider.value = 100; // 기본값을 100으로 설정

        SFXSlider.minValue = 0;
        SFXSlider.maxValue = 100;
        SFXSlider.value = 100; // 기본값을 100으로 설정

        // 슬라이더의 이벤트 리스너 설정
        BGMSlider.onValueChanged.AddListener(UpdateBGMVolume);
        SFXSlider.onValueChanged.AddListener(UpdateSFXVolume);

        // 게임 종료 버튼에 클릭 이벤트 리스너 설정
        GameExit.onClick.AddListener(() => GameManager.instance.ExitGame());
    }

    void UpdateBGMVolume(float value)
    {
        AudioManager.instance.bgmSource.volume = value / 100f; // 슬라이더 값에 비례하여 볼륨 설정
    }

    void UpdateSFXVolume(float value)
    {
        foreach (var audioSource in AudioManager.instance.audioSourcePool)
        {
            audioSource.volume = value / 100f; // 슬라이더 값에 비례하여 볼륨 설정
        }
    }
    void Update()
    {
        if (optionPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            // 불투명한 패널이 클릭되었는지 확인
            if (!RectTransformUtility.RectangleContainsScreenPoint(optionPanel.GetComponent<RectTransform>(), Input.mousePosition))
            {
                UIManager.instance.ActiveOptionUI(false); // 옵션 창 닫기
            }
        }
    }
}
