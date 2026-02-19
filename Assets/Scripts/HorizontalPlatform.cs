using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HorizontalPlatform : MonoBehaviour
{
    [SerializeField] private Transform waypointLeft;
    [SerializeField] private Transform waypointRight;
    [SerializeField] private float speed = 2.0f;
	[SerializeField] private bool isGoingRight = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGoingRight)
        {
			transform.position = Vector3.MoveTowards(transform.position, waypointRight.position, speed * Time.deltaTime);
            if (transform.position.x == waypointRight.position.x)
				isGoingRight = false;
		}
        else
        {
			transform.position = Vector3.MoveTowards(transform.position, waypointLeft.position, speed * Time.deltaTime);
			if (transform.position.x == waypointLeft.position.x)
				isGoingRight = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			other.transform.SetParent(this.transform);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			other.transform.SetParent(null);
		}
	}
}
