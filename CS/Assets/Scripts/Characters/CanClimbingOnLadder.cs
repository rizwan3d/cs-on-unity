using UnityEngine;
using System.Collections;

public class CanClimbingOnLadder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision collison)
	{
		foreach( ContactPoint cp in collison.contacts)
		{
			if(cp.otherCollider.transform.tag == "Ladder")
			{
				this.rigidbody.useGravity = false;
				Debug.Log("is climbing");
			}
		}
	}
	
	void OnCollisionExit(Collision collison)
	{
		foreach( ContactPoint cp in collison.contacts)
		{
			if(cp.otherCollider.transform.tag == "Ladder")
			{
				this.rigidbody.useGravity = true;
				Debug.Log("not climbing");
			}
		}
	}
}
