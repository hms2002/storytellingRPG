using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateUIDatabase : MonoBehaviour
{

    public static StateUIDatabase stateUIDB;

    public GameObject burn;
    public GameObject weaken;
    public GameObject reduction;
    public GameObject glassFragment;
    public GameObject treasureOfDragon;
    public GameObject callingOfMommy;
    private void Awake()
    {
        if (stateUIDB != null)
            Destroy(this);
        stateUIDB = this;
    }
    enum STATE_UI_INDEX
    {
        PROTECT = 0,        // 보호막
        BURN,               // 화상
        WEAKEN,             // 맞을 때 더 아프게 맞음
        REDUCTION,          // 때릴 때 더 약해하게 때림
        GLASS_FRAGMENT,     // 맞으면 스택만큼 반사
        TREASURE_OF_DRAGON, // 용의 보물
        CALLING_OF_MOMMY,   // 어미 용의 부름
        END_INDEX
    }

}
