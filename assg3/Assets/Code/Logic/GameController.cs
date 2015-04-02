using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public static int p1score = 0;
	public static int p2score = 0;
	// Use this for initialization
	public static Graph graph;

	public GameObject menu;
	public GameObject scorePanel;


	void Start () {
		GameController.graph = new Graph();
	}
	
	// Update is called once per frame
	void Update () {
		//Set Score here

	}

	public void GenerateGraph()
	{
		menu.SetActive (false);
		scorePanel.SetActive (true);
	}


	public static void IncreaseScore(bool p1)
	{
		if (p1) {
			p1score++;
			GameObject.Find("Player1Score").GetComponent<Text>().text = "P1 Score: " + p1score;

		} else {
			p2score++;
			GameObject.Find("Player2Score").GetComponent<Text>().text = "P2 Score: " + p1score;
		}
	}



}

