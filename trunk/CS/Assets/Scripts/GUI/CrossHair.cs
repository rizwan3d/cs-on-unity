using UnityEngine;
using System.Collections;

public class CrossHair : MonoBehaviour {
	public Rect position;
	public Texture2D crossHairTexture;
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		position = new Rect((Screen.width - crossHairTexture.width)/2, 
						(Screen.height - crossHairTexture.height)/2, 
						crossHairTexture.width, crossHairTexture.height);
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUI.DrawTexture(position, crossHairTexture);
	}
}
