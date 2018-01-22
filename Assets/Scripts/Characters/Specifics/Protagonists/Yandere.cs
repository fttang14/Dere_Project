using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yandere : CharacterGeneric {

    /*
     * This code is for the Yandere.
     * This script is a child of CharacterGeneric.
     */

    CharacterStats YandereStats;
	
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
        YandereStats = new CharacterStats("Yandere", gameObject.tag, 100, 60, 15, 7, 14, 8, 0, 0, 0, 0);
 
    }

    //return all the character stats of the Yandere
    public CharacterStats ReturnStats
    {
        get { return YandereStats; }
    }
}
