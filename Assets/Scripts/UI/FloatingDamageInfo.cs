using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class FloatingDamageInfo : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public bool isUsing = false;
    private void OnEnable()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        //damageText.DOColor(Color.clear, 1);
        damageText.transform.DOMoveY(2, 1);
        Invoke("Off", 1);
    }
    public void Init(int damage, Color color)
    {
        isUsing = true;
        damageText.text = damage.ToString();
        damageText.color = color;
    }

    private void Off()
    {
        isUsing = false;
        gameObject.SetActive(false);
    }
}
