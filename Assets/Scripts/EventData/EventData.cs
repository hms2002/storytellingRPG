using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class EventData : ScriptableObject
{
    [Header("이벤트")]
    public Sprite roomImage;
    [Multiline(10)]
    public string roomContents;
    [Header("이벤트 키워드")]
    public List<GameObject> eventKeyword;
    [Space(3)]
    [Header("적용할 이벤트 함수")]
    public List<UnityEvent> optionEvent;

    [Space(3)]

    [Header("다음 이벤트")]
    [Multiline(10)]
    public string nextRoomContents;
    public List<UnityEvent> nextOptionEvent;

    [Space(3)]

    [Header("이벤트 종료")]
    [Multiline(10)]
    public string roomContentsAfter;
    public List<UnityEvent> afterOptionEvent;

    [Header("이벤트 종료2")]
    [Multiline(10)]
    public string roomContentsAfter2;
    public List<UnityEvent> afterOptionEvent2;
}
