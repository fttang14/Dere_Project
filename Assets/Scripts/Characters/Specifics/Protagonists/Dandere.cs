using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dandere : CharacterGeneric {

    /*
     * This code is for the Dandere.
     * This script is a child of CharacterGeneric.
     */


    /*
     * Specific to each character; Setting up the stats
     * for each character.
     */

    protected override void CharacterSetup()
    {
        base.CharacterSetup();
        characterStats = new CharacterStats("Dandere", gameObject.tag, 
            80, 130, 10, 6, 10, 13, 0, 0, 0, 0, 0, 0);

    }
}
