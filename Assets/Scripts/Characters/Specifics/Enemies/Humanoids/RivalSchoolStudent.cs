using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalSchoolStudent : CharacterGeneric {

    /*
     * This code is for the Zero Form of the Rival School Student.
     * This script is a child of CharacterGeneric.
     */

    /*
     * Specific to each character; Setting up the stats
     * for each character.
     */

    protected override void CharacterSetup()
    {
        base.CharacterSetup();
        characterStats = new CharacterStats("RivalSchoolStudent", gameObject.tag, 
            70, 30, 8, 10, 7, 5, 0, 0, 60, 0, 0, gameObject.transform);

    }
}
