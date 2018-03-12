using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    /// <summary>
    /// This script will handle all the HUD updates
    /// in the battle scene...
    /// </summary>

    //The HUD for each character
    public List<GameObject> HUD;
    public GameObject healthIcon;
    public GameObject skillIcon;
    public GameObject healthText;
    public GameObject skillText;

    //The instantiated HUD for each character
    List<GameObject> instantiatedHUD;
    List<GameObject> instantiatedHealth;
    List<GameObject> instantiatedSkill;
    List<GameObject> instantiatedHT;
    List<GameObject> instantiatedST;


    private void Awake()
    {
        foreach(GameObject g in HUD)
        {
            g.SetActive(false);

        }

        instantiatedHUD = new List<GameObject>();
        instantiatedHealth = new List<GameObject>();
        instantiatedSkill = new List<GameObject>();
        instantiatedHT = new List<GameObject>();
        instantiatedST = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // This function will use instantiate HUDs
    // for each character on the field...
    public void ActivateHUD(List<CharacterStats> r)
    {
        //For every enemy on the field, create a new
        //HUD for it by shifting pre-exisiting HUD
        Vector2 enemyHUDShift = Vector2.left * 100;
        float shiftNumber = 0;

        //index for instantiated HUD list
        int index = 0;

        foreach(CharacterStats c in r)
        {
            foreach(GameObject g in HUD)
            {
                //instantiating the player HUDs
                if (g.name.ToUpper().Contains(c.g_NAME.ToUpper()))
                {
                    /*** HUD ***/
                    //instantiate and activate the player HUDs
                    instantiatedHUD.Add(Instantiate(g));
                    instantiatedHUD[index].SetActive(true);
                    instantiatedHUD[index].transform.SetParent(GameObject.Find("Canvas").transform);

                    //get the rect transform of each HUD to position other HUD materials
                    RectTransform rt = instantiatedHUD[index].GetComponent<RectTransform>();

                    /*** HEALTH ***/
                    //Instantiating and setting up health
                    instantiatedHealth.Add(Instantiate(healthIcon));
                    instantiatedHT.Add(Instantiate(healthText));
                    SetupHP(instantiatedHealth[index], instantiatedHT[index], c, rt);

                    /*** MANA ***/
                    //Instantiating and setting up skill
                    instantiatedSkill.Add(Instantiate(skillIcon));
                    instantiatedST.Add(Instantiate(skillText));
                    SetupMP(instantiatedSkill[index], instantiatedST[index], c, rt);

                    break;
                }

                //instantiating the enemy HUDs
                else if(c.g_SIDE.ToUpper().Equals("ENEMY") &&
                    g.tag.ToUpper().Equals(c.g_SIDE.ToUpper()))
                {

                    /*** HUD ***/
                    //instantiate and activate the player HUDs
                    instantiatedHUD.Add(Instantiate(g));
                    instantiatedHUD[index].SetActive(true);
                    instantiatedHUD[index].transform.SetParent(GameObject.Find("Canvas").transform);

                    //get the rect transform of each HUD to position other HUD materials
                    RectTransform rt = instantiatedHUD[index].GetComponent<RectTransform>();

                    /*** HEALTH ***/
                    //Instantiating and setting up health
                    instantiatedHealth.Add(Instantiate(healthIcon));
                    instantiatedHT.Add(Instantiate(healthText));
                    SetupHP(instantiatedHealth[index], instantiatedHT[index], c, rt);

                    /*** MANA ***/
                    //Instantiating and setting up skill
                    instantiatedSkill.Add(Instantiate(skillIcon));
                    instantiatedST.Add(Instantiate(skillText));
                    SetupMP(instantiatedSkill[index], instantiatedST[index], c, rt);

                    //setting new anchor position for the enemy HUD
                    rt.anchoredPosition = new Vector2(
                        rt.position.x + (enemyHUDShift.x * shiftNumber),
                        rt.position.y);

                    shiftNumber++;

                    break;
                }
                
            }

            index++;
        }
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
    //G: healthIcon, G: healthText, C: characterStats, R: HUD RectTransform
    void SetupMP(GameObject si, GameObject st, CharacterStats c, RectTransform rt)
    {

        //setting icon and text relative to HUD
        si.transform.SetParent(rt.transform);
        st.transform.SetParent(rt.transform);

        //reset local position relative to its parent and then modify it
        if (c.g_SIDE.ToUpper().Equals("PLAYER"))
        {
            si.transform.localPosition = Vector2.down * rt.position.y;
            st.transform.localPosition = Vector2.down * rt.position.y;
        }

        else if (c.g_SIDE.ToUpper().Equals("ENEMY"))
        {
            si.transform.localPosition = Vector2.up * rt.position.y;
            st.transform.localPosition = Vector2.up * rt.position.y;
        }

        //re-define the texts for health
        si.name = c.g_NAME + "SP";
        st.name = c.g_NAME + "Skill";
        si.tag = c.g_SIDE;
        st.tag = c.g_SIDE;
        st.GetComponent<Text>().text = c.gs_SP.ToString();
    }

}
