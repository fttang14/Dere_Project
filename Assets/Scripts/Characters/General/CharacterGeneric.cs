using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeneric : MonoBehaviour {

    /*
     * This code is going to be used for all characters.
     * All specific character scripts will inherit from
     * this script.
     */

    protected CharacterStats characterStats;    //stats of the current character
    protected Rigidbody2D rb2d; //the 2D Rigidbody of the object


	// Use this for initialization
	protected void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        CharacterSetup();
    }

    /*
     * Specific to each character; Setting up the stats
     * for each character. The function will also set if
     * the character will have either a player or enemy
     * controller script attached.
     */

    protected virtual void CharacterSetup()
    {
        if (gameObject.tag.ToUpper().Equals("PLAYER"))
        {
            gameObject.AddComponent<PlayerBattleController>();
            gameObject.GetComponent<PlayerBattleController>().enabled = false;
        }

        else if (gameObject.tag.ToUpper().Equals("ENEMY"))
        {
            gameObject.AddComponent<EnemyBattleController>();
            gameObject.GetComponent<EnemyBattleController>().enabled = false;
        }

        //Debug.Log("Setting up character...");
    }

    public CharacterStats ReturnStats
    {
        get { return characterStats; }
    }
}
