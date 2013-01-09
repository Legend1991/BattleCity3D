using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class TankViewControl : MonoBehaviour {
	
	public float TurretTurnSpeed = 4f;
	public float TankTurnSpeed = 1f;
	
	Vector3 TankMoveSpeed = new Vector3(0, 0, 0.3f);
	
	enum Rotation {
		Forward,
		Right,
		Back,
		Left
	}
	
	Rotation currRot = Rotation.Forward;
	GameObject fPTank;
	float moveX = 0f;
	float angle = 0f;

	void FixedUpdate()
	{
		if (Input.GetAxis("Mouse X") != 0)
		{
			RotateTurret();
		}
		if (Input.GetKey(KeyCode.W))
		{
			MoveTank();
		}
		else if (Input.GetKey(KeyCode.A))
		{
			RotateTank(-TankTurnSpeed);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			RotateTank(TankTurnSpeed);
		}
		
	}
	
	void RotateTank(float turnSpeed)
	{
		angle = fPTank.transform.rotation.y + turnSpeed;
		angle = Mathf.Round(Mathf.Clamp(angle, -360, 360));
		fPTank.transform.Rotate(0, angle, 0);
	}
	
	void RotateTurret()
	{
		moveX = Input.GetAxis("Mouse X") * TurretTurnSpeed;
		//GameObject.Find("GUIText").guiText.text = string.Format("MoveX {0}", moveX);
		moveX = Mathf.Round(Mathf.Clamp(moveX, -360, 360));
		GameObject.FindWithTag("PlayerTurret").transform.Rotate(0, moveX, 0);
	}

	void MoveTank()
	{
		Quaternion qua = Quaternion.AngleAxis(fPTank.transform.eulerAngles.y, Vector3.up);
		fPTank.transform.position += qua * TankMoveSpeed;
	}
	
	void Start ()
	{
		if (rigidbody)
			rigidbody.freezeRotation = true;
		fPTank = GameObject.Find("FPTank");
		Screen.showCursor = false;
	}
}