using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGeneric : MonoBehaviour {

    /// <summary>
    /// This code is going to be used for all characters. It is going to add the correct battle controller
    /// components to each character in battle. This is the parent script for all specific character scripts.
    /// </summary>

    /*** VARIABLES ***/

    protected CharacterStats characterStats;    //stats of the current character
    protected Rigidbody2D rb2d; //the 2D Rigidbody of the object


    /*** FUNCTIONS ***/

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
    
    //This function sets up the correct battle controllers for each character based on their tag as
    //either "Player" or "Enemy."
    protected virtual void CharacterSetup()
    {
        //The controllers are set to false so that everyone's turn doesn't all happen at once.
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

    }

    //This function just returns the full stats of a specified character
    public CharacterStats ReturnStats
    {
        get { return characterStats; }
    }
}
