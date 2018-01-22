using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeneric : MonoBehaviour {

    /*
     * This code is going to be used for all characters.
     * All specific character scripts will inherit from
     * this script.
     */

    protected Rigidbody2D rb2d;

	// Use this for initialization
	protected void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        CharacterSetup();
    }

    protected void Start()
    {
        
    }

    /*
     * Specific to each character; Setting up the stats
     * for each character.
     */

    protected virtual void CharacterSetup()
    {
        Debug.Log("Setting up character...");
    }
}
