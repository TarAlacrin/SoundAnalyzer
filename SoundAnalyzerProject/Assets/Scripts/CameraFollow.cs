using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform target;
	public Vector2 minMaxXRot = new Vector2(15,30f);
	Transform Xrotkid;


		// Use this for initialization
	void Start () 
	{
		Xrotkid = transform.GetChild(0);
	}

	// Update is called once per frame
	void Update () 
	{
		transform.position = target.transform.position;

		float mX = Input.GetAxis("Mouse X");
		
		transform.Rotate(0,mX*2,0);

		
		float mY = Input.mousePosition.y;
		mY /= (float)Screen.currentResolution.height;




		float rotX = Mathf.Lerp(minMaxXRot.x, minMaxXRot.y, mY);

		Quaternion quatro = Quaternion.AngleAxis(rotX, Vector3.right);

		Xrotkid.localRotation = Quaternion.RotateTowards(Xrotkid.localRotation, quatro,100f);



	}
}
