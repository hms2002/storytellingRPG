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
    }

    public void EventTextPlay(string _str, float time)
    {
        StartCoroutine(PlayTextByLine(_str, time));
    }

    private IEnumerator PlayTextByLine(string fullText, float time)
    {
        // 텍스트를 줄바꿈(\n)으로 분리
        string[] lines = fullText.Split(new string[] { "\n" }, System.StringSplitOptions.None);

        foreach (string line in lines)
        {
            // 현재 줄을 출력하기 전에 텍스트를 비웁니다.
            Text.text = string.Empty;

            // 현재 줄을 타이핑 효과로 출력
            yield return Text.DOText(line, time).WaitForCompletion();

            // 다음 줄을 출력하기 전에 1초 대기 (필요에 따라 조정 가능)
            yield return new WaitForSeconds(3.0f);
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
        for(int i=0; i < textList.Length; i++)
        {
            Text.text = string.Empty;

            yield return Text.DOText(textList[i], time).WaitForCompletion();

            if(i == textList.Length - 1 && textList.Length != 1)
            {
                UIManager.instance.ActiveRestButton(true);
                yield return new WaitForSeconds(0);
            }

            yield return new WaitForSeconds(3.0f);
        }
    }


    public void KeywordTextPlay(Actor actor)
    {
        Text.DOText($"{actor.name}은 _____ _____을 사용했다.", 1f);
        Text.alignment = TextAlignmentOptions.Top;
    }

    public void SupKeywordTextPlay(Actor actor)
    {
        string sup = actor.keywordSup.keywordName;
        Color supColor = actor.keywordSup.GetKeywordColor();
        string supColorHex = ColorUtility.ToHtmlStringRGB(supColor);
        Text.DOText($"{actor.name}은 <color=#{supColorHex}>{sup} </color> _____을 사용했다.", 1f);
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
        Text.DOText($"{actor.name}은 <color=#{supColorHex}>{sup}</color> <color=#{mainColorHex}>{main}</color>을 사용했다.", textTime);
/*        Text.text = $"{actor.name}은 <color=#{supColorHex}>{sup}</color> <color=#{mainColorHex}>{main}</color>을 사용했다.";*/
        Text.alignment = TextAlignmentOptions.Top;
        if(textTime < 1f)
        {
            Text.alignment = TextAlignmentOptions.Midline;
        }
    }

    public void EncounterTextPlay(Monster monster)
    {
        Text.DOText(monster.encounterText, 3f);
        Text.alignment = TextAlignmentOptions.Midline;
    }

    public void PrintVictory()
    {
        Text.DOText("기사는 승리하였다.", 2f);
        Text.alignment = TextAlignmentOptions.Midline;
    }

    public void PrintPlayerDie()
    {
        Text.DOText("기사의 이야기는 여기에서 끝났다.", 2f);
        Text.alignment = TextAlignmentOptions.Midline;
    }
}
