using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSoundController : MonoBehaviour
{
    // 사운드 출력 함수들 ================================
    public void TurnPageRightSound()
    {
        AudioManager.instance.PlaySound("Book", "페이지_넘기기");
    }
    public void TurnPageLeftSound()
    {
        AudioManager.instance.PlaySound("Book", "페이지_돌아가기");
    }
    public void BookMarkSound()
    {
        AudioManager.instance.PlaySound("Book", "책갈피_넘기기");
    }
}
