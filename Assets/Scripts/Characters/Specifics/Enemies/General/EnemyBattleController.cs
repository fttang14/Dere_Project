using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleController : BattleController {

    /// <summary>
    /// This script enables enemy AI functionality during battle. It will allow the AI to "decide" on which
    /// character to target, and that instruction will be relayed to BattleManager so the correct interaction
    /// will take place. Also, this is a child of BattleController.
    /// </summary>
    
    /*** VARIABLES ***/

    

    Coroutine co;   //TEMPORARY: this coroutine will serve to have the enemy AI "think" about which target to
                    //choose, but in reality all it does is have the interaction wait for a few seconds before
                    //moving on.


    /*** FUNCTIONS ***/

    protected override void OnEnable()
    {

        base.OnEnable();    //run code from parent script first

        co = StartCoroutine("TestingTimer");    //DEBUGGING: start the coroutine to let the AI "decide" on 
                                                //its target.

    }

    private void Update()
    {
        //if enemy couldn't detect player, it will try again...
        if (onceMore)
        {
            StopCoroutine("TestingTimer");
            onceMore = false;
            StartCoroutine("TestingTimer");
        }
    }

    //DEBUGGING: This coroutine will allow the enemy to "choose" its target to attack
    IEnumerator TestingTimer()
    {
        //Waiting period for the enemy to decide
        Debug.Log("Awaiting Orders...");
        yield return new WaitForSeconds(3f);

        //Enemy will decide who to attack...
        int playerToAttack = Random.Range(0, 4);

        //this boolean will determine if the enemy will have to attack again or not.
        endingTurn = battleManager.EnemyDecision(battleID, playerToAttack);

        yield return null;
    }
}
