using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour {


    /// <summary>
    /// This script will provide the generic functions during battle for players
    /// and enemies to inherit
    /// </summary>
    
    /*** VARIABLES ***/
    
    protected BattleManager battleManager;  //the battle manager

    protected bool endingTurn;    //once active, the turn will end, and the battle will progress
    protected bool onceMore;    //for EnemyBattleController; lets the AI know if it can take action again.

    protected bool takeAction;  //prevent the player from abusing key presses
                                //and only take action when ready

    protected Coroutine test;   //DEBUGGING: timer for the character's turn to end

    protected int battleID;   //determine whose turn it is...

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

    /*** FUNCTIONS ***/

    /*
     * disable the battle controller for everyone at the beginning, or else everyone's turn
     * will come up right away and potentially crash the game...
     */
    protected void Awake()
    {
        endingTurn = false;   //no one's taking more than one turn at a time
        onceMore = false;    //waits if the enemy made an error on deciding a target
        takeAction = false; //no one's turn should be going up at the start

        battleID = 0;   //all battle controllers have 0 as their ID, since it will be
                        //adjusted by the Battle Manager

        test = null;    //it's no one's turn at startup, so the coroutine shouldn't be
                        //running

        enabled = false;
    }

    //DEBUGGING: IEnumerator - a timer that pauses the game after the character finishes
    //its turn.
    protected IEnumerator FinishTurn()
    {
        Debug.Log("Ending Turn...");

        yield return new WaitForSeconds(1.0f);
    }


    //once enabled, this function will display whose turn it is (DEBUGGING purposes). Also, it will find 
    //the BattleManager script within the BattleManager game object so that the battle controller can access 
    //its functionality. Child scripts will be allowed to override this function for their own purposes.
    protected virtual void OnEnable()
    {
        Debug.Log("This is " + gameObject.name + " and this controller's ID is: " + battleID);
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    //get and set the battle id of each battle controller
    public int gs_BID
    {
        get { return battleID; }
        set { battleID = value; }
    }


}
