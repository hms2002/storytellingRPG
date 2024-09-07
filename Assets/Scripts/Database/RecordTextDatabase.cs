using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RecordTextDatabase : MonoBehaviour
{
    public static RecordTextDatabase instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        GameManager.instance.PrintGameClearCredit();
    }

    IEnumerator GoToStart()
    {
        texts.SetActive(false);
        btn.SetActive(false);
        bookAnim.SetTrigger("turnPageToLeft");
        yield return new WaitForSeconds(0.5f);
        bookAnim.SetTrigger("turnPageToLeft");
        yield return new WaitForSeconds(0.5f);
        bookAnim.SetTrigger("turnPageToLeft");
        yield return new WaitForSeconds(1f);
        fadeAnim.SetTrigger("ReturnStart");
        yield return new WaitForSeconds(3f);
        GameManager.instance.LoadScene(0);
    }

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI nomalMonsterCounting;
    public TextMeshProUGUI eliteMonsterCounting;
    public TextMeshProUGUI bossCounting;
    public TextMeshProUGUI GetRelicCounting;
    public TextMeshProUGUI GetGoldCounting;
    public TextMeshProUGUI GetKeywordCounting;
    public GameObject creditCanvas;
    public GameObject texts;
    public GameObject btn;
    public Button EndBtn;
    public Animator bookAnim;
    public Animator fadeAnim;
}
