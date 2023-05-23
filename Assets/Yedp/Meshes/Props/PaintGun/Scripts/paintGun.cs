using UnityEngine;
using System.Collections;


public class paintGun : MonoBehaviour {

	public GameObject triggerObj;
	public GameObject sprayPart;

	void Start () 
	{

		sprayPart.active = true;

	}
	

	void Update () 
	{
		//this is what caused the gun to just slowly rotate for no reason
		//transform.Rotate(Vector3.up * Time.deltaTime*10f, Space.World);

		if (Input.GetMouseButtonDown (0)) 
		{
			triggerObj.transform.Rotate(0f, 0f, -8.5f);
			sprayPart.GetComponent<ParticleSystem>().Play();

		}

		if (Input.GetMouseButtonUp (0)) 
		{
			triggerObj.transform.Rotate(0f, 0f, 8.5f);
			sprayPart.GetComponent<ParticleSystem>().Stop();

		}
	}
}
