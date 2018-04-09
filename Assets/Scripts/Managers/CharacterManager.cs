using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    /// <summary>
    /// This script will create the roster for the characters and send it to Battle Manager
    /// </summary>

    /*** VARIABLES ***/

    int battleID;                   //this will be the BID for the character's battle controllers
    List<CharacterStats> roster;    //list of characters entering battle
    List<GameObject> playerObjects; //list of all players entering battle
    List<GameObject> enemyObjects;  //list of all enemies entering battle

    public AllCharacterList allCharacters; //list of all characters ever created
    public GameObject battleManager;    //the battle manager game object
    public GameObject backgroundCheck;  //a the background game object
    public GameObject HUD;  //the HUDs of all the characters
    public List<Transform> playerSpawn; //list of all player spawn points
    public List<Transform> enemySpawn;  //list of all enemy spawn points

    /*** FUNCTIONS ***/

    //Initializing some variables first
    private void Awake()
    {
        battleID = 0;
        roster = new List<CharacterStats>();
        playerObjects = new List<GameObject>();
        enemyObjects = new List<GameObject>();
        battleManager.SetActive(false); //activating the battle manager once
                                        //all the characters have been
                                        //added to their proper lists...
        HUD.SetActive(false);   //wait until all the characters have been added to their proper lists...
    }

    //Initializing the scene with all the startup functions
    private void Start()
    {
        InstantiateCharacters();    //instantiating all the characters who will participate in battle

        AddPlayerToRoster();  //adding all the instantiated player characters to the
                              //roster so that they can be added to the
                              //battle manager to handle...

        AddEnemyToRoster(); //adding all the instantiated enemy characters to the
                            //roster so that they can be added to the
                            //battle manager to handle...

        //once everything has been set up, activate the battle manager and HUD
        battleManager.SetActive(true);  
        HUD.SetActive(true);
        

        //transfer data from Character to Battle Manager
        BattleManager bm = battleManager.GetComponent<BattleManager>();
        bm.TransferFromGM(roster, playerObjects, enemyObjects);
    }

    //This function will instantiate all the characters for battle
    public void InstantiateCharacters()
    {

        //instantiate the player characters first
        for(int i = 0; i < allCharacters.Players.Count; i++)
        {
            //adding instantiated objects to game object list so that their stats can be added to the roster.
            playerObjects.Add(Instantiate(allCharacters.Players[i], playerSpawn[i]));

            //also setting each object's parent to the CharacterRoster GameObject for organization.
            playerObjects[i].transform.SetParent(GameObject.Find("CharacterRoster").transform);
        }

        /****** Now we instantiate the enemies ******/

        //First, we determine how many enemies to spawn
        int howManyEnemies = Random.Range(1, enemySpawn.Count+1);

        //Second, determine from which list we will choose from, based on background...
        string backgroundName = backgroundCheck.name.ToUpper();

        //Third, we decide which enemies to spawn depending on how many enemies to spawn and
        //the background's name
        if (backgroundName.Equals("BATTLEBACKGROUNDTEST"))
        {
            
            //choose random enemies from EnemiesTest and add them to enemyObjects 
            for (int i = 0; i < howManyEnemies; i++)
            {
                int decision = Random.Range(0, allCharacters.EnemiesTest.Count);
                GameObject temp = allCharacters.EnemiesTest[decision];

                //adding instantiated objects to game object list so that their stats can be added to the roster.
                enemyObjects.Add(Instantiate(temp, enemySpawn[i]));

                //also setting each object's parent to the CharacterRoster GameObject for organization.
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
                Dandere dandere = g.GetComponent<Dandere>();
                dandere.ReturnStats.gs_BID = battleID;
                roster.Add(dandere.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("KUUDERE"))
            {
                Kuudere kuudere = g.GetComponent<Kuudere>();
                kuudere.ReturnStats.gs_BID = battleID;
                roster.Add(kuudere.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("TSUNDERE"))
            {
                Tsundere tsundere = g.GetComponent<Tsundere>();
                tsundere.ReturnStats.gs_BID = battleID;
                roster.Add(tsundere.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("YANDERE"))
            {
                Yandere yandere = g.GetComponent<Yandere>();
                yandere.ReturnStats.gs_BID = battleID;
                roster.Add(yandere.ReturnStats);
            }

            battleID++;  //incremement battle ID afterwards
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
                PinkSlime pinkSlime = g.GetComponent<PinkSlime>();
                pinkSlime.ReturnStats.gs_BID = battleID;
                roster.Add(pinkSlime.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("RIVALSCHOOLSTUDENT"))
            {
                RivalSchoolStudent rivalSchoolStudent = g.GetComponent<RivalSchoolStudent>();
                rivalSchoolStudent.ReturnStats.gs_BID = battleID;
                roster.Add(rivalSchoolStudent.ReturnStats);
            }

            else if (g.name.ToUpper().Contains("SMALLVIRUS"))
            {
                SmallVirus smallVirus = g.GetComponent<SmallVirus>();
                smallVirus.ReturnStats.gs_BID = battleID;
                roster.Add(smallVirus.ReturnStats);
            }

            battleID++;  //incremement battle ID afterwards
        }
    }

}
