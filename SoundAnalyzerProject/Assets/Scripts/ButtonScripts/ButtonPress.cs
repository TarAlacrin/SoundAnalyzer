using UnityEngine;
using System.Collections;

public abstract class ButtonPress : MonoBehaviour 
{
	public bool isDepressed = false; //is the Switch Down Currently
	Vector3 defaultScale;
	public float speed = 0.3f; //how fast will it shift

	[HideInInspector]
	public float timeTillReset = 0f;//time after a button has been pressed that it is able to be pressed again
	
	// Use this for initialization
	public virtual void Start () {
		defaultScale = this.transform.localScale;
		//defaultPos = this.transform.position;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		if(isDepressed)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.Scale(defaultScale, new Vector3(1,0.5f,1)), speed);
		}
		else
		{
			transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, speed);
		}

		timeTillReset -= Time.deltaTime;
	}


	void OnCollisionEnter(Collision col)
	{
		if(col.collider.tag == "Player" && timeTillReset < 0)
		{
			OnPress();
		}
	}

	void OnCollisionExit(Collision col)
	{
		if(col.collider.tag == "Player")
		{
			OnLeave();
		}
	}


	public virtual void OnPress()
	{
		timeTillReset = 3f;
	}

	public virtual void OnLeave()
	{
	}
}
