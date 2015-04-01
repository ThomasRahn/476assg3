using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	public enum MovementDirection {
		up,
		left,
		down,
		right
	}
	public MovementDirection direction;
	public Node currentNode;
	public bool hasControl = false;
	// Use this for initialization
	void Start () {
		direction = MovementDirection.left;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasControl) {
			currentNode = GameController.graph.FindClosestNode (this.transform.position);
			KeyInput ();
			Node next = currentNode.hasDirectionNode (direction);
			if (next != null) {
				this.transform.position = Vector3.MoveTowards (this.transform.position, next.position, 4.0f * Time.deltaTime);
			} else {
				this.transform.position = Vector3.MoveTowards (this.transform.position, currentNode.position, 4.0f * Time.deltaTime);
			}
		}

	}

	private void KeyInput()
	{
		if (Input.GetKeyDown (KeyCode.W)) {
			direction = MovementDirection.up;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			direction = MovementDirection.left;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			direction = MovementDirection.right;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			direction = MovementDirection.down;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("HIT");
		if (col.gameObject.CompareTag ("cube")) {
			Destroy(col.gameObject);
			GameController.IncreaseScore();
			this.GetComponent<AudioSource>().Play();
		}
	}
}
