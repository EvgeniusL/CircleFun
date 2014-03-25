using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {
	private int _points;
	static public int points { get; set; }
	static public ArrayList sprites;
	// Use this for initialization
	public enum GameStates	//States of game
	{
		Menu,
		Play,
		Pause,
	}
	static public GameStates curStates;	//current state of the game
	IEnumerator Start () 
	{
		WWW www = WWW.LoadFromCacheOrDownload ("file://C:/Users/Evgenius/Documents/CircleFun/AssetBundles/sprites.unity3d", 1);
		yield return www;
		AssetBundle bundleSprites = www.assetBundle;
		Object [] objects = bundleSprites.LoadAll ();
		sprites = new ArrayList ();
		foreach (Object spr in objects) 
		{
			if(spr as Sprite)
				sprites.Add(spr);
		}
		points = 0;
		curStates = GameStates.Menu;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
