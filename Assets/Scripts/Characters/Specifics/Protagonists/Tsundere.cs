using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsundere : CharacterGeneric {

    /*
     * This code is for the Tsundere.
     * This script is a child of CharacterGeneric.
     */

    CharacterStats TsundereStats;

    // Update is called once per frame
    void Update () {
		
	}

    /*
     * Specific to each character; Setting up the stats
     * for each character.
     */

    protected override void CharacterSetup()
    {
        base.CharacterSetup();
        TsundereStats = new CharacterStats("Tsundere", gameObject.tag, 90, 110, 9, 10, 11, 8, 0, 0, 0, 0);

    }

    //return all the character stats for the Tsundere
    public CharacterStats ReturnStats
    {
        get { return TsundereStats; }
    }
}
