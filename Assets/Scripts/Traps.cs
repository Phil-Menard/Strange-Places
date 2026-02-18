using System;
using UnityEngine;

public class Traps : MonoBehaviour
{	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(collision.gameObject);
	}
}
