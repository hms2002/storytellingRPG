using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Book book;
    public bool isClick = false;
    private bool isSatarted = false;
    public GameObject titleObjectSet;
    public GameObject imageSet;
    public GameObject imageSet2;
    private List<CanvasGroup> images = new List<CanvasGroup>();
    private List<CanvasGroup> images2 = new List<CanvasGroup>();
    [SerializeField] List<Sprite> defaultEndingSprites;
    [SerializeField] List<Sprite> demonKingEndingSprites;
    public GameObject foldedPage;
    public GameObject Tutorial;
    private bool smallSmallBig = false;

    private void Start()
    {
        imageSet.SetActive(true);
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Title")
        {
            GameManager.instance._gameState = GameState.Title;
        }
        if(sceneName == "Endding")
        {
            GameManager.instance._gameState = GameState.Ending;
        }
        AudioManager.instance.UpdateBGM();
        foreach (Transform child in imageSet.transform)
        {
            CanvasGroup canvasGroup = child.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                images.Add(canvasGroup);
            }
        }
        foreach (CanvasGroup cg in images)
        {
            cg.alpha = 0f;
        }
        imageSet.SetActive(false);
        if(imageSet2 != null)
        {
            foreach (Transform child in imageSet2.transform)
            {
                CanvasGroup canvasGroup = child.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    images2.Add(canvasGroup);
                }
            }
            foreach (CanvasGroup cg in images2)
            {
                cg.alpha = 0f;
            }
            imageSet2.SetActive(false);
        }
    }

    public void GameStart()
    {
        PageGo();
        if (isClick == false)
        {
/*            OnSound();*/
            DOVirtual.DelayedCall(2, () => { StartCoroutine(StartProduction()); });
            isClick = true;
            isSatarted = false;
        }
    }

    public void GameEnding()
    {
        PageGo();
        if (isClick == false)
        {
            OnSound();
            DOVirtual.DelayedCall(2, () => { StartCoroutine(EndingProduction()); });
            isClick = true;
        }
    }

    public void GameSceneLoad()
    {
        PageGo();
        DOVirtual.DelayedCall(2, () => { SceneManager.LoadScene("aoooo"); });
        GameManager.instance.gameState = GameState.Map;
    }

    public void TitleSceneLoad()
    {
        DOVirtual.DelayedCall(2, () => { SceneManager.LoadScene("Title"); });
/*        GameManager.instance.gameState = GameState.Map;*/
    }

    public IEnumerator StartProduction()
    {
        imageSet.SetActive(true);
        foldedPage.SetActive(true);
        float waitTime = 1.0f;

        for (int i = 0; i < images.Count; i++)
        {
            images[i].alpha = 0f;
            images[i].gameObject.SetActive(true);

            float elapsedTime = 0f;

            while (elapsedTime < waitTime)
            {
                images[i].alpha = Mathf.Clamp01(elapsedTime / waitTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            images[i].alpha = 1f;

            // 마지막 이미지일 경우 2초 대기, 그렇지 않으면 1초 대기
            if (i == images.Count - 1)
            {
                yield return new WaitForSeconds(2.0f);
                foldedPage.SetActive(false);
                GameSceneLoad();
            }
            else
            {
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

    public IEnumerator EndingProduction()
    {
        EndingCheck();
        if(smallSmallBig == true)
        {
            imageSet2.SetActive(true);
            float waitTime = 1.0f;

            for (int i = 0; i < images2.Count; i++)
            {
                images2[i].alpha = 0f;
                images2[i].gameObject.SetActive(true);

                float elapsedTime = 0f;

                while (elapsedTime < waitTime)
                {
                    images2[i].alpha = Mathf.Clamp01(elapsedTime / waitTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                images2[i].alpha = 1f;

                // 마지막 이미지일 경우 2초 대기, 그렇지 않으면 1초 대기
                if (i == images2.Count - 1)
                {
                    yield return new WaitForSeconds(2.0f);
                    TitleSceneLoad();
                }
                else
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }
        }
        else
        {
            imageSet.SetActive(true);
            float waitTime = 1.0f;

            for (int i = 0; i < images.Count; i++)
            {
                images[i].alpha = 0f;
                images[i].gameObject.SetActive(true);

                float elapsedTime = 0f;

                while (elapsedTime < waitTime)
                {
                    images[i].alpha = Mathf.Clamp01(elapsedTime / waitTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                images[i].alpha = 1f;

                // 마지막 이미지일 경우 2초 대기, 그렇지 않으면 1초 대기
                if (i == images.Count - 1)
                {
                    yield return new WaitForSeconds(2.0f);
                    TitleSceneLoad();
                }
                else
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }
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
        if(titleObjectSet != null)
        {
            titleObjectSet.SetActive(false);
        }
        imageSet.SetActive(false);
        if(!isSatarted)
        {
            Book.instance.bookAnimator.SetTrigger("turnPageToRight");
            isSatarted = true;
        }
    }

    public void ActiveTutorial()
    {
        Tutorial.SetActive(true);
    }

    private void EndingCheck()
    {
        if(GameManager.instance.killCnt_nomalMonster + GameManager.instance.killCnt_eleteMonster + GameManager.instance.killCnt_bossMonster > 24)
        {
            smallSmallBig = true;
            for (int i = 0; i < images.Count; i++)
            {
                images2[i].GetComponent<Image>().sprite = demonKingEndingSprites[i];    
            }
        }
        else
        {
            smallSmallBig = false;
            for (int i = 0; i < images.Count; i++)
            {
                images[i].GetComponent<Image>().sprite = defaultEndingSprites[i];
            }
        }
    }
}
