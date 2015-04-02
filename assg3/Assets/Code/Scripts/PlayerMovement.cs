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
				Vector3 targetDir = next.position - this.transform.position;
				transform.rotation = Quaternion.LookRotation(targetDir);
			} else {
				this.transform.position = Vector3.MoveTowards (this.transform.position, currentNode.position, 4.0f * Time.deltaTime);
				Vector3 targetDir = currentNode.position - this.transform.position;
				transform.rotation = Quaternion.LookRotation(targetDir);
			}
			
		}
		
	}

	private void KeyInput()
	{
		if (Input.GetKeyDown (KeyCode.W) && canMove(transform.forward)) {
			direction = MovementDirection.up;
		}
		if (Input.GetKeyDown (KeyCode.A) && canMove(-transform.right)) {
			direction = MovementDirection.left;
		}
		if (Input.GetKeyDown (KeyCode.D) && canMove(transform.right)) {
			direction = MovementDirection.right;
		}
		if (Input.GetKeyDown (KeyCode.S) && canMove(-transform.forward)) {
			direction = MovementDirection.down;
		}
	}

	private bool canMove(Vector3 dir)
	{
		return true;
		//return Physics.Linecast (transform.position, dir, LayerMask.GetMask ("Wall")); 
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
