using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Bot : MonoBehaviour {
	
	public AudioClip FireSound;
	public GameObject FireObj;
	public GameObject loseFireObj;
	
	Vector3[] directions = { new Vector3(0, 0, 0), new Vector3(0, 180, 0), new Vector3(0, -90, 0), new Vector3(0, 90, 0) };
	Vector3 currDir = Vector3.zero;
	float atomMove = 10f;
	float sleepTime = 1f;
	float oldTime;
	bool stop = false;
	GameObject gun;
	Vector3 oldPos;
	RaycastHit hit = new RaycastHit();
	string tag = "";
	System.Random rnd;
	System.IO.StreamWriter sw;
	int index;

	// Use this for initialization
	void Start () {
		//Random.seed = (int)System.DateTime.Now.Ticks;
		rnd = new System.Random((int)System.DateTime.Now.Ticks);
		oldTime = Time.time;
		gun = GameObject.Find("enemyGun");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.time > oldTime + sleepTime)
		{
			bool isPassable;
			Vector3 dir;
			
			do {
				dir = GetRndDirection();
				this.transform.rotation = Quaternion.Euler(dir);
				isPassable = CheckAndFire(dir);
				//GameObject.Find("GUIText").guiText.text = string.Format("isPassable: {0}   object: {1}", isPassable, tag);
				//Debug.Log(string.Format("isPassable: {0}   object: {1}", isPassable, tag));
				using(sw = new System.IO.StreamWriter("D:\\Log.txt", true))
				{
					sw.WriteLine(string.Format("isPassable: {0}\tobject: {1}\tdirection: {2}", isPassable, tag, index));
				}
			}
			while (!isPassable);
			stop = false;
			atomMove = 10f;
			currDir = dir;
			oldTime = Time.time;
		}
		else 
		{
			if (!stop)
			{
				MoveTank();
			}
		}
	}
	/*
	void OnTriggerEnter(Collider other)
	{
		transform.position = oldPos;
	}

	void OnTriggerStay(Collider other)
	{
		transform.position = oldPos;
	}
	*/
	void MoveTank()
	{
		oldPos = transform.position;
		Quaternion qua = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up);
		float distance = Mathf.Round(10 * (Time.time - oldTime) / sleepTime);
		if(atomMove - distance < 0)
		{
			distance = atomMove;
			stop = true;
		}
		else
		{
			atomMove -= distance;
		}
		Vector3 dir = new Vector3(0, 0, distance);
		this.transform.position += qua * dir;
	}
	
	bool CheckAndFire(Vector3 dir)
	{
		/*
		if (Physics.Raycast(this.transform.position,
			transform.forward, out hit, 160))
		{
			if (hit.collider.gameObject.tag == "Player")
			{
				Debug.Log("Lose");
				//GameObject.FindWithTag("EnemyCam").SetActive(true);
				//GameObject.FindWithTag("MainCamera").SetActive(false);
				//this.transform.rotation = Quaternion.Euler(dir);
				Explode();
				GameObject player = hit.collider.gameObject;
				player.BroadcastMessage("Lose");
			}
		}
		 */
		if (Physics.Raycast(transform.position,
			transform.forward, out hit, 10f))
		{
			if (hit.collider.gameObject.tag == "SteelBlock")
			{
				//GameObject.Find("GUIText").guiText.text = string.Format("Steel Block");
				tag = "SteelBlock";
				return false;
			}
			else if (hit.collider.gameObject.tag == "BrickBlock")
			{
				tag = "BrickBlock";
				//this.transform.rotation = Quaternion.Euler(dir);
				Explode();
				GameObject brick = hit.collider.gameObject;
				brick.BroadcastMessage("Explode");
				return false;
			}
		}
		tag = "";
		return true;
	}
	
	void Explode()
	{
		/*
		this.animation.Play("GunFire");
		GameObject fire = (GameObject)Instantiate(FireObj);
		fire.transform.parent = gun.transform;
		fire.transform.localRotation = gun.transform.localRotation;
		fire.transform.localPosition = new Vector3(gun.transform.localPosition.x ,
		gun.transform.localPosition.y , gun.transform.localPosition.z + 3);
		fire.BroadcastMessage("Explode");
		audio.PlayOneShot(FireSound);
		*/
	}
	
	public void Lose()
	{
		loseFireObj.transform.position = this.transform.position;
		GameObject fire = (GameObject)Instantiate(loseFireObj);
		fire.BroadcastMessage("Explode");
		audio.PlayOneShot(FireSound);
		Destroy(gameObject, 0.3f);
	}
	
	Vector3 GetRndDirection()
	{
		index = rnd.Next(0, directions.Length - 1);
		//int index = Random.Range(0, directions.Length - 1);
		return directions[index];
	}
}
