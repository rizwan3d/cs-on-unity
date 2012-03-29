using UnityEngine;
using System.Collections;

public class GrenadeController : MonoBehaviour {
	public GameObject explosion;
	
	public float MAX_WAITING_TIME = 1.5f;
	public float currentTime = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if(currentTime >= MAX_WAITING_TIME)
		{
			GameObject explosionInst = (GameObject)Instantiate(explosion, transform.position, transform.root.rotation);
			Destroy(gameObject);
		}
	}
}
