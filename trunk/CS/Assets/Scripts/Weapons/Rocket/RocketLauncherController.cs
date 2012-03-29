using UnityEngine;
using System.Collections;

public class RocketLauncherController : MonoBehaviour 
{
	public Rigidbody projectile;
	public float speed = 50;
	public float reloadTime = 0.5f;
	public int ammoCount = 20;
	float lastShot = -10.0f;
	
	//Use this for initialization
	void Start () 
	{
		if(audio)
			audio.loop = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButton("Fire1"))
		{
			Rigidbody inst = (Rigidbody)Instantiate(projectile, transform.position, transform.rotation);
			inst.velocity = (Vector3)transform.TransformDirection(new Vector3(0, 0, speed));
			Physics.IgnoreCollision(inst.collider, transform.root.collider);
			
			if(audio)
			{
				for(int i=0; i<3; i++)
					audio.Play();
			}
		}else if(audio)
		{
			audio.loop = false;
		}
	}
} 