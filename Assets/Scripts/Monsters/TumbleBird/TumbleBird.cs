using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleBird : Actor
{
    private int _glassFragmentStack = 0;

    public int glassFragmentStack
    {
        get { return _glassFragmentStack; }
        set
        {
            _glassFragmentStack = value;
            stateUIController.GlassFragmentOn(_glassFragmentStack);
        }
    }

}
