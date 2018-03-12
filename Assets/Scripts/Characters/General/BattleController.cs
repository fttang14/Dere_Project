using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {

    /*
     * This script will provide the generic functions
     * during battle for players and enemies to inherit.
     */
    
    protected BattleManager battleManager;  //the battle manager

    protected bool takeAction;  //prevent the player from abusing key presses
                                //and only take action when ready

    protected int turnNumber_test;   //TESTING; seeing where errors occur

    //positions for the characters to be in;
    //all characters should have WAIT as default
    protected enum Position
    {
        WAIT, INQUEUE, ACTION, ATTACK, DEFEND
    }

    //picks which enemy in what position to attack
    protected enum Order
    {
        FIRST, SECOND, THIRD, FOURTH
    }

    //disable the battle controller for everyone
    //at the beginning, or else everyone's turn
    //will come up right away and potentially
    //crash the game...
    protected void Awake()
    {
        turnNumber_test = 0;
        takeAction = false;
        this.enabled = false;
    }
}
