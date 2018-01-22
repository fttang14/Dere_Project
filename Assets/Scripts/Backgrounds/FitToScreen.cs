using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitToScreen : MonoBehaviour {

    /*
    The purpose of this code is to fit the background screen
    to fill the camera. Works only for orthographic camera.
    */

    float cameraHeight;
    float cameraWidth;
    SpriteRenderer sr;
    Vector2 cameraSize;
    Vector2 spriteSize;
    Vector2 scale;

	// Use this for initialization
	void OnEnable () {
        sr = GetComponent<SpriteRenderer>();

        cameraHeight = Camera.main.orthographicSize * 2.0f; //half of the camera's height in Unity units * 2.0f
                                                            //to get the full height

        cameraWidth = cameraHeight / Screen.height * Screen.width;  //getting the width of the screen

        spriteSize = sr.sprite.bounds.size; //size of the sprite

        transform.localScale = new Vector2(1, 1);   //resetting the scale of the game object
        transform.position = Vector2.zero;          //resetting the position of the game object

        transform.localScale = new Vector2(cameraWidth / spriteSize.x, cameraHeight / spriteSize.y); //setting the new scale
                                                                                                     //of the background
	}
	

}
