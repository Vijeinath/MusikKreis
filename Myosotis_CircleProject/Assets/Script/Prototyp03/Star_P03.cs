using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using System;
using System.Collections.Generic;

public class Star_P03 : MonoBehaviour {
	private TransformGesture gesture;
	private Rigidbody2D rb2d;
	private bool touchIsHappening = false;

	private int degree;
	private float scalePixel = 0.001f;
	private int deltaDegree = 2;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		if (this.transform.name.Equals ("")) {
			degree = UnityEngine.Random.Range (0, 360);
		}
		print (this.transform.name);
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

	// Update is called once per frame
	void Update () {
		//Moves Star up and down if it is still
		float length = rb2d.velocity.magnitude;
		if ((length < 0.1f) &&(length > -0.1f)) {
			float bla = Mathf.Sin (degree * Mathf.PI / 180) * scalePixel;
			float delta = transform.position.y + bla;
			transform.position = new Vector3(transform.position.x,delta);
			degree = degree + deltaDegree;
			degree = degree % 360;
		}
	}
}

