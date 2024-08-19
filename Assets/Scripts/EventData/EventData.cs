using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EventData : ScriptableObject
{
    [Header("이벤트이미지")]
    public Sprite roomImage;
    [Header("키워드 선택지에 따른 이미지변경")]
    public Sprite nextImage1;
    public Sprite nextImage2;
    public Sprite nextImage3;

    [Header("이벤트 텍스트")]
    [Multiline(10)]
    public string roomContents;
    [Header("이벤트 키워드")]
    public List<GameObject> eventKeyword;
    [Space(3)]

    [Header("이벤트 선택지1로 종료 or 뭘 골라도 같은 문구일 때 내용")]
    [Multiline(10)]
    public string roomContentsAfter;

    [Header("이벤트 선택지2로 종료")]
    [Multiline(10)]
    public string roomContentsAfter2;

    [Header("이벤트 선택지3로 종료")]
    [Multiline(10)]
    public string roomContentsAfter3;
}
