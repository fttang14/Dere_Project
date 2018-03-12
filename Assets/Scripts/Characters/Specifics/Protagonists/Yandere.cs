using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yandere : CharacterGeneric {

    /*
     * This code is for the Yandere.
     * This script is a child of CharacterGeneric.
     */

    /*
     * Specific to each character; Setting up the stats
     * for each character.
     */

    protected override void CharacterSetup()
    {
        base.CharacterSetup();
        characterStats = new CharacterStats("Yandere", gameObject.tag, 
            100, 60, 15, 7, 14, 8, 0, 0, 0, 0, 0, gameObject.transform);
 
    }
}
