using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class FPSdisplay : MonoBehaviour {
	
	private float updatePeriod = 0.5f;
	private float nextUpdate = 0f;
	private float frames = 0f;
	private float fps = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		frames++;
   
   		if(Time.time > nextUpdate)
		{
        	fps = Mathf.Round(frames / updatePeriod);
            guiText.text = "FPS: " + fps;
            nextUpdate = Time.time + updatePeriod;
            frames = 0;      
   		}
	}
}
