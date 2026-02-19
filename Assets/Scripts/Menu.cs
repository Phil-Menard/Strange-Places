using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	[SerializeField] private Button play;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		play.onClick.AddListener(() => onButtonClicked());
	}

	public void onButtonClicked()
	{
		SceneManager.LoadScene("Level1");
	}
}
