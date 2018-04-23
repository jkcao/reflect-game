using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlatform : MonoBehaviour {

	private Animation anim;

	private float allocatedHeight;
	private GameObject allocated;
	private Player collided;

	// Use this for initialization.
	void Start () {
		allocatedHeight = this.transform.position.y;

		allocated = null;

		anim = this.GetComponent<Animation> ();
	}

	// When in-contact with the player, dynamically allocate the mirrored platform.
	protected void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			collided = col.gameObject.GetComponent<Player> ();

			// Finding position mirrored should go to.
			Vector2 mirrored = new Vector2(-(transform.position.x - collided.transform.position.x) + collided.mirror.transform.position.x,
				collided.mirror.getGroundPosition() - collided.getGroundPosition() + allocatedHeight);

			//Making the new platform.
			allocated = Instantiate (this.gameObject);
			//Don't want infinitely spawning platforms!
			if (allocated != null) {
				Destroy (allocated.GetComponent<MirrorPlatform> ());
				Destroy (allocated.GetComponent<SpriteRenderer> ());
				allocated.transform.position = mirrored;
				anim.Play ();
			}
		}
    collided.StartSparkles();
    collided.mirror.StartSparkles();

  }

	//Adjust the mirror platform position if necessary.
	protected void OnCollisionStay2D (Collision2D col){
		if ((col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection")
			&& (Input.GetKeyDown("a") || Input.GetKeyDown("d"))) {

			if(collided == null) collided = col.gameObject.GetComponent<Player> ();
				
			Destroy (allocated);
						
			Vector2 mirrored = new Vector2 (-(transform.position.x - collided.transform.position.x) + collided.mirror.transform.position.x,
						                  collided.mirror.getGroundPosition () - collided.getGroundPosition () + allocatedHeight);
	
			//Making the new platform.
			allocated = Instantiate (this.gameObject);
			//Don't want infinitely spawning platforms!
			if (allocated != null) {
				Destroy (allocated.GetComponent<MirrorPlatform> ());
				Destroy (allocated.GetComponent<SpriteRenderer> ());
				allocated.transform.position = mirrored;
			}
		}

    
	}

	// Destroy the mirrored platform on exit.
	protected void OnCollisionExit2D (Collision2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			Destroy (allocated);
			allocated = null;
		}
    collided.StopSparkles();
    collided.mirror.StopSparkles();
  }
}
