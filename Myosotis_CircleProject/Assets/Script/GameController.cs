using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 * This class manages the two minigames and coordinates between the other objects like the stars, planet, galaxy and collectables.
*/
public class GameController : MonoBehaviour {
	// Public attributes
	public ParticleSystem createCollectable;
	public GameObject prefabCollectable;
	public Star star1;
	public Star star2;
	public Star star3;
	public Star star4;
	public Planet planet;
	public Galaxy galaxy;
	public OuterSpace outerSpace;
	public int planetCycle;
	public int numberOfCollactables;
	public bool starColorChange;

	// Private attributes
	private List<AudioClip>[] audioSource;
	private List<AudioClip> listOfSounds;
	private List<GameObject> collectables;
	private int randomInt = -1;
	private DateTime startTime;
	private bool galaxyStarted = false;
	private bool galaxyJustStopped = false;
	private int degree = 0;
	private int degreeDelta = 14;
	private float radius = 1.3f;
	// Instrument variables for the array
	private int flute = 0;
	private int guitar = 1;
	private int harpe = 2;
	private int piano = 3;
	private int saxophone = 4;
	private int trumpet = 5;
	private int xylophone = 6;

	// Initialization
	void Start () {
		DeactivateGalaxy ();
		LoadSoundFiles ();
		RandomInstrumentMusic ();

		collectables = new List<GameObject> ();
		for (int i = 0; i < numberOfCollactables; i++) {
			collectables.Add(Instantiate(prefabCollectable) as GameObject); 
		}	
		PositionCollectables ();
		StartCoroutine(ShowCollectables ());
	}
		
	private void DeactivateGalaxy(){
		Color temp = galaxy.GetComponent<SpriteRenderer> ().color;
		temp.a = 0f;
		galaxy.GetComponent<SpriteRenderer> ().color = temp;
		galaxy.SetActive (false);
	}	

	private void LoadSoundFiles(){
		audioSource = new List<AudioClip>[7];
		for (int i = 0; i<audioSource.Length; i++){
			audioSource[i] = new List<AudioClip> ();
		}

		audioSource[flute].Add (Resources.Load("Flute/flute01") as AudioClip);
		audioSource[flute].Add (Resources.Load("Flute/flute02") as AudioClip);
		audioSource[flute].Add (Resources.Load("Flute/flute03") as AudioClip);
		audioSource[flute].Add (Resources.Load("Flute/flute04") as AudioClip);
		audioSource[guitar].Add (Resources.Load("Guitar/guitar01") as AudioClip);
		audioSource[guitar].Add (Resources.Load("Guitar/guitar02") as AudioClip);
		audioSource[guitar].Add (Resources.Load("Guitar/guitar03") as AudioClip);
		audioSource[guitar].Add (Resources.Load("Guitar/guitar04") as AudioClip);
		audioSource[harpe].Add (Resources.Load("Harpe/harpe01") as AudioClip);
		audioSource[harpe].Add (Resources.Load("Harpe/harpe02") as AudioClip);
		audioSource[harpe].Add (Resources.Load("Harpe/harpe03") as AudioClip);
		audioSource[harpe].Add (Resources.Load("Harpe/harpe04") as AudioClip);
		audioSource[piano].Add (Resources.Load("Piano/piano01") as AudioClip);
		audioSource[piano].Add (Resources.Load("Piano/piano02") as AudioClip);
		audioSource[piano].Add (Resources.Load("Piano/piano03") as AudioClip);
		audioSource[piano].Add (Resources.Load("Piano/piano04") as AudioClip);
		audioSource[saxophone].Add (Resources.Load("Sax/sax01") as AudioClip);
		audioSource[saxophone].Add (Resources.Load("Sax/sax02") as AudioClip);
		audioSource[saxophone].Add (Resources.Load("Sax/sax03") as AudioClip);
		audioSource[saxophone].Add (Resources.Load("Sax/sax04") as AudioClip);
		audioSource[trumpet].Add (Resources.Load("Trumpet/trumpet01") as AudioClip);
		audioSource[trumpet].Add (Resources.Load("Trumpet/trumpet02") as AudioClip);
		audioSource[trumpet].Add (Resources.Load("Trumpet/trumpet03") as AudioClip);
		audioSource[trumpet].Add (Resources.Load("Trumpet/trumpet04") as AudioClip);
		audioSource[xylophone].Add (Resources.Load("Xylophone/xylophone01") as AudioClip);
		audioSource[xylophone].Add (Resources.Load("Xylophone/xylophone02") as AudioClip);
		audioSource[xylophone].Add (Resources.Load("Xylophone/xylophone03") as AudioClip);
		audioSource[xylophone].Add (Resources.Load("Xylophone/xylophone04") as AudioClip);
	}	

	private void PositionCollectables(){
		for (int i = 0; i < numberOfCollactables; i++) {
			Vector2 tempVector = new Vector2(UnityEngine.Random.Range(-2.8f,2.8f),UnityEngine.Random.Range(-1.4f,1.4f));
			while ((Mathf.Abs((tempVector.magnitude - star1.transform.position.magnitude)) < 0.2) || 
				(Mathf.Abs((tempVector.magnitude - star2.transform.position.magnitude)) < 0.2) ||
				(Mathf.Abs((tempVector.magnitude - star3.transform.position.magnitude)) < 0.2) ||
				(Mathf.Abs((tempVector.magnitude - star4.transform.position.magnitude)) < 0.2)){
				tempVector = new Vector2(UnityEngine.Random.Range(-2.8f,2.8f),UnityEngine.Random.Range(-1.4f,1.4f));
			}
			collectables [i].transform.position = tempVector;
			collectables [i].SetActive (false);
		}
	}

	private IEnumerator ShowCollectables(){
		for (int i = 0; i < numberOfCollactables; i++) {
			createCollectable.transform.position = collectables [i].transform.position;
			createCollectable.Play ();
			collectables [i].SetActive (true);
			yield return new WaitWhile(()=> createCollectable.isPlaying);
			//yield return new WaitForSeconds(0.1F);
		}
	}

	private void ChangeStarColor(){
		star1.ChangeColor ();
		star2.ChangeColor ();
		star3.ChangeColor ();
		star4.ChangeColor ();
	}

	// Update is called once per frame
	void Update () {
		if (planet.hitByStar){
			if (starColorChange) {
				ChangeStarColor();
			}
			RandomInstrumentMusic ();
			planet.hitByStar = false;
		}

		if (!galaxy.IsAlive() && galaxyJustStopped) {
			galaxy.SetActive (false);
			PositionCollectables();
			StartCoroutine(ShowCollectables ());
			galaxyJustStopped = false;
		}

		DrawGalaxy ();
		int sum = star1.GetCollectedNumber () + star2.GetCollectedNumber () + star3.GetCollectedNumber () + star4.GetCollectedNumber ();
		if (sum == numberOfCollactables) {
			if (!galaxyStarted) {
				startTime = DateTime.Now;
			}
				
			galaxy.Play ();
			planet.galaxyAppeared = true;
			star1.galaxyAppeared = true;
			star2.galaxyAppeared = true;
			star3.galaxyAppeared = true;
			star4.galaxyAppeared = true;
			galaxyStarted = true;

			bool isStar1Inside = !star1.isCollisionOutside() || star1.isTouched();
			bool isStar2Inside = !star2.isCollisionOutside() || star2.isTouched();
			bool isStar3Inside = !star3.isCollisionOutside() || star3.isTouched();
			bool isStar4Inside = !star4.isCollisionOutside() || star4.isTouched();
			if (isStar1Inside && isStar2Inside && isStar3Inside && isStar4Inside) {
				galaxy.SetActive (true);
			}

			if (galaxyStarted) {
				TimeSpan durationGalaxy = DateTime.Now - startTime;
				if (/*durationGalaxy.Seconds > 30*/ planet.Rounds == planetCycle) {
					star1.ResetCollected ();
					star2.ResetCollected ();
					star3.ResetCollected ();
					star4.ResetCollected ();
					galaxyStarted = false;
					planet.galaxyAppeared = false;
					star1.galaxyAppeared = false;
					star2.galaxyAppeared = false;
					star3.galaxyAppeared = false;
					star4.galaxyAppeared = false;
					galaxy.Stop();
					galaxyJustStopped = true;
					planet.Rounds = 0;
				}
			}
		}
	}

	void DrawGalaxy(){
		galaxy.SetPosition(new Vector2 (Mathf.Cos (degree * Mathf.PI / 180)*radius,Mathf.Sin (degree * Mathf.PI / 180)*radius));
		degree = (degree + degreeDelta) %360;
	}

	public void RandomInstrumentMusic (){
		var tempRandom = UnityEngine.Random.Range(0,7);
		while (tempRandom == randomInt) {
			tempRandom = UnityEngine.Random.Range(0,7);
		}
		randomInt = tempRandom;

		int[] tempArray = new int[4];
		tempArray [0] = 0;
		tempArray [1] = 1;
		tempArray [2] = 2;
		tempArray [3] = 3;
		for (int i = 0; i < tempArray.Length; i++) {
			tempRandom = UnityEngine.Random.Range (0, 4);
			var tempNumber = tempArray [i];
			tempArray[i] = tempArray[tempRandom];
			tempArray [tempRandom] = tempNumber;
		}

		switch (randomInt) {
		case 0:
			star1.Clip = audioSource [piano] [tempArray [0]];
			star2.Clip = audioSource [piano] [tempArray [1]];
			star3.Clip = audioSource [piano] [tempArray [2]];
			star4.Clip = audioSource [piano] [tempArray [3]];
			break;
		case 1:
			star1.Clip = audioSource [guitar] [tempArray [0]];
			star2.Clip = audioSource [guitar] [tempArray [1]];
			star3.Clip = audioSource [guitar] [tempArray [2]];
			star4.Clip = audioSource [guitar] [tempArray [3]];
			break;
		case 2:
			star1.Clip = audioSource [xylophone] [tempArray [0]];
			star2.Clip = audioSource [xylophone] [tempArray [1]];
			star3.Clip = audioSource [xylophone] [tempArray [2]];
			star4.Clip = audioSource [xylophone] [tempArray [3]];
			break;
		case 3:
			star1.Clip = audioSource [flute] [tempArray [0]];
			star2.Clip = audioSource [flute] [tempArray [1]];
			star3.Clip = audioSource [flute] [tempArray [2]];
			star4.Clip = audioSource [flute] [tempArray [3]];
			break;
		case 4:
			star1.Clip = audioSource [harpe] [tempArray [0]];
			star2.Clip = audioSource [harpe] [tempArray [1]];
			star3.Clip = audioSource [harpe] [tempArray [2]];
			star4.Clip = audioSource [harpe] [tempArray [3]];
			break;
		case 5:
			star1.Clip = audioSource [trumpet] [tempArray [0]];
			star2.Clip = audioSource [trumpet] [tempArray [1]];
			star3.Clip = audioSource [trumpet] [tempArray [2]];
			star4.Clip = audioSource [trumpet] [tempArray [3]];
			break;
		case 6:
			star1.Clip = audioSource [saxophone] [tempArray [0]];
			star2.Clip = audioSource [saxophone] [tempArray [1]];
			star3.Clip = audioSource [saxophone] [tempArray [2]];
			star4.Clip = audioSource [saxophone] [tempArray [3]];
			break;
		default:
			break;		
		}
	}

}