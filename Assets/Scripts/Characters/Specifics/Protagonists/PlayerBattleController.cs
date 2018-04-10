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

        //let the player click on the options available
        attack.onClick.AddListener(Attacking);
        skill.onClick.AddListener(Skills);
        defend.onClick.AddListener(Defending);
        surprise.onClick.AddListener(Surprise);

        actions.SetActive(true); //allow the player to take action

        //reset the enemy Targets every time, in case that there is a change in enemy count
        enemyTargets = new List<Button>();
    }

    //Reset everything in the controller
    private void OnDisable()
    {
        /*
        attack.enabled = false;
        attack = null;

        skill.enabled = false;
        skill = null;

        defend.enabled = false;
        defend = null;

        surprise.enabled = false;
        surprise = null;
        */

        
        hudManager = null;

        Destroy(actions);

        attack = null;
        defend = null;
        skill = null;
        surprise = null;
        
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
        //send a BID to HUDManager, and if there is a match, have the HUD manager return a reference to
        //the button with the corresponding BID
        for(int bid_i = 0; bid_i < 4; bid_i++)
        {
            Button buttonTemp = hudManager.EnemyTargets(bid_i);

            //correspond the correct attacking function with the bid_i
            if (buttonTemp != null)
            {
                switch (bid_i)
                {
                    case (int)Order.FIRST:
                        buttonTemp.onClick.AddListener(AttackOne);
                        break;
                    case (int)Order.SECOND:
                        buttonTemp.onClick.AddListener(AttackTwo);
                        break;
                    case (int)Order.THIRD:
                        buttonTemp.onClick.AddListener(AttackThree);
                        break;
                    case (int)Order.FOURTH:
                        buttonTemp.onClick.AddListener(AttackFour);
                        break;
                }
                enemyTargets.Add(buttonTemp);
            }
        }

        //once the enemy targets have been set up, deactivate player buttons
        Debug.Log("enemyTargets: " + enemyTargets.Count);
        actions.SetActive(false);
    }

    //These four functions will determine which enemy is being attacked...
    void AttackOne()
    {
        Debug.Log("Attacking Enemy One!");
        hudManager.TurnOffEnemyTargets();
        battleManager.PlayerDecision(battleID, (int)Order.FIRST);
    }

    void AttackTwo()
    {
        Debug.Log("Attacking Enemy Two!");
        hudManager.TurnOffEnemyTargets();
        battleManager.PlayerDecision(battleID, (int)Order.SECOND);
    }

    void AttackThree()
    {
        Debug.Log("Attacking Enemy Three!");
        hudManager.TurnOffEnemyTargets();
        battleManager.PlayerDecision(battleID, (int)Order.THIRD);
    }

    void AttackFour()
    {
        Debug.Log("Attacking Enemy Four!");
        hudManager.TurnOffEnemyTargets();
        battleManager.PlayerDecision(battleID, (int)Order.FOURTH);
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
