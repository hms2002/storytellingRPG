using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private Sprite[] TutorialImages;
    private Image TutorialImage;
    [SerializeField]
    private GameObject[] Buttons;

    enum ButtonType
    {
        next,
        previous,
        end
    }

    private void Awake()
    {
        TutorialImage = gameObject.transform.Find("TutorialImage").GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        TutorialImage.sprite = TutorialImages[0];
        Buttons[(int)ButtonType.next].SetActive(true);
        Buttons[(int)ButtonType.previous].SetActive(false);
        Buttons[(int)ButtonType.end].SetActive(false);
    }

    public void Next()
    {
        TutorialImage.sprite = TutorialImages[1];
        Buttons[(int)ButtonType.next].SetActive(false);
        Buttons[(int)ButtonType.previous].SetActive(true);
        Buttons[(int)ButtonType.end].SetActive(true);
    }

    public void Previous()
    {
        TutorialImage.sprite = TutorialImages[0];
        Buttons[(int)ButtonType.next].SetActive(true);
        Buttons[(int)ButtonType.previous].SetActive(false);
        Buttons[(int)ButtonType.end].SetActive(false);
    }

    public void End()
    {
        gameObject.SetActive(false);
    }
}
