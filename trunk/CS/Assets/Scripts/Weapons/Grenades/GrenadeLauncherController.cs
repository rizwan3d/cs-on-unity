using UnityEngine;
using System.Collections;

public class GrenadeLauncherController : MonoBehaviour {

	public Rigidbody projectile;
	public float speed = 50;
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
		{
			Rigidbody inst = (Rigidbody)Instantiate(projectile, transform.position, transform.rotation);
			inst.velocity = (Vector3)transform.TransformDirection(new Vector3(0, 0, speed));
			Physics.IgnoreCollision(inst.collider, transform.root.collider);
		}
	}
}
