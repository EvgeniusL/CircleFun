using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour {

	void OnMouseDown()
	{
		MainScript.instance.GameStart ();
		gameObject.SetActive (false);
	}
}
