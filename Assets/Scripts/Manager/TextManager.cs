using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;
    public TextMeshProUGUI Text;
    [SerializeField] private GameObject theEndIcon;

    
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
        Text.text = $"{actor.name}은 _____ _____을 사용했다.";
        Text.alignment = TextAlignmentOptions.Top;

    }

    public void SupKeywordTextPlay(Actor actor)
    {
        string sup = actor.keywordSup.keywordName;
        Color supColor = actor.keywordSup.GetKeywordColor();
        string supColorHex = ColorUtility.ToHtmlStringRGB(supColor);
        Text.text = $"{actor.name}은 <color=#{supColorHex}>{sup} </color> _____을 사용했다.";
        Text.alignment = TextAlignmentOptions.Top;

    }

    public void MainKeywordTextPlay(Actor actor)
    {
        string sup = actor.keywordSup.keywordName;
        Color supColor = actor.keywordSup.GetKeywordColor();
        string supColorHex = ColorUtility.ToHtmlStringRGB(supColor);
        string main = actor.keywordMain.keywordName;
        Color mainColor = actor.keywordMain.GetKeywordColor();
        string mainColorHex = ColorUtility.ToHtmlStringRGB(mainColor);

        Text.text = $"{actor.name}은 <color=#{supColorHex}>{sup}</color> <color=#{mainColorHex}>{main}</color>을 사용했다.";
        Text.alignment = TextAlignmentOptions.Top;

    }

    public void EncounterTextPlay(Monster monster)
    {   
        Text.text = monster.encounterText;
        Text.alignment = TextAlignmentOptions.Midline;
    }

    public void CombatText(Actor player, Actor monster)
    {
        
    }

    public void PrintVictory()
    {
        Text.text = "기사는 승리하였다.";
        Text.alignment = TextAlignmentOptions.Midline;
    }

    public void PrintPlayerDie()
    {
        Text.text = "기사는 사망하였다.";
        Text.alignment = TextAlignmentOptions.Midline;

        theEndIcon.SetActive(true);

    }
}
