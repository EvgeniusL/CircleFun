using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour 
{
	private int _points;
	private Vector3 _speed;
	private float _size;
	private Vector3 _destination;
	private Vector3 _startPosition;
	private SpriteRenderer sprRend;				//Sprite component
	
	public int points { get; set; }				//Points for destruction
	public Vector3 speed { get; set; }			//Speed of falling
	public float size { get; set; }				//Size of planet
	public Vector3 destination { get; set; }   	//Final destination
	public Vector3 startPosition { get; set; } 	//StartPosition

	private Vector3 temp;
	void Start()
	{
		sprRend = GetComponent<SpriteRenderer> ();
		size = Random.Range(0.3f, 1f);		//Set size
		transform.localScale *= size;		//Change size
		points = 100 - (int)(size * 100);	//Set amount of points

		Vector3 temp = new Vector3 ();
		temp.y = -2f;
		destination = temp;
		temp.y = (1.3f - size)* -0.1f;
		speed = temp;

		startPosition = new Vector3 (Random.Range (-3.0f, 3.0f), 5f, 0f);
		transform.position = startPosition;
	}
	void OnMouseDown()
	{
		Pop (true);
	}
	void ReConstruct()	//Object pool
	{
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

		startPosition = new Vector3 (Random.Range (-3.0f, 3.0f), 5f, 0f);
		transform.position = startPosition;

		if (size <= 0.575) 
		{
			if (MainScript.instance.difficulty == 1)
					sprRend.sprite = MainScript.smallSprites [Random.Range (0, MainScript.smallSprites.Count / 2)] as Sprite;
			else if (MainScript.instance.difficulty == 2)
					sprRend.sprite = MainScript.smallSprites [Random.Range (MainScript.smallSprites.Count / 2, MainScript.smallSprites.Count)] as Sprite;
		}
		else
		{
			if (MainScript.instance.difficulty == 1)
				sprRend.sprite = MainScript.bigSprites [Random.Range (0, MainScript.bigSprites.Count / 2)] as Sprite;
			else if (MainScript.instance.difficulty == 2)
				sprRend.sprite = MainScript.bigSprites [Random.Range (MainScript.bigSprites.Count / 2, MainScript.bigSprites.Count)] as Sprite;

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
		transform.localScale /= size;
		ReConstruct ();
	}
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (speed);
		if (transform.position.y <= destination.y) 
		{
			Pop(false);
		}
	}
}
