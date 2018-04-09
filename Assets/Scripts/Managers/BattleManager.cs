using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    /// <summary>
    /// This script will manage how battles are taken place.
    /// </summary>

    /*** VARIABLES ***/

    bool executeTurn;   //whatever action is going on
    bool turnReady;     //determines if one of the character's is ready to take action
    bool movingOn;    //determines if the battle will continue after character takes action

    Coroutine co;   //the coroutine

    //positions for the characters to be in;
    //all characters should have WAIT as default
    enum Position
    {
        WAIT, INQUEUE, ACTION, ATTACK, DEFEND
    }

    float currentCoolDown;  //current value of the cool down

    //int battleID; //order in which the characters show up in battle

    List<CharacterStats> statistics;    //list of all the characters' stats
    List<CharacterStats> queue;         //when a character's cooldown is up, they will be placed here
    List<GameObject> playersInCombat;   //list of all the player game objects
    List<GameObject> enemiesInCombat;   //list of all the enemy game objects

    EnemyBattleController ebc;  //the enemy's battle controller
    PlayerBattleController pbc; //the player's battle controller

    public CharacterManager characterManager; 	//grabbing the Game Manager script
    public float maxCoolDown = 1.0f;    		//100% ; turn meter ratio
    public float turnRate = 0.4f;   			//determines how fast the turn meter will increment
    public HUDManager HUD;  					//manages the HUD of each character

    /*** FUNCTIONS ***/

    // Use this for initialization
    void Awake()
    {
        executeTurn = false;
        movingOn = false;
        turnReady = false;
        //outOfQueue = false;
        co = null;
        //battleID = 0;
        queue = new List<CharacterStats>();
        statistics = new List<CharacterStats>();
        playersInCombat = new List<GameObject>();
        enemiesInCombat = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        //if any characters are in queue, execute
        //their turns in order
        if (executeTurn && queue.Count > 0)
        {

            //stop cooldown, and let players in queue take action
            StopCoroutine(co);
            LowerOpacity();
            ControllerEnabler();
            executeTurn = false;
        }

        else if (movingOn)
        {
            
            //stop queue, and run cooldown coroutine
            StopCoroutine(co);
            ResetOpacity();
            co = StartCoroutine("AwaitingAction");
            movingOn = false;
        }
    }

    //lower the opacity of all characters if they are not in queue
    void LowerOpacity()
    {
        foreach(CharacterStats cs in statistics)
        {
            //search for all characters who are not in queue
            if(cs.gs_STE != (int)Position.INQUEUE && !cs.gs_DEAD)
            {
                //if the character is a player object
                if (cs.g_SIDE.ToUpper().Equals("PLAYER"))
                {
                    int BID_index = cs.gs_BID;
                    playersInCombat[BID_index].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }

                //if the character is an enemy object
                else if (cs.g_SIDE.ToUpper().Equals("ENEMY"))
                {
                    int BID_index = cs.gs_BID % 4;
                    enemiesInCombat[BID_index].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }
            }
        }
    }

    //reset everyone's opacity after the turn is over
    void ResetOpacity()
    {
        //go through the players first
        foreach(GameObject g in playersInCombat)
        {
            //make sure that the character is NOT dead, therefore is still active
            if (g.activeSelf)
            {
                g.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }

        //then go through the enemies
        foreach(GameObject g in enemiesInCombat)
        {
            //make sure that the character is NOT dead, therefore is still active
            if (g.activeSelf)
            {
                g.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    //enable the controller of the character in queue
    void ControllerEnabler()
    {
        //change position to standby so that character
        //can take their turn
        queue[0].gs_STE = (int)Position.ACTION;

        //if it's a player, search for their battle controller and turn it on
        if (queue[0].g_SIDE.ToUpper().Equals("PLAYER"))
        {
            int BID_index = queue[0].gs_BID;
            playersInCombat[BID_index].GetComponent<PlayerBattleController>().enabled = true;
        }

        //same goes if the character is an enemy
        else if (queue[0].g_SIDE.ToUpper().Equals("ENEMY"))
        {

            int BID_index = queue[0].gs_BID % 4;

            //DEBUGGING
            //Debug.Log("Enemy BID: " + BID_index);

            enemiesInCombat[BID_index].GetComponent<EnemyBattleController>().enabled = true;
        }
    }

    /// <summary>
    /// This function will transfer lists from 
    /// CharacterManager and set up other initial 
    /// information; disable the CharacterManager, 
    /// as nothing will be needed until the next battle...
    /// </summary>
    public void TransferFromGM(List<CharacterStats> r, List<GameObject> po, List<GameObject> eo)
    {
        //adding character stats to a list and setting up the cooldown for each character;
        //randomized for every battle.
        foreach (CharacterStats cs in r)
        {
            //randomize cooldown for each character at the start of battle
            currentCoolDown = Random.Range(0.0f, 0.5f);
            cs.gs_TM = currentCoolDown;

            cs.gs_DEF = 0;  //DEBUGGING: just to see pure damage output

            //Setting up the battle controllers for players and enemies...
            if (cs.g_SIDE.Equals("Player"))
            {
                int index = cs.gs_BID;
                po[index].GetComponent<PlayerBattleController>().gs_BID = index;
                playersInCombat.Add(po[index]);
            }

            else if (cs.g_SIDE.Equals("Enemy"))
            {
                int index = cs.gs_BID % 4;
                eo[index].GetComponent<EnemyBattleController>().gs_BID = index;
                enemiesInCombat.Add(eo[index]);
            }

            //finally, add the character into the overall roster
            statistics.Add(cs);
        }

        //turning off characterManager until battle is over
        characterManager.enabled = false;

        //activating the HUDs for each character
        HUD.ActivateHUD(statistics);

        //DEBUG
        //DisplayRoster_Test();

        // The coroutine will start when all the characters are in the roster
        co = StartCoroutine("AwaitingAction");
    }

    /// <summary>
    /// This function will receive input from Player Controller,
    /// and decide which enemy to attack.
    /// NOTE: THIS IS JUST FOR TESTING THE BATTLE SYSTEM.
    /// THIS WILL CHANGE LATER.
    /// </summary>
    public void PlayerDecision(int player, int enemy)
    {
        bool enemyFound = false; //this determines if the enemy still exists or not...
        int BID_enemy = enemy + 4;  //there are 4 players total, and enemies come after
        int BID_player = player;

        //search for the enemy with the given ID provided by
        //the decide input, provided that they are not dead already
        if(enemy >= enemiesInCombat.Count)
        {
            Debug.Log("Index out of bounds. Try again...");
        }

        else
        {
            if (!statistics[BID_enemy].gs_DEAD)
            {
                Debug.Log("Player has attacked " + statistics[BID_enemy].g_NAME + " !!!");
                //DAMAGE CALCULATION HERE
                PlayerAttacking(BID_player, BID_enemy);

                enemyFound = true;
            }

            //if the enemy has been found, attack that enemy!
            if (enemyFound)
            {

                queue[0].gs_TM = 0;
                playersInCombat[BID_player].GetComponent<PlayerBattleController>().enabled = false;

                queue.RemoveAt(0);
                if (queue.Count <= 0) { movingOn = true; turnReady = false; }
                else { executeTurn = true; }
            }

            else
            {
                Debug.Log("Enemy could not be found. Please try again...");
            }
        }
        
    }

    /// <summary>
    /// This function will receive input from Enemy Controller,
    /// and decide which player to attack.
    /// NOTE: THIS IS JUST FOR TESTING THE BATTLE SYSTEM.
    /// THIS WILL CHANGE LATER.
    /// </summary>
    public bool EnemyDecision(int enemy, int player)
    {
        bool playerFound = false; //this determines if the enemy still exists or not...
        int BID_player = player;  //there are 4 players total, and enemies come after
        //int BID_enemy = enemy % 4;

        //search for the enemy with the given ID provided by
        //the decide input, provided that they are not dead already
        if (!statistics[BID_player].gs_DEAD)
        {
            Debug.Log("Enemy has attacked " + statistics[BID_player].g_NAME + " !!!");

            //DAMAGE CALCULATION HERE
            EnemyAttacking(enemy, BID_player);
            playerFound = true;
        }

        //if the player has been found, attack that player!
        if (playerFound)
        {

            queue[0].gs_TM = 0;
            //enemiesInCombat[BID_enemy].GetComponent<EnemyBattleController>().enabled = false;

            queue.RemoveAt(0);
            if (queue.Count <= 0) { movingOn = true; turnReady = false; }
            else { executeTurn = true; }

            return true;
        }

        else
        {
            Debug.Log("Target could not be found. Retrying...");

            return false;
        }
    }

    //This function displays the full roster
    //JUST FOR DEBUGGING PURPOSES
    public void DisplayRoster_Test()
    {
        /*
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
                        ", BID: " + cs.gs_BID +
                        ", TM: " + cs.gs_TM + 
                        ", POS: " + cs.gs_POS.position);
        }
        */

        //DEBUGGING
        //Debug.Log("Players in Combat: " + playersInCombat.Count);
        //Debug.Log("Enemies in Combat: " + enemiesInCombat.Count);
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

    //this function will increment the cool down timer;
    //applied to Update
    void CoolDownTimer()
    {
        //returning nothing if there are no stats in the list
        if (statistics.Count <= 0) return;

        //else, continue the code
        for (int cs_i = 0; cs_i < statistics.Count; cs_i++)
        {
            //if cooldown is at max, add them to the queue
            if (statistics[cs_i].gs_TM >= maxCoolDown)
            {
                statistics[cs_i].gs_TM = 1.0f;
                statistics[cs_i].gs_STE = (int)Position.INQUEUE;
                queue.Add(statistics[cs_i]);
                turnReady = true;
            }

            //update everyone's cooldown rate
            else if(!statistics[cs_i].gs_DEAD)
            {
                statistics[cs_i].gs_TM += turnRate * Time.deltaTime;
            }

            //update the cooldown timer
            HUD.UpdatingCD(statistics[cs_i].gs_TM, cs_i);
        }
    }

    //Damage output from player to enemy
    //p = player id, e = enemy id
    void PlayerAttacking(int p, int e)
    {
        //int pATK = 0, eDEF = 0, dmg = 0;
        int player_ATK = 0, enemy_DEF = 0, damage = 0;

        //get the player's attack power
        player_ATK = statistics[p].gs_ATK;

        //get the enemy's defense power
        enemy_DEF = statistics[e].gs_DEF;

        //calculate total damage
        damage = (player_ATK - enemy_DEF) > 0 ? (player_ATK - enemy_DEF) : 0;

        //apply damage to enemy's health
        statistics[e].gs_HP -= damage;


        //determine if the enemy is dead
        if (statistics[e].gs_HP <= 0)
        {
            statistics[e].gs_HP = 0;
            statistics[e].gs_DEAD = true;

            //disable the character once dead
            WhoIsDead(statistics[e]);
        }

        //Updating the health value
        HUD.UpdatingHP(statistics[e].gs_HP, e);

    }

    //Damage output from enemy to player
    void EnemyAttacking(int e, int p)
    {
        //since enemies are on the later half of the roster
        //add 4 to their index
        int enemy_ATK = statistics[e].gs_ATK;

        int player_DEF = statistics[p].gs_DEF;

        //calculating damage output based on enemy's attack power
        //and player character's defense value
        int dmg = (enemy_ATK - player_DEF) > 0 ? (enemy_ATK - player_DEF) : 0;

        //calculate the health loss, and then update it in HUD
        statistics[p].gs_HP -= dmg;

        //if the character loses all HP, label them dead
        if (statistics[p].gs_HP <= 0)
        {
            statistics[p].gs_HP = 0;
            statistics[p].gs_DEAD = true;
        }

        //Updating the health value
        HUD.UpdatingHP(statistics[p].gs_HP, p);
    }

    
    //Find out who is dead, and remove them from the roster
    void WhoIsDead(CharacterStats cs)
    {
        //determine if this character is a Player or an Enemy
        if (cs.g_SIDE.Equals("Player"))
        {
            playersInCombat[cs.gs_BID].SetActive(false);
        }

        else if (cs.g_SIDE.Equals("Enemy"))
        {
            enemiesInCombat[cs.gs_BID % 4].SetActive(false);
        }
    }
    
}
