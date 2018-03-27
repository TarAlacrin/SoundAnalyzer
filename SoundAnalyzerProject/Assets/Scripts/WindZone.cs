using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindZone : MonoBehaviour 
{
	public float force;

	public List<Rigidbody> rigids = new List<Rigidbody>();
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		foreach(Rigidbody ronald in rigids)
		{
			ronald.AddForceAtPosition(this.transform.forward*force, this.transform.position);
		}
	}

		void OnTriggerEnter(Collider col)
		{
				Rigidbody rigi = col.gameObject.GetComponent<Rigidbody>();
				if(rigi!=null)
				{
						rigids.Add(rigi);
				}
				
		}

		void OnTriggerExit(Collider col)
		{
			Rigidbody rigi = col.gameObject.GetComponent<Rigidbody>();
			if(rigi!=null)
			{
				rigids.Remove(rigi);
			}
		}
}
