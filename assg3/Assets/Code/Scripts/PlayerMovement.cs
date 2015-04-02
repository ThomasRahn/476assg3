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
	public bool isPlayerOne = false;
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
		if (Input.GetKeyDown (KeyCode.W) && canMove(Vector3.forward)) {
			direction = MovementDirection.up;
			transform.rotation = Quaternion.LookRotation(Vector3.forward);
		}
		if (Input.GetKeyDown (KeyCode.A) && canMove(-Vector3.right)) {
			direction = MovementDirection.left;
			transform.rotation = Quaternion.LookRotation(-Vector3.right);
		}
		if (Input.GetKeyDown (KeyCode.D) && canMove(Vector3.right)) {
			direction = MovementDirection.right;
			transform.rotation = Quaternion.LookRotation(Vector3.right);
		}
		if (Input.GetKeyDown (KeyCode.S) && canMove(-Vector3.forward)) {
			transform.rotation = Quaternion.LookRotation(-Vector3.forward);
			direction = MovementDirection.down;
		}
	}

	private bool canMove(Vector3 dir)
	{
		return true;
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag ("cube")) {
			Destroy(col.gameObject);
			GameController.IncreaseScore(isPlayerOne);
			this.GetComponent<AudioSource>().Play();
		}
	}
}
