using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	[SerializeField] private GameObject text;
	[SerializeField] private Player player;
	[SerializeField] private int nbKeysRequired = 1;
	[SerializeField] private int nextLevelIndex = 1;
	public bool changeLevel = false;
	private bool isLoading = false;
	private TextMeshProUGUI tmp;

	private void Awake()
	{
		tmp = text.GetComponent<TextMeshProUGUI>();
		if (!tmp)
			return;
	}

	private void Update()
	{
		if (changeLevel && !isLoading)
		{
			SceneManager.LoadScene(nextLevelIndex);
			isLoading = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (player.keys == nbKeysRequired)
			changeLevel = true;
		else
			tmp.enabled = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		tmp.enabled = false;
	}
}
