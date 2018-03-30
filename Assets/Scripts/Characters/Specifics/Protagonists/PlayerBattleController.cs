using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleController : BattleController {


    /// <summary>
    /// This script will allow UI with the player characters. Depending on the user's decisions, their inputs
    /// will transfer to BattleManager so that the correct interactions are chosen. This is also a child of
    /// BattleController
    /// </summary>

    /*** FUNCTIONS ***/

    protected override void OnEnable()
    {
        base.OnEnable(); //run code from parent script first

        //DEBUGGING: just a letting the player know how to attack
        Debug.Log("Please press '1', '2', '3', or '4' to attack a target...");
        
    }

    private void Update()
    {
        //TESTING: seeing if the turn-based mechanism is working properly. Depending on which key is pressed,
        //that input will then be relayed back to a function in BattleManager, along with this controller's
        //battle ID.

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            battleManager.PlayerDecision(battleID, (int)Order.FIRST);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            battleManager.PlayerDecision(battleID, (int)Order.SECOND);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            battleManager.PlayerDecision(battleID, (int)Order.THIRD);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            battleManager.PlayerDecision(battleID, (int)Order.FOURTH);
        }

    }


}
