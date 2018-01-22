using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuudere : CharacterGeneric {

    /*
     * This code is for the Kuudere.
     * This script is a child of CharacterGeneric.
     */

    CharacterStats KuudereStats;
	
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
        KuudereStats = new CharacterStats("Kuudere", gameObject.tag, 120, 80, 8, 12, 9, 7, 0, 0, 0, 0);
 
    }

    //return all character stats for the Kuudere
    public CharacterStats ReturnStats
    {
        get { return KuudereStats; }
    }
}
