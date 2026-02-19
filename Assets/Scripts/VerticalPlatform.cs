using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class VerticalPlatform : MonoBehaviour
{
    [SerializeField] private Transform waypointTop;
    [SerializeField] private Transform waypointBottom;
    [SerializeField] private float speed = 2.0f;
	[SerializeField] private bool isGoingTop = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGoingTop)
        {
			transform.position = Vector3.MoveTowards(transform.position, waypointTop.position, speed * Time.deltaTime);
            if (transform.position.y == waypointTop.position.y)
			{
				StartCoroutine(WaitBeforeMoving(false));
			}
		}
        else
        {
			transform.position = Vector3.MoveTowards(transform.position, waypointBottom.position, speed * Time.deltaTime);
			if (transform.position.y == waypointBottom.position.y)
			{
				StartCoroutine(WaitBeforeMoving(true));
			}
		}
	}

	private IEnumerator WaitBeforeMoving(bool state)
	{
		yield return new WaitForSeconds(1);
		isGoingTop = state;
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
