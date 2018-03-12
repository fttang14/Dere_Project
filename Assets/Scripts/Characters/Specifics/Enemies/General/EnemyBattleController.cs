using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleController : BattleController {

    /* 
     * This script will determine enemy AI during battle, 
     * making decisions based on particular algorithms for
     * enemy... ; inheriting from Battle Controller
     */

    bool onceMore;
    Coroutine co;

    private void OnEnable()
    {
        Debug.Log("This is " + gameObject.name + " and this controller's ID is: " + userID);
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        //DEBUGGING
        onceMore = false;
        co = StartCoroutine("TestingTimer");
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

    //DEBUGGING
    IEnumerator TestingTimer()
    {
        Debug.Log("Awaiting Orders...");
        yield return new WaitForSeconds(3f);

        //Enemy will decide who to attack...
        int playerToAttack = Random.Range(1, 4);

        Debug.Log("Done!");
        onceMore = battleManager.EnemyDecision(userID, playerToAttack);
        yield return null;
    }

    //setting up the id for each character
    public override void SetupBID(int bid)
    {
        base.SetupBID(bid);
        userID = bid;
    }
}
