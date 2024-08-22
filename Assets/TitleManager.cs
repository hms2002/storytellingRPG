using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    public Book book;
    public bool isClick = false;
    public GameObject[] gameObjectSet;

    private void Start()
    {
        GameManager.instance._gameState = GameState.Title;
        AudioManager.instance.UpdateBGM();
    }

    public void GameStart()
    {
        PageGo();
        if (isClick == false)
        {
            OnSound();
            DOVirtual.DelayedCall(2, () => { SceneManager.LoadScene("aoooo"); });
            isClick = true;
        }
    }

    public void GameQuit()
    {
        if (isClick == false)
        {
            OnSound();
            DOVirtual.DelayedCall(1, () => { Application.Quit(); });
            isClick = true;
        }
    }

    public void OnSound()
    {
        AudioManager.instance.PlaySound("Book", "페이지_넘기기");
    }

    public void PageGo()
    {
        for(int i=0; i< gameObjectSet.Length; i++)
        {
            gameObjectSet[i].SetActive(false);
        }
        Book.instance.bookAnimator.SetTrigger("turnPageToRight");
    }
}
