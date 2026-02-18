using UnityEngine;

public class Keys : MonoBehaviour
{
	[SerializeField] private Player player;
	private GameObject parent;

	private void Awake()
	{
		parent = transform.parent.gameObject;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		player.keys++;
		Destroy(parent);
	}
}
