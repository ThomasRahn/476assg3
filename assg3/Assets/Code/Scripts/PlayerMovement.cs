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
	public float speed = 4.0f;
	// Use this for initialization
	void Start () {
		direction = MovementDirection.left;
	}
	
	// Update is called once per frame
	void Update () {
		if (hasControl && GameController.playersConnected) {
			currentNode = GameController.graph.FindClosestNode (this.transform.position);
			KeyInput ();
			Node next = currentNode.hasDirectionNode (direction);
			if (next != null) {
				this.transform.position = Vector3.MoveTowards (this.transform.position, next.position, speed * Time.deltaTime);
				//this.rigidbody.velocity = (next.position - this.transform.position).normalized * 4.0f;
			} else {
				this.transform.position = Vector3.MoveTowards (this.transform.position, currentNode.position, speed * Time.deltaTime);
				//this.rigidbody.velocity = (currentNode.position - this.transform.position).normalized * 4.0f;

			}
			
		}
		
	}

	private void KeyInput()
	{
		if (Input.GetKeyDown (KeyCode.W) && canMove(MovementDirection.up)) {
			direction = MovementDirection.up;
			transform.rotation = Quaternion.LookRotation(Vector3.forward);
		}
		if (Input.GetKeyDown (KeyCode.A) && canMove(MovementDirection.left)) {
			direction = MovementDirection.left;
			transform.rotation = Quaternion.LookRotation(-Vector3.right);
		}
		if (Input.GetKeyDown (KeyCode.D) && canMove(MovementDirection.right)) {
			direction = MovementDirection.right;
			transform.rotation = Quaternion.LookRotation(Vector3.right);
		}
		if (Input.GetKeyDown (KeyCode.S) && canMove(MovementDirection.down)) {
			transform.rotation = Quaternion.LookRotation(-Vector3.forward);
			direction = MovementDirection.down;
		}
	}

	private bool canMove(MovementDirection dir)
	{
		return currentNode.hasDirectionNode (dir) != null;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag ("cube")) {
			if(col.gameObject.transform.renderer.material.color == Color.yellow)
			{
				speed = 8.0f;
				StartCoroutine(ResetSpeed());
			}
			Destroy(col.gameObject);
			GameController.IncreaseScore(isPlayerOne);
			this.GetComponent<AudioSource>().Play();
		}
	}

	IEnumerator ResetSpeed()
	{
		yield return new WaitForSeconds (5.0f);
		speed = 4.0f;
	}
}
