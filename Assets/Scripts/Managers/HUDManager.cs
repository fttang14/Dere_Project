using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    /// <summary>
    /// This script will handle all the HUD updates in the battle scene
    /// </summary>

    /*** VARIABLES ***/

    //The HUD for each character
    public List<GameObject> playerHUD;
    public GameObject enemyHUD;

    public GameObject healthIcon;
    public GameObject skillIcon;
    public GameObject healthText;
    public GameObject skillText;

    public GameObject coolDown;

    public GameObject actions;  //actions that the character can make; PLAYER CHARACTERS ONLY
    public GameObject enemyTarget;  //this will pop up above the enemies so the player can choose who to attack

    public List<RectTransform> actionSpawn;
    public List<RectTransform> EnemyTargetSpawn;

    //The instantiated HUD for each character, along with other icons and texts
    List<GameObject> instantiatedHUD;
    List<GameObject> instantiatedHealth;
    List<GameObject> instantiatedSkill;
    List<GameObject> instantiatedHT;
    List<GameObject> instantiatedST;
    List<GameObject> instantiatedCD;

    /*** FUNCTIONS ***/

    private void Awake()
    {

        //deactivate all the HUDs until values are set for each one
        foreach (GameObject g in playerHUD)
        {
            g.SetActive(false);
        }
        enemyHUD.SetActive(false);

        actions.SetActive(false);

        instantiatedHUD = new List<GameObject>();
        instantiatedHealth = new List<GameObject>();
        instantiatedSkill = new List<GameObject>();
        instantiatedHT = new List<GameObject>();
        instantiatedST = new List<GameObject>();
        instantiatedCD = new List<GameObject>();
    }

    // This function will use instantiate HUDs for each character on the field; information relayed from
    // BattleManager
    public void ActivateHUD(List<CharacterStats> roster)
    {
        //For every enemy on the field, create a new HUD for it by shifting pre-existing HUD
        Vector2 enemyHUDShift = Vector2.left * 100;
        float shiftNumber = 0;

        //setting up the HUD for each character
        foreach(CharacterStats cs in roster)
        {
            //going through the HUD list to find the correct one to activate and modify
            if (cs.g_SIDE.ToUpper().Equals("PLAYER"))
            {
                //a temporary variable to store the character's name
                string characterName = cs.g_NAME;

                //going through the playerHUD list to find the correct one to activate and modify
                foreach (GameObject g in playerHUD)
                {
                    //once a match has been found, start modifying each component of the HUD
                    if (g.name.ToUpper().Contains(characterName.ToUpper()))
                    {
                        //create an instance of this game object so that it doesn't affect the actual prefab
                        GameObject newG = Instantiate(g);
                        newG.name = cs.g_NAME + "HUD";

                        /*** HUD ***/
                        newG.SetActive(true);
                        newG.transform.SetParent(GameObject.Find("Canvas").transform);

                        //Next, setting up the Health and Skill values//

                        //get the rect transform of each HUD to position other HUD materials
                        RectTransform rt = newG.GetComponent<RectTransform>();

                        //create instantiations of other game objects relative to HUD
                        
                        GameObject instantHI = Instantiate(healthIcon);
                        GameObject instantHT = Instantiate(healthText);
                        GameObject instantSI = Instantiate(skillIcon);
                        GameObject instantST = Instantiate(skillText);
                        GameObject instantCD = Instantiate(coolDown);
                        

                        /*** HEALTH ***/
                        SetupHP(instantHI, instantHT, cs, rt);

                        /*** SKILL ***/
                        SetupMP(instantSI, instantST, cs, rt);

                        /*** COOLDOWN ***/
                        SetupCD(instantCD, cs, rt);

                        /*** INSTANTIATE ***/
                        
                        instantiatedHUD.Add(newG);
                        instantiatedHealth.Add(instantHI);
                        instantiatedHT.Add(instantHT);
                        instantiatedSkill.Add(instantSI);
                        instantiatedST.Add(instantST);
                        instantiatedCD.Add(instantCD);
                        

                        //go to the next character when finished
                        break;
                    }
                }
            }

            //now we go to the enemy
            else if (cs.g_SIDE.ToUpper().Equals("ENEMY"))
            {
                /*** HUD ***/
                GameObject newEnemyHUD = Instantiate(enemyHUD);  //create a new HUD for the enemy
                newEnemyHUD.name = cs.g_NAME + "HUD";
                newEnemyHUD.SetActive(true);
                newEnemyHUD.transform.SetParent(GameObject.Find("Canvas").transform);

                //Next, setting up the Health and Skill values//

                //get the rect transform of each HUD to position other HUD materials
                RectTransform rt = newEnemyHUD.GetComponent<RectTransform>();

                //create instantiations of other game objects relative to HUD
                GameObject instantHI = Instantiate(healthIcon);
                GameObject instantHT = Instantiate(healthText);
                GameObject instantSI = Instantiate(skillIcon);
                GameObject instantST = Instantiate(skillText);
                GameObject instantCD = Instantiate(coolDown);

                /*** HEALTH ***/
                SetupHP(instantHI, instantHT, cs, rt);

                /*** SKILL ***/
                SetupMP(instantSI, instantST, cs, rt);

                /*** COOLDOWN ***/
                SetupCD(instantCD, cs, rt);

                //setting new anchor position for the enemy HUD
                rt.anchoredPosition = new Vector2(
                    rt.position.x + (enemyHUDShift.x * shiftNumber),
                    rt.position.y);

                /*** INSTANTIATE ***/
                instantiatedHUD.Add(newEnemyHUD);
                instantiatedHealth.Add(instantHI);
                instantiatedHT.Add(instantHT);
                instantiatedSkill.Add(instantSI);
                instantiatedST.Add(instantST);
                instantiatedCD.Add(instantCD);

                //increment shifting value
                shiftNumber++;
            }
        }
    }

    //This function is only for PLAYER characters; activate the Buttons that allow user input and return that game
    //object to the player controller
    public GameObject ActivateActions(int BID)
    {
        GameObject instantAction = Instantiate(actions);
        instantAction.SetActive(true);
        instantAction.transform.SetParent(GameObject.Find("Canvas").transform);
        instantAction.GetComponent<RectTransform>().anchoredPosition = actionSpawn[BID].anchoredPosition;
        return instantAction;
    }

    //Setting up HP for HUD
    //G: healthIcon, G: healthText, C: characterStats, R: HUD RectTransform
    void SetupHP (GameObject hi, GameObject ht, CharacterStats c, RectTransform rt)
    {

        //setting icon and text relative to HUD
        hi.transform.SetParent(rt.transform);
        ht.transform.SetParent(rt.transform);

        //reset local position relative to its parent and then modify it

        if (c.g_SIDE.ToUpper().Equals("PLAYER"))
        {
            hi.transform.localPosition = Vector2.up * rt.position.y;
            ht.transform.localPosition = Vector2.up * rt.position.y;
        }

        else if (c.g_SIDE.ToUpper().Equals("ENEMY"))
        {
            hi.transform.localPosition = Vector2.down * rt.position.y;
            ht.transform.localPosition = Vector2.down * rt.position.y;
        }
        

        //re-define the texts for health
        hi.name = c.g_NAME + "HP";
        ht.name = c.g_NAME + "Health";
        hi.tag = c.g_SIDE;
        ht.tag = c.g_SIDE;
        ht.GetComponent<Text>().text = c.gs_HP.ToString();
    }

    //Setting up SP for HUD
    //G: skillIcon, G: skillText, C: characterStats, R: HUD RectTransform
    void SetupMP(GameObject si, GameObject st, CharacterStats c, RectTransform rt)
    {

        //setting icon and text relative to HUD
        si.transform.SetParent(rt.transform);
        st.transform.SetParent(rt.transform);

        //reset local position relative to its parent and then modify it
        if (c.g_SIDE.ToUpper().Equals("PLAYER"))
        {
            Vector2 newPos = new Vector2(0, -0.5f);
            si.transform.localPosition = newPos * rt.position.y;
            st.transform.localPosition = newPos * rt.position.y;
        }

        else if (c.g_SIDE.ToUpper().Equals("ENEMY"))
        {
            Vector2 newPos = new Vector2(0, 0.5f);
            si.transform.localPosition = newPos * rt.position.y;
            st.transform.localPosition = newPos * rt.position.y;
        }

        //re-define the texts for skill
        si.name = c.g_NAME + "SP";
        st.name = c.g_NAME + "Skill";
        si.tag = c.g_SIDE;
        st.tag = c.g_SIDE;
        st.GetComponent<Text>().text = c.gs_SP.ToString();
    }

    //Setting up CoolDown for HUD
    //G: Cooldown, C: characterStats, R: HUD RectTransform
    void SetupCD(GameObject meter, CharacterStats cs, RectTransform rt)
    {
        //Setting the meter relative to HUD
        meter.transform.SetParent(rt.transform);

        //reset local position relative to its parent and then modify it
        if (cs.g_SIDE.ToUpper().Equals("PLAYER"))
        {
            Vector2 newPos = new Vector2(-1.5f, -1.8f);
            meter.transform.localPosition =  newPos * rt.position.y;
        }

        else if (cs.g_SIDE.ToUpper().Equals("ENEMY"))
        {
            Vector2 newPos = new Vector2(1.5f, 1.8f);
            meter.transform.localPosition = newPos * rt.position.y;
        }

        //re-define the texts for cooldown
        meter.name = cs.g_NAME + "CoolDown";
        meter.tag = cs.g_SIDE;
        meter.GetComponent<Slider>().value = cs.gs_TM;
    }

    //Updating HP values from battle
    public void UpdatingHP(int newHealth, int index)
    {
        instantiatedHT[index].GetComponent<Text>().text = newHealth.ToString();

        //if health is 0, remove the HUD of that character
        if(newHealth <= 0)
        {
            instantiatedHUD[index].SetActive(false);
        }
    }

    //Updating CD values from battle
    public void UpdatingCD(float newCoolDown, int index)
    {
        instantiatedCD[index].GetComponent<Slider>().value = newCoolDown;

        //if cooldown is over 100%, set it to 100%
        if(newCoolDown >= 1.0f)
        {
            instantiatedCD[index].GetComponent<Slider>().value = 1.0f;
        }
    }
}
