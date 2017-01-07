using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Behaviors;
using System;
using System.Collections.Generic;

public class Planet: MonoBehaviour {
	//Public attributes/ properties
	public float RotationSpeed = 20f;
	public bool galaxyAppeared = false;
	public bool hitByStar = false;
	public int Rounds
	{
		get
		{
			return rounds;
		}
		set 
		{	
			if (value >= 0) 
			{
				rounds = value;
			}
		}
	}
		
	//Private attributes
	private int rounds = 0;
	private float degree = 90f;
	private float degreeDelta = 0.25f;
	private float radius = 2.5f;
	private PressGesture gesture;
	private Sprite[] planetSprites;

	private void OnEnable()
	{
		gesture = GetComponent<PressGesture> ();
		gesture.Pressed += PressHandler;
	}

	private void OnDisable()
	{
		gesture.Pressed -= PressHandler;
	}

	private void ChangePlanet(string fileName){
		int number = (Int32.Parse(fileName.Split ('_') [1])) % 4;
		this.GetComponent<SpriteRenderer> ().sprite = planetSprites [number];
	}

	void PressHandler(object sender, EventArgs e)
	{
		ChangePlanet (this.GetComponent<SpriteRenderer>().sprite.name);
	}
		
	// Use this for initialization
	void Start () {
		planetSprites = new Sprite[4];
		planetSprites[0] = Resources.Load<Sprite> ("Sprites/planet_1");
		planetSprites[1] = Resources.Load<Sprite> ("Sprites/planet_2");
		planetSprites[2] = Resources.Load<Sprite> ("Sprites/planet_3");
		planetSprites[3] = Resources.Load<Sprite> ("Sprites/planet_4");
	}
	
	// Update is called once per frame
	void Update () {
		if (galaxyAppeared) {
			transform.Rotate(0, 0, Time.deltaTime*RotationSpeed);
			Orbit ();
		}	
	}

	void Fixed(){
	}

	private void Orbit(){
		this.transform.position = new Vector2 (Mathf.Cos (degree * Mathf.PI / 180)*radius,Mathf.Sin (degree * Mathf.PI / 180)*radius);
		if ((degree  + degreeDelta) == 90) {
			rounds++;
		}
		degree = (degree + degreeDelta) %360;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Star") {
			hitByStar = true;
		}
	}

}