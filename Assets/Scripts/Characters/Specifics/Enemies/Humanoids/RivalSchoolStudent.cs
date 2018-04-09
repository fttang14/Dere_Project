using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalSchoolStudent : CharacterGeneric {

    /// <summary>
    /// This script is for the RivalSchoolStudent, and is the child of Character Generic.
    /// </summary>


    protected override void CharacterSetup()
    {
        base.CharacterSetup();

        //For reading purposes, these local variables will contain each stat to be entered, and then they will
        //all be relayed to CharacterStats. It's quite redundant, but it's easier for me to read 
        //and understand.

        gameObject.name = "RivalSchoolStudent";    //change the game object's name

        string initNAME = gameObject.name;  //Name
        string initSIDE = gameObject.tag;   //Side

        int initHP = 70;   //Health Points
        int initSP = 30;  //Skill Points
        int initATK = 8;   //Attack Points
        int initDEF = 10;    //Defense Points
        int initSPD = 7;   //Speed Points
        int initLUK = 5;   //Luck Points
        int initSTE = 0;    //State (in terms of an integer)
        int initEXM = 0;    //Experience Meter
        int initEXP = 60;    //Experience Points
        int initBID = 0;    //Battle ID
        int initTM = 0;    //Turn Meter

        //Transform initPOS = gameObject.transform;   //Position (in terms of a transform)

        //inserting all the defined variables into the CharacterStats, thus creating the character
        characterStats = new CharacterStats(initNAME, initSIDE, initHP, initSP, initATK, initDEF, initSPD,
            initLUK, initSTE, initEXM, initEXP, initBID, initTM);

    }
}
