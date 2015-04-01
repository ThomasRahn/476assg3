using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public static int score = 0;
	// Use this for initialization
	public static Graph graph;
	void Start () {
		GameController.graph = new Graph();
	}
	
	// Update is called once per frame
	void Update () {
		//Set Score here

	}

	public void GenerateGraph()
	{
		GameObject.FindGameObjectWithTag ("Menu").SetActive (false);
	}

	public static void IncreaseScore()
	{
		score++;
	}
}

