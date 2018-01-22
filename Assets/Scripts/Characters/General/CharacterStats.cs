using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats {

    /*
	This script holds the stats of all characters. Values will change
	accordingly, depending on the situations.
	*/

    //setting up the initial values
    string name;        //NAME: the name of the character
    string side;        //SIDE: the alignment of the character (Player, Enemy, NPC)
    int healthPoints;   //HP: the character's health points
    int skillPoints;    //SP: the character's skill points 
    int attackPoints;   //ATK: the character's attack points
    int defensePoints;  //DEF: the character's defense points
    int speedPoints;    //SPD: the character's speed points
    int luckPoints;     //LUK: the character's luck points
    int experienceMeter;    //EXM: the character's experience meter;
                            //ranges from 0 to 100
    int experiencePoints;   //EXP: the character's experience points;
                            //increases every time EXM % 100 = 0 &&
                            //EXM != 0
    int form;   //FORM: the character's current form
    int orderID;  //OID: the order in which the character is in the roster

    //default constructor for all characters
    public CharacterStats(string id, string alignment, int hp, int sp, int atk, int def, 
        int spd, int luk, int exm, int exp, int phase, int order)
    {

        //setting up initial stat values
        name = id;
        side = alignment;
        healthPoints = hp;
        skillPoints = sp;
        attackPoints = atk;
        defensePoints = def;
        speedPoints = spd;
        luckPoints = luk;
        experienceMeter = exm;
        experiencePoints = exp;
        form = phase;
        orderID = order;
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

    public int gs_FORM
    {
        get { return form; }
        set { form = value; }
    }

    public int gs_OID
    {
        get { return orderID; }
        set { orderID = value; }
    }
}
