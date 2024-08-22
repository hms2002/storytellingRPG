using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("/*해당 부분에 게임 시작 씬 적어줘*/");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
