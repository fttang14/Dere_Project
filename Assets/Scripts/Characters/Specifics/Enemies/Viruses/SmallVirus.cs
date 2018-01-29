using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallVirus : CharacterGeneric{

    /*
     * This code is for the Zero Form of the Small Virus.
     * This script is a child of CharacterGeneric.
     */

    /*
     * Specific to each character; Setting up the stats
     * for each character.
     */

    protected override void CharacterSetup()
    {
        base.CharacterSetup();
        characterStats = new CharacterStats("SmallVirus", gameObject.tag, 
            60, 20, 6, 4, 12, 2, 0, 0, 45, 0, 0, 0);
    }

}
