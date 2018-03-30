using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkSlime : CharacterGeneric {

    /// <summary>
    /// This script is for the PinkSlime, and is a child of CharacterGeneric.
    /// </summary>

    /*** FUNCTIONS ***/

    protected override void CharacterSetup()
    {
        base.CharacterSetup();

        //For reading purposes, these local variables will contain each stat to be entered, and then they will
        //all be relayed to CharacterStats. It's quite redundant, but it's easier for me to read 
        //and understand.

        gameObject.name = "PinkSlime";    //change the game object's name

        string initNAME = gameObject.name;  //Name
        string initSIDE = gameObject.tag;   //Side

        int initHP = 50;   //Health Points
        int initSP = 10;  //Skill Points
        int initATK = 4;   //Attack Points
        int initDEF = 5;    //Defense Points
        int initSPD = 7;   //Speed Points
        int initLUK = 3;   //Luck Points
        int initSTE = 0;    //State (in terms of an integer)
        int initEXM = 0;    //Experience Meter
        int initEXP = 30;    //Experience Points
        int initBID = 0;    //Battle ID
        int initTM = 0;    //Turn Meter

        Transform initPOS = gameObject.transform;   //Position (in terms of a transform)

        //inserting all the defined variables into the CharacterStats, thus creating the character
        characterStats = new CharacterStats(initNAME, initSIDE, initHP, initSP, initATK, initDEF, initSPD,
            initLUK, initSTE, initEXM, initEXP, initBID, initTM, initPOS);
    }
}
