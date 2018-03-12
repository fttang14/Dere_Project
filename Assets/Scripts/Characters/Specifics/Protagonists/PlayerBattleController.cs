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
        Debug.Log("This is " + gameObject.name + " and it is my " + turnNumber_test++ + " turn!");
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    private void Update()
    {
        //TESTING
        //seeing if the turn-based mechanism is working properly
        if (Input.GetKeyDown(KeyCode.Space))
        {
            battleManager.playerDecision(0);
        }
    }
}
