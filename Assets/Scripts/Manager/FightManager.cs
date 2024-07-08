using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public Actor player;
    public Actor monster;
    KeywordSup keywordSup;
    KeywordMain keywordMain;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetKeywordSup(KeywordSup _keywordSup)
    {
        if (_keywordSup == null)
            return;
        Debug.Log("보조 키워드 입력됨");
        keywordSup = _keywordSup;
    }


    public void GetKeywordMain(KeywordMain _keywordMain)
    {
        if (_keywordMain == null)
            return;
        Debug.Log("메인 키워드 입력됨");
        keywordMain = _keywordMain;
    }

    public void Flow()
    {
        // ShowKeywordSup()
        // ShowKeywordMain()
        if (keywordSup == null) return;
        if (keywordMain == null) return;
        Sentence sentence = new Sentence();

        keywordSup.Execute(player, monster, sentence);
        keywordMain.Execute(player, monster, sentence);

        sentence.execute(player, monster);
    }
}
