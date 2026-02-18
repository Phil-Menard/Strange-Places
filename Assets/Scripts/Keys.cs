using UnityEngine;

public class Keys : MonoBehaviour
{
	[SerializeField] private Player player;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		player.keys++;
		Destroy(gameObject);
	}


}
