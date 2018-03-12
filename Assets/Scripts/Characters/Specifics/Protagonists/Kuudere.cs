using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuudere : CharacterGeneric {

    /*
     * This code is for the Kuudere.
     * This script is a child of CharacterGeneric.
     */


    /*
     * Specific to each character; Setting up the stats
     * for each character.
     */

    protected override void CharacterSetup()
    {
        base.CharacterSetup();
        characterStats = new CharacterStats("Kuudere", gameObject.tag, 
            120, 80, 8, 12, 9, 7, 0, 0, 0, 0, 0, gameObject.transform);
 
    }
}
