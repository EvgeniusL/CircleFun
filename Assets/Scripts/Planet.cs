using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour 
{
	private SpriteRenderer sprRend;					//Sprite component
	private Vector3 startSize;
	private int points 				{ get; set; }	//Points for destruction
	private Vector3 speed 			{ get; set; }	//Speed of falling
	private float size				{ get; set; }	//Size of planet
	private Vector3 destination 	{ get; set; }   //Final destination
	private Vector3 startPosition 	{ get; set; } 	//StartPosition

	static private Vector3 temp;	//For calculations
	void Start()
	{
		startSize = new Vector3 (0.5f, 0.5f, 1f);
		MainScript.OnGameOver += ReConstruct;
		sprRend = GetComponent<SpriteRenderer> ();
		ReConstruct ();
	}
	void OnMouseDown()
	{
		Pop (true);
	}
	void ReConstruct()	//Object pool
	{
		transform.localScale = startSize;
		size = Random.Range(0.3f, 1f);		//Set size
		transform.localScale *= size;		//Change size
		points = 100 - (int)(size * 100);	//Set amount of points
		
		temp = new Vector3 ();
		temp.y = -2f;
		destination = temp;
		if(MainScript.instance.difficulty == 1)
			temp.y = (1.3f - size)* -0.1f;
		else if(MainScript.instance.difficulty == 2)
			temp.y = (1.3f - size)* -0.15f;
		speed = temp;
		//Repeat initialization for left and right border, to check screen width in runtime
		startPosition = new Vector3 (Random.Range (MainScript.instance.leftBorder + size/2, MainScript.instance.rightBorder - size/2), 5f, 0f);
		transform.position = startPosition;

		if (size <= 0.575) 
		{
			if (MainScript.instance.difficulty == 1)
				sprRend.sprite = MainScript.instance.smallSprites [Random.Range (0, MainScript.instance.smallSprites.Count / 2)] as Sprite;
			else if (MainScript.instance.difficulty == 2)
				sprRend.sprite = MainScript.instance.smallSprites [Random.Range (MainScript.instance.smallSprites.Count / 2, MainScript.instance.smallSprites.Count)] as Sprite;
		}
		else
		{
			if (MainScript.instance.difficulty == 1)
				sprRend.sprite = MainScript.instance.bigSprites [Random.Range (0, MainScript.instance.bigSprites.Count / 2)] as Sprite;
			else if (MainScript.instance.difficulty == 2)
				sprRend.sprite = MainScript.instance.bigSprites [Random.Range (MainScript.instance.bigSprites.Count / 2, MainScript.instance.bigSprites.Count)] as Sprite;
		}
	}
	void Pop(bool countPoints)	//Poping the planet
	{
		audio.Play ();
		if (countPoints) 
		{
			MainScript.instance.points += points;
			MainScript.instance.UpdateScore();
		}
		ReConstruct ();
	}
	// Update is called once per frame
	void Update () 
	{
		if (MainScript.instance.curState == MainScript.GameStates.Play) 
		{
			transform.Translate (speed);
			if (transform.position.y <= (MainScript.instance.bottomBorder - size)) 
			{
				Pop (false);
			}
		}
	}
}
