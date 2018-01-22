using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCharacterList : MonoBehaviour {


    /*
     * This script will hold all characters from the game,
     * whether they are Player, Enemy, or NPC characters.
     * All characters will be stored here, in multiple Lists
     * according to the area the player is in.
     */

    //All the player character game objects are stored here...
    public List<GameObject> Players;

    /*
     * Enemies will be stored here; multiple lists will be
     * created, depending on region...
     */

    //TESTING: Temporary list
    public List<GameObject> EnemiesTest;

}
