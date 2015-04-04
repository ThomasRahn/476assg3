using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCMovement : MonoBehaviour {
	
	public Node currentPosition;
	public IList<Node> path = new List<Node>();
	public Node nextNode;
	public Vector3 next;
	public int pathCount = 0;
	
	public float MaxAcceleration = 3.0f;
	public Vector3 Velocity = Vector3.zero;
	public float MaxVelocity = 4.0f;
	public float rotateSpeed = 100.0f;

	public int current_node = 0;

	public IList<Node> open_list = new List<Node> ();
	public IList<Node> closed_list = new List<Node> ();
	public Node start;

	public GameObject targetPlayer;
	public Vector3 targetPosition = Vector3.zero;

	void Start () {}

	public void setTargetPlayer(GameObject player)
	{
		start = GameController.graph.FindClosestNode (this.transform.position);
		targetPlayer = player;
		targetPosition = GameController.graph.FindClosestNode (targetPlayer.transform.position).position;
		dijkstra ();
	}

	void Update()
	{
		if(GameController.playersConnected && false)
		{
			Node target = getTarget();
			if (target != null){// && Vector3.Distance(targetPosition, this.transform.position) > 0.2f) {
				this.transform.position = Vector3.MoveTowards (this.transform.position, target.position, 3.0f * Time.deltaTime);
			} else {
				if(targetPlayer != null && Vector3.Distance(targetPlayer.transform.position, this.transform.position) > 0.2f)
				{
					start = GameController.graph.FindClosestNode (this.transform.position);
					targetPosition = GameController.graph.FindClosestNode (targetPlayer.transform.position).position;
					dijkstra ();
				}
			}
		}
		
	}

	private Node getTarget(){

		Node target = null;
		if (path.Count != 0 ) 
		{
			current_node = path.Count - 1;
			if(current_node < 0)
				return null;

			target = path[current_node];
			Vector3 temp = target.position;
			if(Vector3.Distance(temp, this.transform.position) <= 0.2f)
			{
				current_node --;
				path.RemoveAt(current_node+1);
			}
		}
		return target;
	}

	//Performs A* algorithm using euclidean distance to find the shortest path.
	void euclidean()
	{
		open_list.Add (start);
		start.prevNode = null;
		start.heuristic = start.cost;
		Node current_node = null;
		while (open_list.Count != 0) 
		{
			current_node = GetLowerCost();
			if(current_node == null)
				break;
			
			open_list.Remove(current_node);
			
			if(current_node.position == targetPosition)
			{
				current_node.prevNode = closed_list[closed_list.Count - 1];
				break;
			}else{
				foreach(Node node in current_node.getAllConnectingNodes())
				{
					
					if(!open_list.Contains(node) && !closed_list.Contains(node))
					{
						node.prevNode = current_node;
						node.heuristic = node.cost + current_node.heuristic;
						open_list.Add(node);
					}
					else
					{
						if(node.heuristic > (node.cost + current_node.heuristic))
						{
							node.heuristic = node.cost + current_node.heuristic;
						}
					}
				}
				closed_list.Add(current_node);
			}
		}

		int counter = 0;
		while (current_node.prevNode != null && counter < 200) 
		{
			counter++;
			path.Add(current_node);
			current_node = current_node.prevNode;
		}
	}

	//Performs Dijkstra to find the shortest path. Null heuristic (no priority)
	void dijkstra()
	{
		open_list.Clear ();
		closed_list.Clear ();
		open_list.Add (start);
		while (open_list.Count > 0 && open_list[0].position != targetPosition) {
			Node node = open_list [0];
			Node[] neighborList = open_list [0].getAllConnectingNodes ();
			for (int i = 0; i < neighborList.Length; i++) {
				if (!closed_list.Contains (neighborList [i]) && !open_list.Contains (neighborList [i])) {
					neighborList [i].prevNode = node;
					open_list.Add (neighborList [i]);
				}
			}
			closed_list.Add (node);
			open_list.Remove (node);
		}
		if (open_list.Count > 0)
		{
			path.Add (open_list [0]);
			while (true) {
					
				if (path [path.Count - 1].prevNode.position == start.position) {
					path.Add (path [path.Count - 1].prevNode);
					break;
				} else {
					path.Add (path [path.Count - 1].prevNode);
				}
			}
		}
	}

	
	//Gets the best possible node with the lowest heuristic from the open list
	private Node GetLowerCost()
	{
		if (open_list.Count == 0) {
			return null;
		}
	
		return  open_list [0];
	}

	void OnTriggerEnter(Collider col){
		Debug.Log (col.gameObject.name);
		if(col.gameObject.CompareTag("Player")){
			start = GameController.graph.FindClosestNode (this.transform.position);
			targetPosition = Vector3.zero;
			dijkstra ();

			if(col.gameObject.GetComponent<PlayerMovement>().isPlayerOne)
			{
				col.gameObject.transform.position = GameObject.Find("CreatorSpawn").transform.position;
			}else{
				col.gameObject.transform.position = GameObject.Find("JoinSpawn").transform.position;
			}
		}
	}
}