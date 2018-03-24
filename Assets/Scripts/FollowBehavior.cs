using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : MonoBehaviour {

	[SerializeField]
	protected Transform trackingTarget;

	[SerializeField]
	float xOffset;

	[SerializeField]
	float yOffset;

	/* Pre-Level Start: Camera moves up slowly until the topmost platform is visible, and slowly moves down
	 * 
	 * Camera Properties: 
	 * - Maximum height to go up for showing the level
	 * - Speed of camera moving up/down before
	 * - Perhaps some sound/music to indicate that this is level-showing time
	 * 
	 * Then level starts normally
	 * 
	 */

	/* Panning left + right: use "-" and "=" key respectively
	* 
	* When input key press = "-", move the cameras to the left
	* When input key press = "+", move the cameras to the right
	* Jump key would reset the camera position
	* 
	*/

	bool gameStart = false;

	void Start() {

		// Initial Setup
		float ychange = transform.position.y;
		if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;
		transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);

		// After Camera Panning, resume game
		//gameStart = true;
		print("Start loop");
	}

	// Update is called once per frame
	void Update()
	{

		// Camera Panning --

		// Pan Right
		if(Input.GetKey(KeyCode.Semicolon))
		{
			transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
			print ("Semicolon key pressed");
		}

		// Pan Left
		if(Input.GetKey(KeyCode.Quote))
		{
			transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
			print ("Quote key pressed");
		}

		// Game Start toggle by Pressing Space Key: Camera should start following user
		if(Input.GetKey(KeyCode.Space))
		{
			gameStart = true;
			float ychange = transform.position.y;
			if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;

			transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);

			print ("Game Started!");
			print ("Space key pressed - GameStart");
		}



		// Camera
		if (gameStart) {
			float ychange = transform.position.y;
			if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;

			transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);
		} else {
			print ("Camera is in level-exploring mode");
		}
	}
}
