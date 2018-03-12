using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleController : BattleController {

    /* 
     * This script will allow UI with the player characters.
     * Depending on the user's inputs and decisions, those
     * inputs will transfer to BattleManager so that the
     * correct interactions are chosen... ; inheriting from
     * Battle Controller
     */

    private void OnEnable()
    {
        
        Debug.Log("This is " + gameObject.name + " and this controller's ID is: " + userID);
        Debug.Log("Please press '1', '2', '3', or '4' to attack a target...");
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

    }

    private void Update()
    {
        //TESTING
        //seeing if the turn-based mechanism is working properly

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            battleManager.PlayerDecision(userID, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            battleManager.PlayerDecision(userID, 2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            battleManager.PlayerDecision(userID, 3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            battleManager.PlayerDecision(userID, 4);
        }

    }

    //setting up the id for each character
    public override void SetupBID(int bid)
    {
        base.SetupBID(bid);
        userID = bid;
    }
}
