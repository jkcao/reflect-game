using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Play using default keys
 * Camera Panning: Use ; and " keys to pan left and right
 * 
 */

public class FollowBehavior : MonoBehaviour {

	[SerializeField]
	protected Transform trackingTarget;

	[SerializeField]
	float xOffset;

	[SerializeField]
	float yOffset;


	float cameraSpeed = 0.5f;

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

	bool gameStart;

	void Start() {

		// Initial Setup
		float ychange = transform.position.y;
		if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;
		transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);

		// GameStart false at the start
		gameStart = false;

		// After Camera Panning, resume game
		//gameStart = true;
		print("Start loop");
	}

	// Update is called once per frame
	void Update()
	{
		// Properties / Vars
		float ychange = transform.position.y;

		// Camera Panning --

		// Pan Right
		if(Input.GetKey(KeyCode.Semicolon))
		{
			gameStart = false;
			transform.position = new Vector3(transform.position.x + cameraSpeed, transform.position.y, transform.position.z);
			print ("Semicolon key pressed");
		}

		// Pan Left
		if(Input.GetKey(KeyCode.Quote))
		{
			gameStart = false;
			transform.position = new Vector3(transform.position.x - cameraSpeed, transform.position.y, transform.position.z);
			print ("Quote key pressed");
		}

		// Zooming in and out
		if(Input.GetKey(KeyCode.UpArrow))
		{
			if (Camera.main.fieldOfView<=125)
				Camera.main.fieldOfView +=2;
			if (Camera.main.orthographicSize<=20)
				Camera.main.orthographicSize +=0.5f;
		}

		if(Input.GetKey(KeyCode.DownArrow))
		{
			if (Camera.main.fieldOfView>2)
				Camera.main.fieldOfView -=2;
			if (Camera.main.orthographicSize>=1)
				Camera.main.orthographicSize -=0.5f;
		}

		// Re-attach camera
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			ychange = transform.position.y;
			if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;

			transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);
			gameStart = true;
			print ("Left key pressed - Camera attached");
		}

		if(Input.GetKey(KeyCode.RightArrow))
		{
			ychange = transform.position.y;
			if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;

			transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);
			gameStart = true;
			print ("Right key pressed - Camera attached");
		}

		// Camera
		if (gameStart == true) {
			
			ychange = transform.position.y;
			if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;
			transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);
		} else {
			print ("Camera is in level-exploring mode");
		}
	}
}
