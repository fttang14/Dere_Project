using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    /*
     * This script will provide the generic functions
     * during battle for players and enemies to inherit.
     */

    
    //positions for the characters to be in;
    //all characters should have WAIT as default
    protected enum Position
    {
        WAIT, ACTION, ATTACK, DEFEND
    }

    
}
