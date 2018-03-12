using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{

    /*
     * This script will manage how battles will be taking place.
    */

    bool executeTurn;   //whatever action is going on
    bool turnReady;     //determines if one of the character's is ready to take action
    bool outOfQueue;    //determine if character is no longer in queue
    bool movingOn;    //determines if the battle will continue after character takes action

    Coroutine co;   //the coroutine

    //positions for the characters to be in;
    //all characters should have WAIT as default
    enum Position
    {
        WAIT, INQUEUE, ACTION, ATTACK, DEFEND
    }

    float currentCoolDown;  //current value of the cool down

    List<CharacterStats> statistics;    //list of all the characters' stats
    List<CharacterStats> queue;         //when a character's cooldown is up, they will be placed here
    List<GameObject> playersInCombat;   //list of all the player game objects
    List<GameObject> enemiesInCombat;   //list of all the enemy game objects

    EnemyBattleController ebc;  //the enemy's battle controller
    PlayerBattleController pbc; //the player's battle controller

    public CharacterManager characterManager; //grabbing the Game Manager script
    public float maxCoolDown = 1.0f;    //100% ; turn meter ratio
    public float turnRate = 0.4f;   //determines how fast the turn meter will increment
    public HUDManager HUD;  //manages the HUD of each character

    // Use this for initialization
    void Awake()
    {
        executeTurn = false;
        movingOn = false;
        turnReady = false;
        outOfQueue = false;
        co = null;
        queue = new List<CharacterStats> ();
        statistics = new List<CharacterStats>();
        playersInCombat = new List<GameObject>();
        enemiesInCombat = new List<GameObject>();

    }

    // The start function will start the ienumerator and
    // coroutine for the battle...
    void Start()
    {
        co = StartCoroutine("AwaitingAction");
    }

    // Update is called once per frame
    void Update()
    {
        //if any characters are in queue, execute
        //their turns in order
        if (executeTurn)
        {
            //stop cooldown, and run queue coroutine
            StopCoroutine(co);
            co = StartCoroutine("CharacterPhase");
            executeTurn = false;
        }

        else if (movingOn)
        {
            //stop queue, and run cooldown coroutine
            StopCoroutine(co);
            co = StartCoroutine("AwaitingAction");
            movingOn = false;
        }
    }

    //this function will increment the cool down timer;
    //applied to Update
    void CoolDownTimer()
    {
        //returning nothing if there are no stats in the list
        if (statistics.Count <= 0) return;

        //else, continue the code
        foreach(CharacterStats c in statistics)
        {

            //if cooldown is at max, add them to the queue
            if(c.gs_TM >= maxCoolDown)
            {
                c.gs_TM = 1.0f;
                c.gs_STE = (int)Position.INQUEUE;
                queue.Add(c);
                turnReady = true;
            }

            //update everyone's cooldown rate
            else
            {   
                c.gs_TM += turnRate * Time.deltaTime;
            }
        }
    }

    //enable the controller of the character in queue
    void ControllerEnabler()
    {
        //change position to standby so that character
        //can take their turn
        queue[0].gs_STE = (int)Position.ACTION;

        //if it's a player, search for their battle
        //controller and turn it on
        if (queue[0].g_SIDE.ToUpper().Equals("PLAYER"))
        {
            foreach(GameObject g in playersInCombat)
            {
                if (g.name.ToUpper().Contains(queue[0].g_NAME.ToUpper()))
                {
                    g.GetComponent<PlayerBattleController>().enabled = true;
                }
            }
        }

        //same goes if the character is an enemy
        else if (queue[0].g_SIDE.ToUpper().Equals("ENEMY"))
        {
            foreach (GameObject g in enemiesInCombat)
            {
                if (g.name.ToUpper().Contains(queue[0].g_NAME.ToUpper()) &&
                    g.transform.position.Equals(queue[0].gs_POS.position))
                {
                    g.GetComponent<EnemyBattleController>().enabled = true;
                }
            }
        }

        outOfQueue = true;
    }

    /// <summary>
    /// This function will transfer lists from 
    /// CharacterManager and set up other initial 
    /// information; disable the CharacterManager, 
    /// as nothing will be needed until the next battle...
    /// </summary>
    public void TransferFromGM(List<CharacterStats> r, List<GameObject> po, List<GameObject> eo)
    {
        //adding character stats to a list and setting up
        //the cooldown for each character; randomized for
        //every battle...
        foreach (CharacterStats c in r)
        {
            currentCoolDown = Random.Range(0.0f, 0.5f);
            c.gs_TM = currentCoolDown;
            statistics.Add(c);
        }

        //all player characters will be entered into the
        //playersInCombat list...
        foreach (GameObject g in po)
        {
            playersInCombat.Add(g);

        }

        //Same thing as above, except for enemies
        foreach (GameObject g in eo)
        {
            enemiesInCombat.Add(g);

        }

        //turning off characterManager until battle is over
        characterManager.enabled = false;

        //activating the HUDs for each character
        HUD.ActivateHUD(statistics);
    }

    /// <summary>
    /// This function will receive input from Player Controller,
    /// and decide which enemy to attack.
    /// NOTE: THIS IS JUST FOR TESTING THE BATTLE SYSTEM.
    /// THIS WILL CHANGE LATER.
    /// </summary>
    public void playerDecision(int decide)
    {
        if(decide == 0)
        {
            queue[0].gs_TM = 0;
            foreach(GameObject g in playersInCombat)
            {
                if (g.name.ToUpper().Contains(queue[0].g_NAME.ToUpper()))
                {
                    g.GetComponent<PlayerBattleController>().enabled = false;
                }
            }
            queue.RemoveAt(0);
            if(queue.Count <= 0) { movingOn = true; turnReady = false; }
            outOfQueue = false;
        }
    }

    /// <summary>
    /// This function will receive input from Enemy Controller,
    /// and decide which player to attack.
    /// NOTE: THIS IS JUST FOR TESTING THE BATTLE SYSTEM.
    /// THIS WILL CHANGE LATER.
    /// </summary>
    public void enemyDecision(int decide)
    {
        if(decide == 0)
        {
            queue[0].gs_TM = 0;
            foreach (GameObject g in enemiesInCombat)
            {
                if (g.name.ToUpper().Contains(queue[0].g_NAME.ToUpper()) &&
                    g.transform.position.Equals(queue[0].gs_POS.position))
                {
                    g.GetComponent<EnemyBattleController>().enabled = false;
                }
            }
            queue.RemoveAt(0);
            if(queue.Count <= 0) { movingOn = true; turnReady = false; }
            outOfQueue = false;
        }
    }

    //if the spot that the character selected contains
    //no enemy, let the player decide again

    //This function displays the full roster
    //JUST FOR DEBUGGING PURPOSES
    public void DisplayRoster_Test()
    {
        foreach (CharacterStats cs in statistics)
        {
            Debug.Log("NAME: " + cs.g_NAME +
                        ", SIDE: " + cs.g_SIDE +
                        ", HP: " + cs.gs_HP +
                        ", SP: " + cs.gs_SP +
                        ", ATK: " + cs.gs_ATK +
                        ", DEF: " + cs.gs_DEF +
                        ", SPD: " + cs.gs_SPD +
                        ", LUK: " + cs.gs_LUK +
                        ", STE: " + cs.gs_STE +
                        ", EXM: " + cs.gs_EXM +
                        ", EXP: " + cs.gs_EXP +
                        ", FORM: " + cs.gs_FORM +
                        ", TM: " + cs.gs_TM + 
                        ", POS: " + cs.gs_POS.position);
        }
    }

    //manages timers during battle
    IEnumerator AwaitingAction()
    {
        //run the cool down timer while no one has
        //max cooldown yet
        while (!turnReady)
        {
            CoolDownTimer();
            yield return null;
        }

        //let the characters in queue take their turn
        //Debug.Log("Queue Count: " + queue.Count);
        executeTurn = true;
        yield return null;

    }

    //let the characters in queue take their turns
    IEnumerator CharacterPhase()
    {
        //as long as the queue is not empty
        //and the front of the queue is not
        //deciding on an action yet, activate
        //their battle controller
        while(queue.Count > 0 && !outOfQueue)
        {
            //let whoever is in front of the queue
            //take their turn
            ControllerEnabler();
            yield return null;
        }

        yield return null;
    }
}
