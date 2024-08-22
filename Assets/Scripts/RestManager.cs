using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RestManager : MonoBehaviour
{
    public static List<Button> btnList = new List<Button>();
    public static void allBtnOff()
    {
        foreach(Button b in  btnList)
        {
            b.enabled = false;
        }
        btnList.Clear();
    }
}
