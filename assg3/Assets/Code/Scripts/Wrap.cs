using UnityEngine;
using System.Collections;

public class Wrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag ("Player")) {
			col.gameObject.transform.position = Vector3.Scale (col.gameObject.transform.position, new Vector3(-0.98f,1,1));
		}
	}
}
