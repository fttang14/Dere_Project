using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    /*
     * This script will create the roster for the characters
     * and send it to the battle manager
     */

    bool transferReady; //key to determine whether to transfer
                        //data to the battle manager

    int orderID;    //the order in which the characters are 
                    //registered in the roster
                    

    List<CharacterStats> roster;    //list of characters entering battle
    List<GameObject> playerObjects; //list of all players entering battle
    List<GameObject> enemyObjects;  //list of all enemies entering battle

    public AllCharacterList allCharacters; //list of all characters ever created
    public GameObject battleManager;    //the battle manager game object
    public GameObject backgroundCheck;  //a the background game object
    public List<Transform> playerSpawn; //list of all player spawn points
    public List<Transform> enemySpawn;  //list of all enemy spawn points

    //Initializing all private variables first
    private void Awake()
    {
        transferReady = false;
        orderID = 0;
        roster = new List<CharacterStats>();
        playerObjects = new List<GameObject>();
        enemyObjects = new List<GameObject>();
        battleManager.SetActive(false); //activating the battle manager once
                                        //all the characters have been
                                        //added to their proper lists...
    }

    //Initializing the scene with all the startup functions
    private void Start()
    {
        InstantiateCharacters();    //instantiating all the characters
                                    //who will participate in battle

        AddPlayerToRoster();  //adding all the instantiated player characters to the
                              //roster so that they can be added to the
                              //battle manager to handle...

        AddEnemyToRoster(); //adding all the instantiated enemy characters to the
                            //roster so that they can be added to the
                            //battle manager to handle...

        /*
        DisplayRoster();    //this is just for DEBUGGING, just to display
                            //all the character stats in the roster
        */

        battleManager.SetActive(true);  //once everything has been set up,
                                        //activate the battle manager
        

        //transfer data from Character to Battle Manager
        BattleManager bm = battleManager.GetComponent<BattleManager>();
        bm.TransferFromGM(roster, playerObjects, enemyObjects);
    }

    //This function will instantiate all the characters for battle
    public void InstantiateCharacters()
    {
        int index = 0;

        //instantiate the player characters first
        foreach (GameObject g in allCharacters.Players)
        {
            //adding instantiated objects to game object list
            //so that their stats can be added to the 'roster'
            //CharacterStats list later...
            playerObjects.Add(Instantiate(g, playerSpawn[index]));
            playerObjects[index].transform.SetParent(GameObject.Find("CharacterRoster").transform);
            index++;
        }

        /****** Now we instantiate the enemies ******/

        //First, we determine how many enemies to spawn
        int howManyEnemies = Random.Range(1, enemySpawn.Count+1);

        //Second, determine from which list we will choose from,
        //based on the background...
        string backgroundName = backgroundCheck.name.ToUpper();

        //Third, we decide which enemies to spawn
        //depending on how many enemies to spawn
        //and the background's name
        if (backgroundName.Equals("BATTLEBACKGROUNDTEST"))
        {
            
            for (int i = 0; i < howManyEnemies; i++)
            {
                int decision = Random.Range(0, allCharacters.EnemiesTest.Count);
                GameObject temp = allCharacters.EnemiesTest[decision];

                //adding instantiated objects to game object list
                //so that their stats can be added to the 'roster'
                //CharacterStats list later...
                enemyObjects.Add(Instantiate(temp, enemySpawn[i]));
                enemyObjects[i].transform.SetParent(GameObject.Find("CharacterRoster").transform);
            }
        }
    }

    //This function adds the player characters to the roster in battle
    public void AddPlayerToRoster()
    {

        //populating the player characters into the roster first
        foreach(GameObject g in playerObjects)
        {

            if (g.name.ToUpper().Contains("DANDERE"))
            {
                Dandere de = g.GetComponent<Dandere>();
                de.ReturnStats.gs_OID = orderID;
                roster.Add(de.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("KUUDERE"))
            {
                Kuudere ke = g.GetComponent<Kuudere>();
                ke.ReturnStats.gs_OID = orderID;
                roster.Add(ke.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("TSUNDERE"))
            {
                Tsundere te = g.GetComponent<Tsundere>();
                te.ReturnStats.gs_OID = orderID;
                roster.Add(te.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("YANDERE"))
            {
                Yandere ye = g.GetComponent<Yandere>();
                ye.ReturnStats.gs_OID = orderID;
                roster.Add(ye.ReturnStats);
            }

            orderID++;
        }
    }

    //This function adds the enemy characters to the roster in battle
    public void AddEnemyToRoster()
    {
        //As more enemies are added, the function will grow immensely...
        //This is only a temporary fix, until I can figure out a better
        //way to handle this...
        //string backgroundName = backgroundCheck.name.ToUpper();

        foreach (GameObject g in enemyObjects)
        {
            if (g.name.ToUpper().Contains("PINKSLIME"))
            {
                PinkSlime ps = g.GetComponent<PinkSlime>();
                ps.ReturnStats.gs_OID = orderID;
                roster.Add(ps.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("RIVALSCHOOLSTUDENT"))
            {
                RivalSchoolStudent rss = g.GetComponent<RivalSchoolStudent>();
                rss.ReturnStats.gs_OID = orderID;
                roster.Add(rss.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("SMALLVIRUS"))
            {
                SmallVirus sv = g.GetComponent<SmallVirus>();
                sv.ReturnStats.gs_OID = orderID;
                roster.Add(sv.ReturnStats);
            }

            orderID++;
        }

    }

    /*
    //This function displays the full roster
    //JUST FOR DEBUGGING PURPOSES
    public void DisplayRoster()
    {

        Debug.Log("Number of characters in roster: " + roster.Count);

        foreach (CharacterStats cs in roster)
        {
            Debug.Log(  "NAME: "    + cs.g_NAME + 
                        ", SIDE: "  + cs.g_SIDE +
                        ", HP: "    + cs.gs_HP + 
                        ", SP: "    + cs.gs_SP + 
                        ", ATK: "   + cs.gs_ATK + 
                        ", DEF: "   + cs.gs_DEF + 
                        ", SPD: "   + cs.gs_SPD + 
                        ", LUK: "   + cs.gs_LUK +
                        ", EXM: "   + cs.gs_EXM +
                        ", EXP: "   + cs.gs_EXP +
                        ", FORM: "  + cs.gs_FORM +
                        ", OID: "   + cs.gs_OID);
        }
    }
    */

}
