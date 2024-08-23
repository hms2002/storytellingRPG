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
        char lastString = actor.Name[actor.Name.Length - 1];
        if (lastString >= 0xAC00 && lastString <= 0xD7A3)
        {
            // 한글의 유니코드에서 종성 인덱스 추출
            int unicodeIndex = lastString - 0xAC00;
            int jongseongIndex = unicodeIndex % 28;

            // 종성이 있는지 확인
            if (jongseongIndex == 0)
            {
                Text.DOText($"{actor.Name}는 _____ _____을 사용했다.", 1f);
            }
            else
            {
                Text.DOText($"{actor.Name}은 _____ _____을 사용했다.", 1f);
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
                Text.DOText($"{actor.Name}는 <color=#{supColorHex}>{sup} </color> _____을 사용했다.", 1f);
            }
            else
            {
                Text.DOText($"{actor.Name}은 <color=#{supColorHex}>{sup} </color> _____을 사용했다.", 1f);
            }
        }
            /*Text.text = $"{actor.name}은 <color=#{supColorHex}>{sup} </color> _____을 사용했다.";*/
            Text.alignment = TextAlignmentOptions.Top;
    }

    public void MainKeywordTextPlay(Actor actor,float textTime)
    {
        /* string sup = actor.keywordSup.keywordName;
         Color supColor = actor.keywordSup.GetKeywordColor();
         string supColorHex = ColorUtility.ToHtmlStringRGB(supColor);
         string main = actor.keywordMain.keywordName;
         Color mainColor = actor.keywordMain.GetKeywordColor();
         string mainColorHex = ColorUtility.ToHtmlStringRGB(mainColor);
         char lastString = actor.Name[actor.Name.Length - 1];
         if (lastString >= 0xAC00 && lastString <= 0xD7A3)
         {
             // 한글의 유니코드에서 종성 인덱스 추출
             int unicodeIndex = lastString - 0xAC00;
             int jongseongIndex = unicodeIndex % 28;

             // 종성이 있는지 확인
             if (jongseongIndex == 0)
             {
                 Text.DOText($"{actor.Name}는 <color=#{supColorHex}>{sup}</color> <color=#{mainColorHex}>{main}</color>을 사용했다.", textTime);
             }
             else
             {
                 Text.DOText($"{actor.Name}은 <color=#{supColorHex}>{sup}</color> <color=#{mainColorHex}>{main}</color>을 사용했다.", textTime);
             }
         }
                 *//*        Text.text = $"{actor.name}은 <color=#{supColorHex}>{sup}</color> <color=#{mainColorHex}>{main}</color>을 사용했다.";*//*
         Text.alignment = TextAlignmentOptions.Top;
         if(textTime < 1f)
         {
             Text.alignment = TextAlignmentOptions.Midline;
         }*/
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

        Text.DOText($"{actor.Name}{subjectPostfix} <color=#{supColorHex}>{sup}</color> <color=#{mainColorHex}>{main}</color>{objectPostfix} 사용했다.", textTime);

        Text.alignment = TextAlignmentOptions.Top;
        if (textTime < 1f)
        {
            Text.alignment = TextAlignmentOptions.Midline;
        }
    }

    public void EncounterTextPlay(Monster monster)
    {
        Text.DOText(monster.encounterText, 2f);
        Text.alignment = TextAlignmentOptions.Midline;
    }

    public void PrintVictory()
    {
        Text.DOText("당신은 승리하였다.", 2f);
        Text.alignment = TextAlignmentOptions.Midline;
    }

    public void PrintPlayerDie()
    {
        Text.DOText("당신의 이야기는 여기에서 끝났다.", 2f);
        Text.alignment = TextAlignmentOptions.Midline;
    }
}
