using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlatform : MonoBehaviour {
	
	private float allocatedHeight;
	private GameObject allocated;
	
	// Use this for initialization.
	void Start () {
		allocatedHeight = this.transform.position.y;

		allocated = null;
	}

	// When in-contact with the player, dynamically allocate the mirrored platform.
	protected void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Player") {
			Player p = col.gameObject.GetComponent<Player>();
			// Finding position mirrored should go to.
			Vector2 mirrored = new Vector2(-(transform.position.x - p.transform.position.x) + p.mirror.transform.position.x,
											p.mirror.getGroundPosition() - p.getGroundPosition() + allocatedHeight);

			//Making the new platform.
			allocated = Instantiate (this.gameObject);
			//Don't want infinitely spawning platforms!
			Destroy(allocated.GetComponent<MirrorPlatform>());
			Destroy(allocated.GetComponent<SpriteRenderer>());
			allocated.transform.position = mirrored;
		}
	}

	// Destroy the mirrored platform on exit.
	protected void OnCollisionExit2D (Collision2D col){
		if (col.gameObject.tag == "Player") {
			Destroy (allocated);
			allocated = null;
		}
	}
}
