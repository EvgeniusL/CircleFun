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

	void Start()
	{
		sprRend = GetComponent<SpriteRenderer> ();
		size = Random.Range(0.25f, 1f);		//Set size
		transform.localScale *= size;		//Change size
		points = 100 - (int)(size * 100);	//Set amount of points

		Vector3 temp = new Vector3 ();
		temp.y = -2f;
		destination = temp;
		temp.y = (1f - size)* -0.2f;
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
		sprRend.sprite = MainScript.sprites [Random.Range (0, MainScript.sprites.Count)] as Sprite;
		size = Random.Range(0.25f, 1f);		//Set size
		transform.localScale *= size;		//Change size
		points = 100 - (int)(size * 100);	//Set amount of points
		
		Vector3 temp = new Vector3 ();
		temp.y = -2f;
		destination = temp;
		temp.y = (1f - size)* -0.2f;
		speed = temp;
		
		startPosition = new Vector3 (Random.Range (-3.0f, 3.0f), 5f, 0f);
		transform.position = startPosition;
	}
	void Pop(bool countPoints)	//Poping the planet
	{
		if (countPoints)
			MainScript.points += points;
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
