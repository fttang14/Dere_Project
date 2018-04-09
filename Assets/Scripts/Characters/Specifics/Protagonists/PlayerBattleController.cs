using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleController : BattleController {


    /// <summary>
    /// This script will allow UI with the player characters. Depending on the user's decisions, their inputs
    /// will transfer to BattleManager so that the correct interactions are chosen. This is also a child of
    /// BattleController
    /// </summary>

    /*** VARIABLES ***/
    bool playerTakingAction = false; //determines if the player has taken action yet

    Button attack, skill, defend, surprise; //the buttons for the player
    List<Button> enemyTargets;  //the buttons for deciding which enemy to attack

    GameObject actions; //the action buttons game object retrieved from HUD Manager 

    HUDManager hudManager;  //the HUD Manager


    /*** FUNCTIONS ***/

    protected override void OnEnable()
    {
        base.OnEnable(); //run code from parent script first

        //DEBUGGING: just a letting the player know how to attack
        Debug.Log("Please press '1', '2', '3', or '4' to attack a target...");

        //Retrieve the buttons from the HUD Manager
        hudManager = GameObject.Find("HUDManager").GetComponent<HUDManager>();

        //Setup the actions for the player
        actions = hudManager.ActivateActions(battleID);
        actions.name = gameObject.name + "Action";

        //Setting up the buttons for the player, and then renaming the text within each button
        attack = actions.transform.Find("AttackButton").GetComponent<Button>();
        attack.GetComponentInChildren<Text>().text = "Attack";

        skill = actions.transform.Find("SkillButton").GetComponent<Button>();
        skill.GetComponentInChildren<Text>().text = "Skill";

        defend = actions.transform.Find("DefendButton").GetComponent<Button>();
        defend.GetComponentInChildren<Text>().text = "Defend";

        surprise = actions.transform.Find("SurpriseButton").GetComponent<Button>();
        surprise.GetComponentInChildren<Text>().text = "Surprise!";

        //player has not taken any action yet
        playerTakingAction = false;

        //let the player click on the options available
        attack.onClick.AddListener(Attacking);
        skill.onClick.AddListener(Skills);
        defend.onClick.AddListener(Defending);
        surprise.onClick.AddListener(Surprise);
    }

    private void Update()
    {

        //Using the buttons created to determine the player's course of action
        /*
        if (!playerTakingAction)
        {
            //let the player click on the options available
            
            playerTakingAction = true;
        }
        */

        //TESTING: seeing if the turn-based mechanism is working properly. Depending on which key is pressed,
        //that input will then be relayed back to a function in BattleManager, along with this controller's
        //battle ID.

        /*
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
        */

    }

    /// <summary>
    /// creating four functions that deal with attacking, skills, defending, and surprise!
    /// </summary>

    //ATTACKING
    void Attacking()
    {
        
        Debug.Log("I am attacking");
    }

    //USING SKILLS
    void Skills()
    {
        Debug.Log("I am using a skill");
    }

    //DEFENDING
    void Defending()
    {
        Debug.Log("I am defending");
    }

    //SURPRISE!!!
    void Surprise()
    {
        Debug.Log("I am doing a surprise move!");
    }
}
