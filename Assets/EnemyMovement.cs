using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

	public int speed = 100;

//	[Header("X Variables")]
	public float xVelocity = -1;
	public bool xSin = false;
	public float xSinVelocity = 0;
	public float xSinRate = 1;
	public float xSinOffset = 0;
	public bool xCos = false;
	public float xCosVelocity = 0;
	public float xCosRate = 1;
	public float xCosOffset = 0;

//	[Header("Y Variables")]
	public float yVelocity;
	public bool ySin = false;
	public float ySinVelocity = 0;
	public float ySinRate = 1;
	public float ySinOffset = 0;
	public bool yCos = false;
	public float yCosVelocity = 0;
	public float yCosRate = 1;
	public float yCosOffset = 0;

	Rigidbody2D rbody;

	// Use this for initialization
	void Start ()
	{
		rbody = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float x = xVelocity;
		float y = yVelocity;

		if (xSin)
		{
			x += Mathf.Sin ((Time.time + xSinOffset) * xSinRate) * xSinVelocity;
		}
		else if (xCos)
		{
			x += Mathf.Cos ((Time.time + xCosOffset) * xCosRate) * xCosVelocity;
		}
		if (ySin)
		{
			y += Mathf.Sin ((Time.time + ySinOffset) * ySinRate) * ySinVelocity;
		}
		else if (yCos)
		{
			y += Mathf.Cos ((Time.time + yCosOffset) * yCosRate) * yCosVelocity;
		}

		rbody.velocity = new Vector2 (x, y) * Time.deltaTime * speed;
	}
}
