using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public static int p1score = 0;
	public static int p2score = 0;
	// Use this for initialization
	public static Graph graph;
	public static bool playersConnected = false;

	public GameObject menu;
	public GameObject scorePanel;
	public GameObject statusPanel;


	void Start () {
		GameController.graph = new Graph();

		AddSpecialNode(new Vector3(-14.0f, 0.0f, -7.3f));
		AddSpecialNode(new Vector3(-14.0f, 0.0f, 7.7f));
		AddSpecialNode(new Vector3(14.0f, 0.0f, -7.3f));
		AddSpecialNode(new Vector3(14.0f, 0.0f, 7.7f));

	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] dots = GameObject.FindGameObjectsWithTag ("cube");
		if (dots.Length == 0) {
			statusPanel.SetActive(true);
			string winner = "No one... its a tie";
			if(p1score > p2score)
				winner = "PLAYER 1";
			else if(p1score < p2score)
				winner = "PLAYER 2";

			GameObject.Find("Winner").GetComponent<Text>().text = "The winner is..... " + winner;
		}
	}

	public void GenerateGraph()
	{
		menu.SetActive (false);
		scorePanel.SetActive (true);
	}

	public static void InitializeScore()
	{
		GameObject.Find("Player1Score").GetComponent<Text>().text = "P1 Score: " + p1score;
		GameObject.Find("Player2Score").GetComponent<Text>().text = "P2 Score: " + p2score;
	}

	public void IncreaseScore(bool p1)
	{
		if (p1) {
			p1score++;
			GameObject.Find("Player1Score").GetComponent<Text>().text = "P1 Score: " + p1score;

		} else {
			p2score++;
			GameObject.Find("Player2Score").GetComponent<Text>().text = "P2 Score: " + p2score;
		}
		networkView.RPC ("SyncScores", RPCMode.Others, p1score, p2score);
	}

	[RPC]
	void SyncScores(int score1, int score2)
	{
		GameObject.Find("Player1Score").GetComponent<Text>().text = "P1 Score: " + score1;
		GameObject.Find("Player2Score").GetComponent<Text>().text = "P2 Score: " + score2;
		p1score = score1;
		p2score = score2;
	}

	void AddSpecialNode(Vector3 node)
	{
		Collider[] cols = Physics.OverlapSphere (node, 0.1f);

		for (int i = 0; i < cols.Length; i++) {
			if(cols[i].gameObject.name == "Cube"){
				cols[i].gameObject.transform.renderer.material.color = Color.yellow;
				cols[i].gameObject.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
			}
		}
	}
}

