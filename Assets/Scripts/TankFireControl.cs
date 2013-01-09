using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class TankFireControl : MonoBehaviour {
	
	public AudioClip FireSound;
	public GameObject FireObj;
	public GameObject loseFireObj;
	
	GameObject fPTank;
	GameObject gun;

	// Use this for initialization
	void Start () {
		fPTank = GameObject.Find("FPTank");
		gun = GameObject.Find("gun");
	}
	
	// Update is called once per frame
	void Update () {
		if(!fPTank.animation.isPlaying)
		{
			if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space))
			{
				fPTank.animation.Play("GunFire");
				GameObject fire = (GameObject)Instantiate(FireObj);
				fire.transform.parent = gun.transform;
				fire.transform.localRotation = gun.transform.localRotation;
				fire.transform.localPosition = new Vector3(gun.transform.localPosition.x ,
					gun.transform.localPosition.y , gun.transform.localPosition.z + 3);
				fire.BroadcastMessage("Explode");
				audio.PlayOneShot(FireSound);
				
				RaycastHit hit = new RaycastHit();
				
				if (Physics.Raycast(transform.position,
					transform.forward, out hit))
				{
					if (hit.collider.gameObject.tag == "BrickBlock" ||
						hit.collider.gameObject.tag == "SteelBlock")
					{
						GameObject brick = hit.collider.gameObject;
						brick.BroadcastMessage("Explode");
					}
					else if (hit.collider.gameObject.tag == "Enemy")
					{
						GameObject brick = hit.collider.gameObject;
						brick.BroadcastMessage("Lose");
					}
				}
			}
		}
	}
	
		public void Lose()
	{
		loseFireObj.transform.position = this.transform.position;
		GameObject fire = (GameObject)Instantiate(loseFireObj);
		fire.BroadcastMessage("Explode");
		audio.PlayOneShot(FireSound);
		Destroy(gameObject, 0.3f);
	}
}
