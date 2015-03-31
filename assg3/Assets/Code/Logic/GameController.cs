using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	Graph g;
	// Use this for initialization
	void Start () {
		g = new Graph ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateGraph()
	{
		GameObject.FindGameObjectWithTag ("Menu").SetActive (false);
	}
}

