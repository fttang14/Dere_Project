using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleController : BattleController {

    /* 
     * This script will determine enemy AI during battle, 
     * making decisions based on particular algorithms for
     * enemy... ; inheriting from Battle Controller
     */

    Coroutine co;

    private void OnEnable()
    {
        Debug.Log("This is " + gameObject.name + " and it is my " + turnNumber_test++ + " turn!");
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        //DEBUGGING
        co = StartCoroutine("TestingTimer");
    }

    //DEBUGGING
    IEnumerator TestingTimer()
    {
        Debug.Log("Awaiting Orders...");
        yield return new WaitForSeconds(3f);
        Debug.Log("Done!");
        battleManager.enemyDecision(0);
        yield return null;
    }
}
