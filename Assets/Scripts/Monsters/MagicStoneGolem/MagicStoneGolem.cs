using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStoneGolem : Actor
{
    private int _stonePiece = 0;

    public int stonePiece
    {
        get { return _stonePiece; }
        set { _stonePiece = value; }
    }

}
