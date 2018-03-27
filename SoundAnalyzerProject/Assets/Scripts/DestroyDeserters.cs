using UnityEngine;
using System.Collections;

//destroys cubes/objects that have gone out of bounds. Resets the player if they do
public class DestroyDeserters : MonoBehaviour 
{
	PopulateCubes popCubs;
	void Start()
	{
		popCubs = this.gameObject.GetComponent<PopulateCubes>();
	}
	void OnTriggerExit(Collider col)
	{
		if(col.tag == "BouncyCube")
			popCubs.DestroyCube(col.gameObject);//has to remove cube from the list before destroying it
		else if(col.tag != "Player")
			Destroy(col.gameObject);
		else
			col.transform.position = this.transform.position - Vector3.down*2f;
	}
}
