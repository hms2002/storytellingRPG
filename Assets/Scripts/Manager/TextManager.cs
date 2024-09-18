using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Linq;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;
    public TextMeshProUGUI Text;
    public bool firstText = true;
    private Tween currentTween; // 현재 재생 중인 Tween을 추적
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 마우스 왼쪽 클릭 시, 진행 중인 Tween을 완성
        if (Input.GetMouseButtonDown(0) && currentTween != null && currentTween.IsActive())
        {
            currentTween.Complete(); // 현재 실행 중인 텍스트 애니메이션을 즉시 완료
        }
    }

    public void OnlyTextPlay(string[] _textList, float _time)
    {
        /*
        if (!Text.gameObject.activeInHierarchy)
        {
            // 필요한 경우 GameObject를 활성화합니다.
            Text.gameObject.SetActive(true);
        }
        */

        StartCoroutine(OnlyText(_textList, _time));
    }

    private IEnumerator OnlyText(string[] textList, float time)
    {
        Text.text = string.Empty;
        for (int i=0; i < textList.Length; i++)
        {
            currentTween = Text.DOText(textList[i], time);

            // Tween이 끝날 때까지 대기
            yield return currentTween.WaitForCompletion();

            if (i == textList.Length - 1 && textList.Length != 1)
            {
                UIManager.instance.ActiveRestButton(true);
                yield return new WaitForSeconds(0);
            }

            yield return new WaitForSeconds(3.0f);
        }
    }

    public void KeywordTextPlay(Actor actor)
    {
        Text.text = string.Empty;
        char lastString = actor.Name[actor.Name.Length - 1];
        if (lastString >= 0xAC00 && lastString <= 0xD7A3)
        {
            // 한글의 유니코드에서 종성 인덱스 추출
            int unicodeIndex = lastString - 0xAC00;
            int jongseongIndex = unicodeIndex % 28;

            // 종성이 있는지 확인
            if (jongseongIndex == 0)
            {
                currentTween = Text.DOText($"{actor.Name}는 _____ _____을 사용했다.", 1f);
            }
            else
            {
                currentTween = Text.DOText($"{actor.Name}은 _____ _____을 사용했다.", 1f);
            }
        }
        Text.alignment = TextAlignmentOptions.Top;
    }

    public void SupKeywordTextPlay(Actor actor)
    {
        string sup = actor.keywordSup.keywordName;
        Color supColor = actor.keywordSup.GetKeywordColor();
        string supColorHex = ColorUtility.ToHtmlStringRGB(supColor);
        char lastString = actor.Name[actor.Name.Length - 1];
        if (lastString >= 0xAC00 && lastString <= 0xD7A3)
        {
            // 한글의 유니코드에서 종성 인덱스 추출
            int unicodeIndex = lastString - 0xAC00;
            int jongseongIndex = unicodeIndex % 28;

            // 종성이 있는지 확인
            if (jongseongIndex == 0)
            {
                currentTween = Text.DOText($"{actor.Name}는 <color=#{supColorHex}>{sup} </color> _____을 사용했다.", 1f);
            }
            else
            {
                currentTween = Text.DOText($"{actor.Name}은 <color=#{supColorHex}>{sup} </color> _____을 사용했다.", 1f);
            }
        }
            /*Text.text = $"{actor.name}은 <color=#{supColorHex}>{sup} </color> _____을 사용했다.";*/
            Text.alignment = TextAlignmentOptions.Top;
    }

    public void MainKeywordTextPlay(Actor actor,float textTime)
    {
        string sup = actor.keywordSup.keywordName;
        Color supColor = actor.keywordSup.GetKeywordColor();
        string supColorHex = ColorUtility.ToHtmlStringRGB(supColor);
        string main = actor.keywordMain.keywordName;
        Color mainColor = actor.keywordMain.GetKeywordColor();
        string mainColorHex = ColorUtility.ToHtmlStringRGB(mainColor);
        char lastNameChar = actor.Name[actor.Name.Length - 1];
        char lastMainChar = main[main.Length - 1];

        string subjectPostfix = "은";
        string objectPostfix = "을";

        // 한글의 유니코드 범위에서 종성 확인
        if (lastNameChar >= 0xAC00 && lastNameChar <= 0xD7A3)
        {
            int nameUnicodeIndex = lastNameChar - 0xAC00;
            int nameJongseongIndex = nameUnicodeIndex % 28;

            // 종성이 없으면 "는", 있으면 "은"
            if (nameJongseongIndex == 0)
            {
                subjectPostfix = "는";
            }
        }

        // main의 마지막 글자에 따라 "을/를" 결정
        if (lastMainChar >= 0xAC00 && lastMainChar <= 0xD7A3)
        {
            int mainUnicodeIndex = lastMainChar - 0xAC00;
            int mainJongseongIndex = mainUnicodeIndex % 28;

            // 종성이 없으면 "를", 있으면 "을"
            if (mainJongseongIndex == 0)
            {
                objectPostfix = "를";
            }
        }

        currentTween = Text.DOText($"{actor.Name}{subjectPostfix} <color=#{supColorHex}>{sup}</color> <color=#{mainColorHex}>{main}</color>{objectPostfix} 사용했다.", textTime);

        Text.alignment = TextAlignmentOptions.Top;
        if (textTime < 1f)
        {
            Text.alignment = TextAlignmentOptions.Midline;
        }
    }

    public void EncounterTextPlay(Monster monster)
    {
        Text.text = string.Empty;
        float textTime = 2.5f; // 텍스트가 완전히 표시될 시간

        // 텍스트 애니메이션 실행
        currentTween = Text.DOText(monster.encounterText, textTime).OnComplete(() =>
        {
            // 텍스트 애니메이션이 완료되면 1.5초 후에 UI 활성화와 Flow 함수 실행
            DOVirtual.DelayedCall(1.5f, () =>
            {
                UIManager.instance.ActiveCombatKeywordUI(true);
                FightManager.fightManager.Flow(); // Flow 함수가 FightManager에 있는 것으로 가정
            });
        });

        Text.alignment = TextAlignmentOptions.Midline;
    }

    public void PrintVictory()
    {
        currentTween = Text.DOText("당신은 승리하였다.", 1f);
        Text.alignment = TextAlignmentOptions.Midline;
    }

    public void PrintPlayerDie()
    {
        currentTween = Text.DOText("당신의 이야기는 여기에서 끝났다.", 1f);
        Text.alignment = TextAlignmentOptions.Midline;
    }
}
