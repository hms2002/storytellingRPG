using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootTectacles : Monster
{
    private ForestMaster forestMaster;

    public void Init(ForestMaster _forestMaster)
    {
        forestMaster = _forestMaster;
    }
}
