using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats{

    //default constructor for all characters
    public CharacterStats(string id, string alignment, int hp, int sp, int atk, int def, 
        int spd, int luk, int ste, int exm, int exp, int bid, float tm)
    {

        //String - setting up initial stat values
        g_NAME = id;
        g_SIDE = alignment;

        //int - setting up intial stat values
        gs_HP = hp;
        gs_SP = sp;
        gs_ATK = atk;
        gs_DEF = def;
        gs_SPD = spd;
        gs_LUK = luk;
        gs_STE = ste;
        gs_EXM = exm;
        gs_EXP = exp;
        gs_BID = bid;

        //float - setting up intial stat values
        gs_TM = tm;

        //bool - setting up initial stat values
        gs_DEAD = false;
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
    public string g_NAME { get; private set; }  //Name of the character

    public string g_SIDE { get; private set; }  //Affiliation of the character

    public int gs_HP { get; set; }  //Character's Health value
    
    public int gs_SP { get; set; }  //Character's Skill value

    public int gs_ATK { get; set; } //Character's Attack value

    public int gs_DEF { get; set; } //Character's Defense value

    public int gs_SPD { get; set; } //Character's Speed value

    public int gs_LUK { get; set; } //Character's Luck value

    public int gs_STE { get; set; } //Character's State / Position

    public int gs_EXM { get; set; } //Character's Experience meter value

    public int gs_EXP { get; set; } //Character's Experience points value

    public int gs_BID { get; set; } //Character's Battle ID

    public float gs_TM { get; set; }    //Character's cooldown value

    public bool gs_DEAD { get; set; }   //Character's death state
}
