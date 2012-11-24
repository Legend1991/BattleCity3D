using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {
	
	public float sensitivityX = 15f;
	
	enum Rotation {
		Forward,
		Right,
		Back,
		Left
	}
	
	Rotation currRot = Rotation.Forward;
	GameObject fPTank;
	float moveX = 0f;

	void Update ()
	{
		if(!fPTank.animation.isPlaying)
		{
			moveX += Input.GetAxis("Mouse X") * sensitivityX;
			GameObject.Find("GUIText").guiText.text = string.Format("MoveX {0}", moveX);
			if(moveX > 20)
			{
				switch(currRot)
				{
				case Rotation.Forward:
					RotateTurr("Right0", moveX, Rotation.Right);
					break;
				case Rotation.Right:
					RotateTurr("Right90", moveX, Rotation.Back);
					break;
				case Rotation.Back:
					RotateTurr("Right180", moveX, Rotation.Left);
					break;
				case Rotation.Left:
					RotateTurr("Right270", moveX, Rotation.Forward);
					break;
				}
			}
			if(moveX < -20)
			{
				switch(currRot)
				{
				case Rotation.Forward:
					RotateTurr("Left0", moveX, Rotation.Left);
					break;
				case Rotation.Left:
					RotateTurr("Left90", moveX, Rotation.Back);
					break;
				case Rotation.Back:
					RotateTurr("Left180", moveX, Rotation.Right);
					break;
				case Rotation.Right:
					RotateTurr("Left270", moveX, Rotation.Forward);
					break;
				}
			}
		}
	}
	
	void RotateTurr(string animName, float newX, Rotation nextRot)
	{
		GameObject fPTank = GameObject.Find("FPTank");
		fPTank.animation.Play(animName);
		currRot = nextRot;
		moveX = 0f;
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
		fPTank = GameObject.Find("FPTank");
	}
}