using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dandere : CharacterGeneric {

    /// <summary>
    /// This script is for the Dandere, and is a child of CharacterGeneric.
    /// </summary>

    /*** FUNCTION ***/

    //This function sets up the initial stats of the Dandere.
    protected override void CharacterSetup()
    {
        base.CharacterSetup();

        //For reading purposes, these local variables will contain each stat to be entered, and then they will
        //all be relayed to CharacterStats. It's quite redundant, but it's easier for me to read 
        //and understand.
 
        gameObject.name = "Dandere";    //change the game object's name

        string initNAME = gameObject.name;  //Name
        string initSIDE = gameObject.tag;   //Side

        int initHP      = 80;   //Health Points
        int initSP      = 130;  //Skill Points
        int initATK     = 10;   //Attack Points
        int initDEF     = 6;    //Defense Points
        int initSPD     = 10;   //Speed Points
        int initLUK     = 13;   //Luck Points
        int initSTE     = 0;    //State (in terms of an integer)
        int initEXM     = 0;    //Experience Meter
        int initEXP     = 0;    //Experience Points
        int initBID     = 0;    //Battle ID
        int initTM      = 0;    //Turn Meter

        //Transform initPOS = gameObject.transform;   //Position (in terms of a transform)

        //inserting all the defined variables into the CharacterStats, thus creating the character
        characterStats = new CharacterStats(initNAME, initSIDE, initHP, initSP, initATK, initDEF, initSPD, 
            initLUK, initSTE, initEXM, initEXP, initBID, initTM);

    }
}
