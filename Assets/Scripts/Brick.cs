using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Brick : MonoBehaviour {
	
	public GameObject FireObj;
	public AudioClip FireSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Explode()
	{
		FireObj.transform.position = this.transform.position;
		GameObject fire = (GameObject)Instantiate(FireObj);
		fire.BroadcastMessage("Explode");
		audio.PlayOneShot(FireSound);
		Destroy(gameObject, 0.3f);
	}
}
