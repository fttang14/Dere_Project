using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats {

    /*
	This script holds the stats of all characters. Values will change
	accordingly, depending on the situations.
	*/

    //setting up the initial values
    string name;            //NAME: the name of the character
    string side;            //SIDE: the alignment of the character (Player, Enemy, NPC)
    int healthPoints;       //HP: the character's health points
    int skillPoints;        //SP: the character's skill points 
    int attackPoints;       //ATK: the character's attack points
    int defensePoints;      //DEF: the character's defense points
    int speedPoints;        //SPD: the character's speed points
    int luckPoints;         //LUK: the character's luck points
    int state;              //STE: the character's current state
    int experienceMeter;    //EXM: the character's experience meter;
                            //ranges from 0 to 100
    int experiencePoints;   //EXP: the character's experience points;
                            //increases every time EXM % 100 = 0 &&
                            //EXM != 0
    int battleID;           //BID: the character's battle ID (will reset every battle)
    float turnMeter;        //TM: meter that increments over time to determine when the character can take action
    Transform position;     //POS: the character's battle position

    //default constructor for all characters
    public CharacterStats(string id, string alignment, int hp, int sp, int atk, int def, 
        int spd, int luk, int ste, int exm, int exp, int bid, float tm, Transform pos)
    {

        //String - setting up initial stat values
        name = id;
        side = alignment;

        //int - setting up intial stat values
        healthPoints = hp;
        skillPoints = sp;
        attackPoints = atk;
        defensePoints = def;
        speedPoints = spd;
        luckPoints = luk;
        state = ste;
        experienceMeter = exm;
        experiencePoints = exp;
        battleID = bid;

        //float - setting up intial stat values
        turnMeter = tm;

        //Transform - setting up initial stat values
        position = pos;
    }

    //special addition for certain characters;
    //limited to protagonists, antagonists, some enemies, and some npcs

    /*
    public virtual void CharacterStatsExtra(string name)
    {
        Debug.Log("The name of this character is: " + name);
    }
    */

    //get-set properties for each stat 
    public string g_NAME
    {
        get { return name; }
    }

    public string g_SIDE
    {
        get { return side; }
    }
    
    public int gs_HP
    {
        get { return healthPoints; }
        set { healthPoints = value; }
    }

    public int gs_SP
    {
        get { return skillPoints; }
        set { skillPoints = value; }
    }

    public int gs_ATK
    {
        get { return attackPoints; }
        set { attackPoints = value; }
    }

    public int gs_DEF
    {
        get { return defensePoints; }
        set { defensePoints = value; }
    }

    public int gs_SPD
    {
        get { return speedPoints; }
        set { speedPoints = value; }
    }

    public int gs_LUK
    {
        get { return luckPoints; }
        set { luckPoints = value; }
    }

    public int gs_STE
    {
        get { return state; }
        set { state = value; }
    }

    public int gs_EXM
    {
        get { return experienceMeter; }
        set { experienceMeter = value; }
    }

    public int gs_EXP
    {
        get { return experiencePoints; }
        set { experiencePoints = value; }
    }

    public int gs_BID
    {
        get { return battleID; }
        set { battleID = value; }
    }

    public float gs_TM
    {
        get { return turnMeter; }
        set { turnMeter = value; }
    }

    public Transform gs_POS
    {
        get { return position; }
        set { position = value; }
    }
}
