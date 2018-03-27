using UnityEngine;
using System.Collections;

public class TimeVelocityKill : MonoBehaviour 
{
	public float TimeToDie = 2f;
	public float ShrinkStart = 1f;

	private float origScale = 0f;

	private float age = 0f;
	private float birthTime = 0f;
	// Use this for initialization
	void Start () 
	{
		birthTime = Time.time;
		origScale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		age = Time.time - birthTime;
		if(age > TimeToDie)
		{
			Destroy(this.gameObject);
		}

		if(age > ShrinkStart)
		{
			transform.localScale = Vector3.one * origScale * ((TimeToDie - age)/(TimeToDie - ShrinkStart));
		}
	}
}
