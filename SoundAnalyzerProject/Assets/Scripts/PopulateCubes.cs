using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulateCubes : MonoBehaviour 
{
	public int queue = 0;//number waiting to spawn
	public int queueWait = 1;//time to wait between spawning next cube
	public int maxNumber = 100; //max number of active cubes allowed
	private int queuePos = 0;

	List<GameObject> spawnedCubes = new List<GameObject>();//keeps track of all spawned cubes



	public Vector2 sizeRange = new Vector2(0.3f,3f); //range of scales that the cubes will spawn between and the midpoint to lerp from
	public float midSizeValue = 1f;

	public Vector2 massRange = new Vector2(0.1f,2f);//range of masses that the cubes will spawn between and the midpoint to lerp from
	public float midMassValue = 0.7f;


	public Material mat;//materials to apply to the cube
	public PhysicMaterial cubePhysMat;//materials to apply to the cube


	void MakeCube()
	{

		float size = 2*Random.value - 1;
		float masse = size;//2*Random.value - 1;

		if(size > 0)
		{
			size = size*(sizeRange.y - midSizeValue) + midSizeValue;
		}
		else
		{
			size = (-size)*(midSizeValue - sizeRange.x) + sizeRange.x;
		}

		if(masse > 0)
		{
			masse = masse*(massRange.y - midMassValue) + midMassValue;
		}
		else
		{
			masse = (-masse)*(midMassValue - massRange.x) + massRange.x;
		}

		GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		newCube.AddComponent<Rigidbody>();
		newCube.transform.position = this.transform.position;
		newCube.transform.localScale *= size;
		newCube.GetComponent<Rigidbody>().mass = masse;
		newCube.GetComponent<Renderer>().material = mat;
		newCube.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
		newCube.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(Random.value,Random.value, Random.value) - Vector3.one*0.5f, transform.position + new Vector3(Random.value,Random.value, Random.value) - Vector3.one*0.5f);
		newCube.GetComponent<BoxCollider>().material = this.cubePhysMat;
		newCube.tag = "BouncyCube";
		spawnedCubes.Add(newCube);
		if(spawnedCubes.Count >maxNumber)
		{
			DestroyCube(spawnedCubes[0]);
		}
	}



	//removes the object from the list before destroying it.
	public void DestroyCube(GameObject parCube)
	{
		spawnedCubes.Remove(parCube);
		Destroy(parCube);
	}

	// Update is called once per frame
	void Update () 
	{
		if(queue > 0)
		{
			queuePos += 1;
			
			if(queuePos >= queueWait)
			{
				queuePos = 0;
				queue -= 1;

				MakeCube();
			}
		}
		else
		{
			queue = 0;
		}
	}
}
