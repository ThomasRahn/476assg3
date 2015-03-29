using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	Graph g;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateGraph()
	{
		g = new Graph ();
		GameObject.FindGameObjectWithTag ("Menu").SetActive (false);
	}
}

