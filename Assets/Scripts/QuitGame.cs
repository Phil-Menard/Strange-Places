using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
	[SerializeField] private Button quit;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		quit.onClick.AddListener(() => onButtonClicked());
	}

	public void onButtonClicked()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
				Application.Quit();
		#endif
	}
}
