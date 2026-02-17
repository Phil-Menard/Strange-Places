using System;
using UnityEngine;

public class Traps : MonoBehaviour
{	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("collision normal : " + collision.gameObject);
		Destroy(collision.gameObject);
	}
}
