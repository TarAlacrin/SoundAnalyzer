using UnityEngine;
using System.Collections;

public class RollMove : MonoBehaviour {

	Camera cam;
	Rigidbody rigid;
	public float torque = 100;
	public float jump = 300;
	public float maxJumpVelocity = 5;
	// Use this for initialization
	void Start () 
	{
		rigid = this.gameObject.GetComponent<Rigidbody>();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Vector3 forceDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		Vector3 forceDir = -Input.GetAxis("Vertical")*Vector3.Scale(cam.transform.forward , new Vector3(1,0,1)).normalized;
		forceDir += -Input.GetAxis("Horizontal")*Vector3.Scale(cam.transform.right, new Vector3(1,0,1)).normalized;

		if(forceDir != Vector3.zero)
		{
			//rigid.AddForceAtPosition(torque*Vector3.up, transform.position + -1f*forcePos);
			rigid.AddForceAtPosition(torque*-1f*forceDir,Vector3.up + transform.position);
//			Debug.Log(forceDir);
		}

		float twirl = Input.GetAxis("Twirl");

		if(twirl != 0)
		{
			rigid.AddForceAtPosition(torque*1.5f*cam.transform.forward,cam.transform.right*twirl + transform.position);
		}



		if(Input.GetButtonDown("Jump"))
		{
			rigid.AddForce(jump*Vector3.up * Mathf.Min(maxJumpVelocity - Mathf.Min(rigid.velocity.y, maxJumpVelocity), maxJumpVelocity));
		}
		else if(Input.GetKey(KeyCode.LeftShift))
		{
			rigid.AddForce(jump*Vector3.down);
		}




	}
}
