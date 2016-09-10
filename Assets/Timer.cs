using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{

	public float time;
	public bool running;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (running)
		{
			time += Time.deltaTime;
		}
	}

	public void Begin ()
	{
		running = true;
	}

	public void Stop ()
	{
		running = false;
	}

	public void Reset ()
	{
		time = 0;
	}
}
