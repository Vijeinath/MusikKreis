using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public ParticleSystem circlePs;
	public GameObject prefabCollectable;

	private List<AudioClip>[] audioSource;
	private Star_P02 star1;
	private Star_P02 star2;
	private Star_P02 star3;
	private Star_P02 star4;
	private GameObject circleBig;
	private PlanetEarth planet;
	private AudioSource source;
	private List<AudioClip> listOfSounds;
	private List<GameObject> collectables;
	private int randomInt;
	private readonly int numberOfCollactables = 1;

	private DateTime startTime;
	private bool galaxyStarted = false;

	private int degree = 0;
	private int degreeDelta = 14;
	private float radius = 1.3f;

	// Use this for initialization
	void Start () {
		//Stars referenzieren
		star1 = GameObject.Find ("Star 01").GetComponent<Star_P02>();
		star2 = GameObject.Find ("Star 02").GetComponent<Star_P02>();
		star3 = GameObject.Find ("Star 03").GetComponent<Star_P02>();
		star4 = GameObject.Find ("Star 04").GetComponent<Star_P02>();
		circleBig = GameObject.Find ("Circle Big");
		planet = GameObject.Find ("Earth").GetComponent<PlanetEarth> ();

		Color temp = circleBig.GetComponent<SpriteRenderer> ().color;
		temp.a = 0f;
		circleBig.GetComponent<SpriteRenderer> ().color = temp;
		circleBig.SetActive (false);

		//Sounds hinzufügen
		audioSource = new List<AudioClip>[7];
		for (int i = 0; i<audioSource.Length; i++){
			audioSource[i] = new List<AudioClip> ();
		}
			
		audioSource[0].Add (Resources.Load("Flute/flute01") as AudioClip);
		audioSource[0].Add (Resources.Load("Flute/flute02") as AudioClip);
		audioSource[0].Add (Resources.Load("Flute/flute03") as AudioClip);
		audioSource[1].Add (Resources.Load("Guitar/guitar01") as AudioClip);
		audioSource[1].Add (Resources.Load("Guitar/guitar02") as AudioClip);
		audioSource[1].Add (Resources.Load("Guitar/guitar03") as AudioClip);
		audioSource[2].Add (Resources.Load("Harpe/harpe01") as AudioClip);
		audioSource[2].Add (Resources.Load("Harpe/harpe02") as AudioClip);
		audioSource[2].Add (Resources.Load("Harpe/harpe03") as AudioClip);
		audioSource[3].Add (Resources.Load("Piano/piano01") as AudioClip);
		audioSource[3].Add (Resources.Load("Piano/piano02") as AudioClip);
		audioSource[3].Add (Resources.Load("Piano/piano03") as AudioClip);
		audioSource[4].Add (Resources.Load("Sax/sax01") as AudioClip);
		audioSource[4].Add (Resources.Load("Sax/sax02") as AudioClip);
		audioSource[4].Add (Resources.Load("Sax/sax03") as AudioClip);
		audioSource[5].Add (Resources.Load("Trumpet/trumpet01") as AudioClip);
		audioSource[5].Add (Resources.Load("Trumpet/trumpet02") as AudioClip);
		audioSource[5].Add (Resources.Load("Trumpet/trumpet03") as AudioClip);
		audioSource[6].Add (Resources.Load("Xylophone/xylophone01") as AudioClip);
		audioSource[6].Add (Resources.Load("Xylophone/xylophone01") as AudioClip);
		audioSource[6].Add (Resources.Load("Xylophone/xylophone01") as AudioClip);

		star1.clip = audioSource [0] [1];
		star2.clip = audioSource [0] [0];
		star3.clip = audioSource [0] [2];
		star4.clip = audioSource [4] [2];

		collectables = new List<GameObject> ();
		for (int i = 0; i < 8; i++) {
			collectables.Add(Instantiate(prefabCollectable) as GameObject);
			collectables [i].transform.position= new Vector2(0.1f*i,0.2f*i);
		}	

		//Zum Testen
		//source = GetComponent<AudioSource> ();
		//source.PlayOneShot(audioSource[6][1], 1F);
	}

	// Update is called once per frame
	void Update () {
		
		if (planet.hitByStar){
			LoadNewSounds ();
			planet.hitByStar = false;
		}

		if (!circlePs.IsAlive()) {
			//Randomize Collectables Positions

		}

		DrawGalaxy ();
		int sum = star1.GetCollectedNumber () + star2.GetCollectedNumber () + star3.GetCollectedNumber () + star4.GetCollectedNumber ();
		if (sum == numberOfCollactables) {
			if (!galaxyStarted) {
				startTime = DateTime.Now;
			}
			galaxyStarted = true;
			circleBig.SetActive (true);
			planet.galaxyAppeared = true;
			circlePs.Play ();
			if (galaxyStarted) {
				TimeSpan durationGalaxy = DateTime.Now - startTime;
				if (durationGalaxy.Seconds > 5) {
					star1.ResetCollected ();
					star2.ResetCollected ();
					star3.ResetCollected ();
					star4.ResetCollected ();
					galaxyStarted = false;
					planet.galaxyAppeared = false;
					circleBig.SetActive (false);
					circlePs.Stop(true);
				}
			}

		}
	}

	void DrawGalaxy(){
		circlePs.transform.position = new Vector2 (Mathf.Cos (degree * Mathf.PI / 180)*radius,Mathf.Sin (degree * Mathf.PI / 180)*radius);
		degree = (degree + degreeDelta) %360;
	}

	public void LoadNewSounds (){
		var tempRandom = UnityEngine.Random.Range(0,7);
		while (tempRandom == randomInt) {
			tempRandom = UnityEngine.Random.Range(0,7);
		}
		randomInt = tempRandom;

		switch (randomInt) {
		case 0:
			star1.clip = audioSource [3] [2];
			star2.clip = audioSource [0] [2];
			star3.clip = audioSource [3] [0];
			star4.clip = audioSource [3] [1];
			break;
		case 1:
			star1.clip = audioSource [6] [1];
			star2.clip = audioSource [1] [2];
			star3.clip = audioSource [1] [0];
			star4.clip = audioSource [1] [1];
			break;
		case 2:
			star1.clip = audioSource [6] [1];
			star2.clip = audioSource [6] [2];
			star3.clip = audioSource [2] [0];
			star4.clip = audioSource [6] [0];
			break;
		case 3:
			star1.clip = audioSource [0] [1];
			star2.clip = audioSource [0] [0];
			star3.clip = audioSource [0] [2];
			star4.clip = audioSource [4] [2];
			break;
		case 4:
			star1.clip = audioSource [3] [1];
			star2.clip = audioSource [2] [2];
			star3.clip = audioSource [2] [0];
			star4.clip = audioSource [2] [1];
			break;
		case 5:
			star1.clip = audioSource [5] [1];
			star2.clip = audioSource [5] [2];
			star3.clip = audioSource [5] [0];
			star4.clip = audioSource [1] [1];
			break;
		case 6:
			star1.clip = audioSource [4] [1];
			star2.clip = audioSource [4] [2];
			star3.clip = audioSource [0] [0];
			star4.clip = audioSource [4] [0];
			break;
		default:
			break;		
		}
	}


}
