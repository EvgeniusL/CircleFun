using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {
	private int _points;
	static public int points { get; set; }
	// Use this for initialization
	public enum GameStates	//States of game
	{
		Menu,
		Play,
		Pause,
	}
	static public GameStates curStates;	//current state of the game
	void Start () 
	{
		points = 0;
		curStates = GameStates.Menu;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
