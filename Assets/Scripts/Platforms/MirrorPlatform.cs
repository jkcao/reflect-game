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

	// Timer on sprite.
	IEnumerator DestroySprite()
	{
		yield return new WaitForSeconds(0.25f);
		if (allocated != null) Destroy(allocated.GetComponent<SpriteRenderer>());
	}

	// When in-contact with the player, dynamically allocate the mirrored platform.
	protected void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			Player p = col.gameObject.GetComponent<Player> ();

			// Finding position mirrored should go to.
			Vector2 mirrored = new Vector2(-(transform.position.x - p.transform.position.x) + p.mirror.transform.position.x,
				p.mirror.getGroundPosition() - p.getGroundPosition() + allocatedHeight);

			//Making the new platform.
			allocated = Instantiate (this.gameObject);
			//Don't want infinitely spawning platforms!
			if (allocated != null) {
				Destroy (allocated.GetComponent<MirrorPlatform> ());
				allocated.transform.position = mirrored;
			}
			StartCoroutine(DestroySprite());
		}
	}

	//Adjust the mirror platform position if necessary.
	protected void OnCollisionStay2D (Collision2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			if (Input.GetKeyDown("a") || Input.GetKeyDown("d") || Input.GetKeyDown("left") || Input.GetKeyDown("right")) {

				Destroy (allocated);

				Player p = col.gameObject.GetComponent<Player> ();
				Vector2 mirrored = new Vector2 (-(transform.position.x - p.transform.position.x) + p.mirror.transform.position.x,
					                  p.mirror.getGroundPosition () - p.getGroundPosition () + allocatedHeight);

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
	}

	// Destroy the mirrored platform on exit.
	protected void OnCollisionExit2D (Collision2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			Destroy (allocated);
			allocated = null;
		}
	}
}
