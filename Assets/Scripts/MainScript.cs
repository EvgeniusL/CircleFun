using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour 
{
	public delegate void GameAction();
	public static event GameAction OnGameOver;

	//Singleton
	static private MainScript _instance;
	static public MainScript instance { get; private set; }
	
	public int difficulty	{ get; set; }
	public int points 		{ get; set; }
	public ArrayList bigSprites;	//256x256
	public ArrayList smallSprites;	//128x128

	public GUIText scoreText;
	public GUIText gameTimer;
	public GameObject planetPrefab;
	public GameObject playButton;

	private float gameTime;
	private float timeToPlay;

	//Screen borders
	public float leftBorder;
	public float rightBorder;
	public float bottomBorder;

	private int planetsAmount;
	private ArrayList planets;
	public enum GameStates	//States of game
	{
		Menu,
		Play
	}
	public GameStates curState;	//current state of the game
	void Awake()
	{
		instance = this;	//Without lazy initialization
	}

	IEnumerator Start () 
	{
		planets = new ArrayList ();
		planetsAmount = 5;
		leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, -10)).x;
		rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, -10)).x;
		bottomBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, -10)).y;
		//Big sprites
		WWW www = WWW.LoadFromCacheOrDownload ("file://C:/Users/Evgenius/Documents/CircleFun/Assets/Bundles/BigSprites.unity3d", 1);
		yield return www;
		AssetBundle bundleSprites = www.assetBundle;
		Object [] objects = bundleSprites.LoadAll ();
		bigSprites = new ArrayList ();
		foreach (Object spr in objects) 
		{
			if(spr as Sprite)
				bigSprites.Add(spr);
		}
		bundleSprites.Unload (false);
		//Small sprites
		www = WWW.LoadFromCacheOrDownload ("file://C:/Users/Evgenius/Documents/CircleFun/Assets/Bundles/SmallSprites.unity3d", 1);
		yield return www;	
		bundleSprites = www.assetBundle;
		objects = bundleSprites.LoadAll ();
		smallSprites = new ArrayList ();
		foreach (Object spr in objects) 
		{
			if (spr as Sprite)
				smallSprites.Add (spr);
		}
		curState = GameStates.Menu;
		bundleSprites.Unload (false);
	}
	private void CreatePlanet(int amount)
	{
		if(amount > 0)
		{
			for(int i = 0; i < amount; i++)
				planets.Add(Instantiate (planetPrefab, Vector3.zero, Quaternion.identity));
		}
	}
	public void GameStart()
	{
		if (planets.Count < planetsAmount)
		{
			CreatePlanet (planetsAmount - planets.Count);
		}
		curState = GameStates.Play;
		gameTime = Time.time;
		points = 0;
		UpdateScore ();
		difficulty = 1;
		timeToPlay = 30;
	}
	public void GameOver()
	{
		difficulty = 1;
		OnGameOver ();
		curState = GameStates.Menu;
		playButton.SetActive (true);
	}
	
	public void UpdateScore()
	{
		scoreText.text = "Score: " + points;
		if (difficulty == 1 && points > 500) 
		{
			difficulty++;
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(curState == GameStates.Play)
		{
			if((int)(Time.time - gameTime) < timeToPlay)
			{
				gameTimer.text = "Time left: " + (timeToPlay - (int)(Time.time - gameTime));
			}
			else
			{
				gameTimer.text = "Time left: " + (timeToPlay - (int)(Time.time - gameTime));

				GameOver();
			}
		}
	}
}