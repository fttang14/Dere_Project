using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    /*
     * This script will manage how battles will be taking place.
    */

    bool turnReady; //determines if one of the character's is ready to take action

    Coroutine co;   //the coroutine

    //positions for the characters to be in;
    //all characters should have WAIT as default
    enum Position
    {
        WAIT, ACTION, ATTACK, DEFEND
    }

    float currentCoolDown;  //current value of the cool down

    List<CharacterStats> statistics;    //list of all the characters' stats
    List<GameObject> playersInCombat;   //list of all the player game objects
    List<GameObject> enemiesInCombat;   //list of all the enemy game objects

    EnemyBattleController ebc;  //the enemy's battle controller
    PlayerBattleController pbc; //the player's battle controller

    public CharacterManager characterManager; //grabbing the Game Manager script
    public float maxCoolDown = 1.0f;    //100% ; turn meter ratio
    public float turnRate = 0.4f;   //determines how fast the turn meter will increment

    // Use this for initialization
    void Awake()
    {
        turnReady = false;
        statistics = new List<CharacterStats>();
        playersInCombat = new List<GameObject>();
        enemiesInCombat = new List<GameObject>();
    }

    // The start function will start the ienumerator and
    // coroutine for the battle...
    void Start()
    {
        co = StartCoroutine("BattleInProgress");
    }

    // Update is called once per frame
    void Update()
    {
        //when someone's turn is ready, stop the coroutine
        //and find out whose turn it is
        if (turnReady)
        {
            StopCoroutine(co);
            CheckForAction();
        }
    }

    //this function will increment the cool down timer;
    //applied to Update
    void CoolDownTimer()
    {
        //if the list hasn't been populated yet, do nothing
        if (statistics.Count == 0) return;

        foreach (CharacterStats cs in statistics)
        {
            currentCoolDown = cs.gs_TM;

            //if the cooldown has reached the maximum,
            //then establish that it is that character's turn
            if (currentCoolDown >= maxCoolDown && !turnReady)
            {
                cs.gs_POS = (int)Position.ACTION;
                turnReady = true;
            }

            //update cooldown time if it hasn't reached
            //maximum cooldown yet...
            else
            {
                float coolDownBoost = (float)cs.gs_SPD * 0.001f;
                currentCoolDown += coolDownBoost + (turnRate * Time.deltaTime);
                cs.gs_TM = currentCoolDown;
            }
        }
    }

    void CheckForAction()
    {
        foreach(CharacterStats characterReady in statistics)
        {
            if(characterReady.gs_POS == (int)Position.ACTION) {
                //go through the character lists to find out
                //who is ready to take action, and then enable
                //their script...

            }
        }
    }

    /*
     * This function will transfer lists from CharacterManager
     * and set up other initial information; disable the
     * CharacterManager, as nothing will be needed until the
     * next battle...
     */
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
        //playersInCombat list; disabled controllers will
        //be added as well, so that no one can take action
        //until their turn is up...
        foreach (GameObject g in po)
        {
            g.GetComponent<PlayerBattleController>().enabled = false;
            playersInCombat.Add(g);

        }

        //Same thing as above, except for enemies
        foreach (GameObject g in eo)
        {
            g.GetComponent<EnemyBattleController>().enabled = false;
            enemiesInCombat.Add(g);

        }

        characterManager.enabled = false;

        //DEBUGGING
        DisplayRoster();
    }

    //This function displays the full roster
    //JUST FOR DEBUGGING PURPOSES
    public void DisplayRoster()
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
                        ", POS: " + cs.gs_POS +
                        ", EXM: " + cs.gs_EXM +
                        ", EXP: " + cs.gs_EXP +
                        ", FORM: " + cs.gs_FORM +
                        ", OID: " + cs.gs_OID +
                        ", TM: " + cs.gs_TM);
        }
    }

    //manages timers during battle
    IEnumerator BattleInProgress()
    {
        while (!turnReady)
        {
            CoolDownTimer();
            yield return null;
        }
    }
}
