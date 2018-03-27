using UnityEngine;
using System.Collections;

public class PhysicsWarp : MonoBehaviour 
{
	Rigidbody rigid;

	//public Vector2 min
	// Use this for initialization
	void Start () 
	{
		rigid = this.gameObject.GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

	}
}
