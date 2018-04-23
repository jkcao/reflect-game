using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlatform : MonoBehaviour {

	private float allocatedHeight;
	private GameObject allocated;
	private GameObject firstAlloc;
	private Player collided;
	private Animator anim;
	private bool animPlaying;
	private AudioSource sound;

	// Use this for initialization.
	void Start () {
		allocatedHeight = this.transform.position.y;

		allocated = null;

		this.GetComponent<Animator>().enabled = false;
		sound = this.GetComponent<AudioSource> ();
		animPlaying = false;
	}

	IEnumerator animTimer() {
		yield return new WaitForSeconds(1.2f);
		Destroy(anim);
		animPlaying = false;
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
			firstAlloc = allocated;
			//Don't want infinitely spawning platforms!
			if (allocated != null) {
				sound.Play ();
				anim = allocated.GetComponent<Animator> ();
				anim.enabled = true;
				animPlaying = true;
				anim.Play ("MirrorPlatform");
				StartCoroutine (animTimer());
				Destroy (allocated.GetComponent<MirrorPlatform> ());
				allocated.transform.position = mirrored;

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

			Vector2 mirrored = new Vector2 (-(transform.position.x - collided.transform.position.x) + collided.mirror.transform.position.x,
						                  collided.mirror.getGroundPosition () - collided.getGroundPosition () + allocatedHeight);
	
			if (allocated != firstAlloc && allocated != null) Destroy (allocated);
			if (!animPlaying && firstAlloc != null) Destroy (firstAlloc);

			//Making the new platform.
			allocated = Instantiate (this.gameObject);
			//Don't want infinitely spawning platforms!
			if (allocated != null) {
				Destroy (allocated.GetComponent<MirrorPlatform> ());
				Destroy (allocated.GetComponent<SpriteRenderer> ());
				Destroy (allocated.GetComponent<Animator> ());
				allocated.transform.position = mirrored;
			}
		}

    
	}

	// Destroy the mirrored platform on exit.
	protected void OnCollisionExit2D (Collision2D col){
		if (col.gameObject.tag == "Character" || col.gameObject.tag == "Reflection") {
			if (allocated != firstAlloc) Destroy (firstAlloc);
			Destroy (allocated);
		}
    collided.StopSparkles();
    collided.mirror.StopSparkles();
  }
}
