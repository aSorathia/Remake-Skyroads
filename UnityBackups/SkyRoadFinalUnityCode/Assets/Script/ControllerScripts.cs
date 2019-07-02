using System;
using UnityEngine;
[Serializable]
public class ControllerScripts : MonoBehaviour
{	
	public bool isGrounded;
	public float speed;
	public float maxSpeed;
	private float lastZ;
	public ControllerScripts()
	{
		this.speed = (float)0;
		this.maxSpeed = (float)0;
		this.lastZ = (float)0;
	}
	public void Update(){
		Debug.DrawRay (transform.position, -Vector3.up);
		if(Physics.Raycast (this.transform.position, -Vector3.up, 0.5f)){
			isGrounded=true;
		}
		else{
			isGrounded=false;
		}
	}
	public void FixedUpdate()
	{
		if (Input.GetAxis("Vertical") != 0f)
		{
			this.speed += Input.GetAxis("Vertical") * 0.3f * Time.deltaTime;
			float z = this.constantForce.force.z + this.speed;
			Vector3 force = this.constantForce.force;
			float num = force.z = z;
			Vector3 vector;
			this.constantForce.force=(vector = force);
		}
		this.speed = (float)0;
		if (this.rigidbody.velocity.z > this.maxSpeed)
		{
			float z2 = this.maxSpeed;
			Vector3 velocity = this.rigidbody.velocity;
			float num2 = velocity.z = z2;
			Vector3 vector2;
			this.rigidbody.velocity=(vector2 = velocity);
		}
		else
		{
			if (this.rigidbody.velocity.z < (float)0)
			{
				int num3 = 0;
				Vector3 velocity2 = this.rigidbody.velocity;
				float num4 = velocity2.z = (float)num3;
				Vector3 vector3;
				this.rigidbody.velocity=(vector3 = velocity2);
			}
		}
		if (Physics.Raycast (this.transform.position, -Vector3.up, 0.5f)) {
		}
		if (Input.GetButton("Jump") && Physics.Raycast(this.transform.position, -Vector3.up, 0.5f))
		{
			this.rigidbody.AddForce(Vector3.up * 0.7f,ForceMode.Impulse);
			Debug.Log ("Am Jumping");
		}
		if (Input.GetAxis("Horizontal") != 0f)
		{
			this.rigidbody.MovePosition(this.rigidbody.position + Vector3.right * (Input.GetAxis("Horizontal") * (this.rigidbody.velocity.z * 0.25f) * Time.deltaTime));
		}
	}
}
