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

	/* Panning left + right: use "-" and "=" key respectively
	 * 
	 * When input key press = "-", move the cameras to the left
	 * When input key press = "+", move the cameras to the right
	 * Jump key would reset the camera position
	 * 
	 * 
	 * 
	 * 
	 * /

	// Update is called once per frame
	void Update()
	{
		float ychange = transform.position.y;
		if(trackingTarget.position.y >= -3f) ychange = trackingTarget.position.y + yOffset;
		
		transform.position = new Vector3(trackingTarget.position.x + xOffset, ychange, transform.position.z);
	}
}
