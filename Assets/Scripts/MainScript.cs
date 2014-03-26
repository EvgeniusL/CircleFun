using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour 
{
	//Singleton
	static private MainScript _instance;
	static public MainScript instance { get; private set; }

	private int _points;
	public int points { get; set; }
	static public ArrayList bigSprites;
	static public ArrayList smallSprites;

	public int planetAmount;
	public int difficulty;
	public GUIText scoreText;
	public GameObject planetPrefab;
	// Use this for initialization
	public enum GameStates	//States of game
	{
		Menu,
		Play,
		Pause,
	}
	static public GameStates curStates;	//current state of the game
	void Awake()
	{
		instance = this;	//Without lazy initialization
	}
	private void CreatePlanet()
	{
		Instantiate (planetPrefab, Vector3.zero, Quaternion.identity);
	}
	IEnumerator Start () 
	{
		planetAmount = 6;
		difficulty = 1;
		//Big sprites
		WWW www = WWW.LoadFromCacheOrDownload ("file://C:/Users/Evgenius/Documents/CircleFun/AssetBundles/BigSprites.unity3d", 1);
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
		www = WWW.LoadFromCacheOrDownload ("file://C:/Users/Evgenius/Documents/CircleFun/AssetBundles/SmallSprites.unity3d", 1);
		yield return www;
		bundleSprites = www.assetBundle;
		objects = bundleSprites.LoadAll ();
		smallSprites = new ArrayList ();
		foreach (Object spr in objects) 
		{
			if (spr as Sprite)
				smallSprites.Add (spr);
		}
		points = 0;
		curStates = GameStates.Menu;
		bundleSprites.Unload (false);
		for (int i = 0; i < planetAmount; i++)
		{
			CreatePlanet();
		}
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

	}
}
