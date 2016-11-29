using UnityEngine;
using System.Collections;

public class CircleSmall: MonoBehaviour {
	private GameController controller;
	private Rigidbody2D rb2d;
	public ParticleSystem winParticle;
	public bool isfound = false;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Star") {
			
				//controller = GameObject.Find (other.gameObject.transform.name).GetComponent<GameController>();
				//other.gameObject.transform.position = new Vector3 (2.7f, 0.0f, 0.0f);
				//print(other.gameObject.transform.position.x);
				//print(other.gameObject.transform.position.magnitude);
				Rigidbody2D rf = other.gameObject.GetComponent<Rigidbody2D>();
				rf.AddForce(new Vector3 (-100f, -10f, 0.0f));
				//other.attachedRigidbody.AddForce(new Vector3 (10f, 10f, 0.0f));
				if (other.GetComponent<Star_P02> ().isWrongStar) {	
					isfound = true;
					winParticle.Play ();
					//controller.NewAudios();
				}
		}
	}


}
