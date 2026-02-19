using UnityEngine;

public class Keys : MonoBehaviour
{
	[SerializeField] private Player player;
	private GameObject parent;
	private bool gotKey = false;

	private void Awake()
	{
		parent = transform.parent.gameObject;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!gotKey)
		{
			player.keys++;
			gotKey = true;
			Destroy(parent);
		}
	}
}
