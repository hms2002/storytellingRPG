using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateUIDatabase : MonoBehaviour
{

    public static StateUIDatabase stateUIDB;

    public GameObject stateUIPrefab;
    public GameObject stateInfoUI;
    private void Awake()
    {
        if (stateUIDB != null)
            Destroy(this);
        stateUIDB = this;
    }

}
