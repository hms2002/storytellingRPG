using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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

    public void KeywordTextPlay(Actor actor)
    {
        Text.DOText($"{actor.name}은 _____ _____을 사용했다.", 2f);
        Text.alignment = TextAlignmentOptions.Top;
    }

    public void SupKeywordTextPlay(Actor actor)
    {
        string sup = actor.keywordSup.keywordName;
        Color supColor = actor.keywordSup.GetKeywordColor();
        string supColorHex = ColorUtility.ToHtmlStringRGB(supColor);
        Text.DOText($"{actor.name}은 <color=#{supColorHex}>{sup} </color> _____을 사용했다.", 2f);
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
    }

    public void EncounterTextPlay(Monster monster)
    {
        Text.DOText(monster.encounterText, 3f);
        Text.alignment = TextAlignmentOptions.Midline;
    }

    public void CombatText(Actor player, Actor monster)
    {
        
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
