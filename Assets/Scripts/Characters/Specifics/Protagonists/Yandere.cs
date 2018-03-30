using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yandere : CharacterGeneric {

    /// <summary>
    /// This code is for the Yandere, and is a child of CharacterGeneric.
    /// </summary>


    protected override void CharacterSetup()
    {
        base.CharacterSetup();

        //For reading purposes, these local variables will contain each stat to be entered, and then they will
        //all be relayed to CharacterStats. It's quite redundant, but it's easier for me to read 
        //and understand.

        gameObject.name = "Yandere";    //change the game object's name

        string initNAME = gameObject.name;  //Name
        string initSIDE = gameObject.tag;   //Side

        int initHP = 100;   //Health Points
        int initSP = 60;  //Skill Points
        int initATK = 15;   //Attack Points
        int initDEF = 7;    //Defense Points
        int initSPD = 14;   //Speed Points
        int initLUK = 8;   //Luck Points
        int initSTE = 0;    //State (in terms of an integer)
        int initEXM = 0;    //Experience Meter
        int initEXP = 0;    //Experience Points
        int initBID = 0;    //Battle ID
        int initTM = 0;    //Turn Meter

        Transform initPOS = gameObject.transform;   //Position (in terms of a transform)

        //inserting all the defined variables into the CharacterStats, thus creating the character
        characterStats = new CharacterStats(initNAME, initSIDE, initHP, initSP, initATK, initDEF, initSPD,
            initLUK, initSTE, initEXM, initEXP, initBID, initTM, initPOS);

        characterStats = new CharacterStats("Yandere", gameObject.tag, 
            100, 60, 15, 7, 14, 8, 0, 0, 0, 0, 0, gameObject.transform);
 
    }
}
