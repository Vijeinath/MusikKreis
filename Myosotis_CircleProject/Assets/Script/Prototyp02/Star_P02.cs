using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using System;
using System.Collections.Generic;

public class Star_P02 : MonoBehaviour {

	//Public Attributes
	public ParticleSystem collisionParticle;
	public ParticleSystem trailParticle;
	public AudioClip sound;
	public bool isWrongStar = false;

	//Constants
	private Vector3 startPoint = new Vector3 (0.3f, 0.3f);
	private Vector2 strength = new Vector2 (7.0f, 7.0f);
	private float circleRadius = 1.2f;

	private TransformGesture gesture;
	private Rigidbody2D rb2d;
	private AudioSource source;
	public AudioClip clip;
	private List<AudioClip> listOfSounds;
	private bool touchIsHappening = false;
	private bool isInSmallCircle = false;

	private int degree;
	private float scalePixel = 0.0007f;
	private int deltaDegree = 2;

	// Use this for initialization
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> ();
		source = GetComponent<AudioSource> ();
		listOfSounds = new List<AudioClip> ();
		LoadAudioClips (listOfSounds);
		degree = UnityEngine.Random.Range(0,360);
	}

	public virtual void LoadAudioClips(List<AudioClip> listOfSounds){
		listOfSounds.Add (Resources.Load("Sound/luteJazz") as AudioClip);
		listOfSounds.Add (Resources.Load("Sound/classicfunk01") as AudioClip);
		listOfSounds.Add (Resources.Load("Sound/classicfunk02") as AudioClip);
		listOfSounds.Add (Resources.Load("Sound/2cuteBells") as AudioClip);
		listOfSounds.Add (Resources.Load("Sound/oldStringRise") as AudioClip);
		listOfSounds.Add (Resources.Load("Sound/discoGuitar") as AudioClip);
		listOfSounds.Add (Resources.Load("Sound/harpe01") as AudioClip);
		listOfSounds.Add (Resources.Load("Sound/harpe02") as AudioClip);
		listOfSounds.Add (Resources.Load("Sound/cavaleria") as AudioClip);
	}

	private void OnEnable()
	{
		gesture = GetComponent<TransformGesture> ();
		gesture.TransformStarted += HandleTransformStarted;
		gesture.TransformCompleted += HandleTransformCompleted;
	}

	private void OnDisable()
	{
		gesture.TransformStarted -= HandleTransformStarted;
		gesture.TransformCompleted -= HandleTransformCompleted;
	}

	void HandleTransformStarted(object sender, EventArgs e)
	{
		touchIsHappening = true;
	}

	void HandleTransformCompleted (object sender, EventArgs e)
	{	
		touchIsHappening = false;
	}


	// All the physic stuffs
	void FixedUpdate () 
	{	
		if (touchIsHappening) {
			Vector2 targetPosition = Camera.main.ScreenToWorldPoint (new Vector2 (gesture.ScreenPosition.x, gesture.ScreenPosition.y));
			transform.position = targetPosition;

			trailParticle.transform.position = targetPosition;
			trailParticle.Play ();

		} else {
			rb2d.drag = 2.2f;
			float distanceLength = this.transform.position.magnitude;
			Vector2 objectDistance = this.transform.position - startPoint;

			if ((distanceLength > circleRadius) && !isInSmallCircle)
			{
				rb2d.AddForce (-7 * objectDistance);
			
				trailParticle.transform.position = transform.position;
				trailParticle.Play ();
			} /*else if ((distanceLength < circleRadius) && (isMoving)){
				print ("Rotate");
				transform.rotation = Quaternion.AngleAxis(bla, new Vector3(0,10,0));
				bla = bla + 15.0f;
			}
			*/
		}

	}

	public void AForce(Vector2 v2){
		rb2d.AddForce (-7 * v2);
	}

	//Partikel- und Soundstart nur nach Kreiseintritt
	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Circle_Small") {
			isInSmallCircle = true;
		}

		if ((touchIsHappening) && !isCollisionOutside()) {
			
		}

		if (!touchIsHappening && isCollisionOutside()){
			collisionParticle.transform.position = this.transform.position;
			collisionParticle.Play ();	

			var randomInt = UnityEngine.Random.Range(0,listOfSounds.Count);
			//source.PlayOneShot(listOfSounds[randomInt], 1F);
			source.PlayOneShot(clip, 1F);
		}
	}

	//Rotationsträgheit der Sterne werden erhöht nach Kollision 
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Star") {
			coll.gameObject.GetComponent<Rigidbody2D> ().angularDrag = 1.5f;
			rb2d.angularDrag = 1.5f;

			//Funtkioniert noch nicht
			/*
			ContactPoint2D bla = new ContactPoint2D();
			print ("X:"+bla.point.x + "? Y: " + bla.point.y);
			*/

			//Funktioniert, aber sieht blöd aus
			/*
			Vector3 distance = coll.rigidbody.transform.position - this.transform.position;
			distance =  Vector3.Scale(new Vector2(0.5f,0.5f), distance);
			Vector3 collisionPoint = this.transform.position+distance;
			collisionParticle.transform.position = collisionPoint;
			collisionParticle.Play ();
			*/
		}
	}

	void Update(){
		//Moves Star up and down if it is still
		float length = rb2d.velocity.magnitude;
		if ((length <0.1f) &&(length>-0.1f)) {
			float bla = Mathf.Sin (degree * Mathf.PI / 180) * scalePixel;
			float delta = transform.position.y + bla;
			transform.position = new Vector3(transform.position.x,delta);
			degree = (degree + deltaDegree) % 360;
		}
	}

	bool isCollisionOutside(){
		return this.transform.position.magnitude > circleRadius;
	}

	//
	void OnCollisionExit2D(Collision2D coll) {
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Circle_Small") {
			isInSmallCircle = false;
		}
	}

}
