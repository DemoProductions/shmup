using UnityEngine;

public class BoundingsTimeout2D : MonoBehaviour
{

    public int timeout;
    public float time;

    void Start()
    {
    }

	void LateUpdate ()
    {
		if (time > timeout)
			Destroy (this.gameObject);

        // TODO: same checks in Boundings2D.cs. Can modularize
		var left = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
		var right = Camera.main.ViewportToWorldPoint(Vector3.one).x;
		var top = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
		var bottom = Camera.main.ViewportToWorldPoint(Vector3.one).y;

		bool xout = false;
		bool yout = false;

		float x = transform.position.x;
		float y = transform.position.y;

		// check x
		if (transform.position.x <= left - GetComponentInChildren<Renderer>().bounds.size.x) {
			xout = true;
		} else if (transform.position.x >= right + GetComponentInChildren<Renderer>().bounds.size.x) {
			xout = true;
		}

		// check y
		if (transform.position.y <= top - GetComponentInChildren<Renderer>().bounds.size.y) {
			yout = true;
		} else if (transform.position.y >= bottom + GetComponentInChildren<Renderer>().bounds.size.y) {
			yout = true;
		}

        // transform.position = new Vector3(x, y, transform.position.z);
		if (xout || yout)
			time += Time.deltaTime;
		else
			time = 0f;
	}
}
