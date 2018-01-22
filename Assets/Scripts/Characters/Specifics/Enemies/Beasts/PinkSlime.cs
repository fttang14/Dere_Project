using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkSlime : CharacterGeneric {

    /*
     * This code is for the Zero Form of the Pink Slime.
     * This script is a child of CharacterGeneric.
     */

    CharacterStats PinkSlimeStats;

    //Update is called once per frame
    void Update()
    {

    }

    /*
     * Specific to each character; Setting up the stats
     * for each character.
     */

    protected override void CharacterSetup()
    {
        base.CharacterSetup();
        PinkSlimeStats = new CharacterStats("PinkSlime", gameObject.tag, 50, 10, 4, 5, 7, 3, 0, 30, 0, 0);
 
    }

    //return all character stats for Pink Slime
    public CharacterStats ReturnStats
    {
        get { return PinkSlimeStats; }
    }
}
