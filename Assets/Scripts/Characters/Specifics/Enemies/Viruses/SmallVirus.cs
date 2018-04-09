using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallVirus : CharacterGeneric{

    /// <summary>
    /// This script is for the SmallVirus, and is a child of CharacterGeneric
    /// </summary>

    /*** FUNCTIONS ***/

    protected override void CharacterSetup()
    {
        base.CharacterSetup();

        //For reading purposes, these local variables will contain each stat to be entered, and then they will
        //all be relayed to CharacterStats. It's quite redundant, but it's easier for me to read 
        //and understand.

        gameObject.name = "SmallVirus";    //change the game object's name

        string initNAME = gameObject.name;  //Name
        string initSIDE = gameObject.tag;   //Side

        int initHP = 60;   //Health Points
        int initSP = 20;  //Skill Points
        int initATK = 6;   //Attack Points
        int initDEF = 4;    //Defense Points
        int initSPD = 12;   //Speed Points
        int initLUK = 2;   //Luck Points
        int initSTE = 0;    //State (in terms of an integer)
        int initEXM = 0;    //Experience Meter
        int initEXP = 45;    //Experience Points
        int initBID = 0;    //Battle ID
        int initTM = 0;    //Turn Meter

        //Transform initPOS = gameObject.transform;   //Position (in terms of a transform)

        //inserting all the defined variables into the CharacterStats, thus creating the character
        characterStats = new CharacterStats(initNAME, initSIDE, initHP, initSP, initATK, initDEF, initSPD,
            initLUK, initSTE, initEXM, initEXP, initBID, initTM);

    }

}
