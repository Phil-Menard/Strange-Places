using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	[SerializeField] private InputActionAsset inputActions;
	[SerializeField] private RectTransform panel;
	[SerializeField] private Button play;
	[SerializeField] private Button options;
	[SerializeField] private Button quit;

	private InputAction menuAction;
	public bool isPaused;
	public bool hasStarted;
	private string currentScene;
	

	private void OnEnable()
	{
		inputActions.FindActionMap("Player").Enable();
	}

	private void Awake()
	{
		currentScene = SceneManager.GetActiveScene().name;
		menuAction = InputSystem.actions.FindAction("Menu");
		if (currentScene == "Level1")
		{
			isPaused = true;
			hasStarted = false;
		}
		else
		{
			isPaused = false;
			hasStarted = true;
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		play.onClick.AddListener(() => onButtonClicked(1));
		options.onClick.AddListener(() => onButtonClicked(2));
		quit.onClick.AddListener(() => onButtonClicked(3));
	}

	private void Update()
	{
		if (menuAction.WasPressedThisFrame() && hasStarted)
			isPaused = !isPaused;

		if (isPaused)
		{
			panel.gameObject.SetActive(true);
			Time.timeScale = 0;
		}
		else
		{
			panel.gameObject.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void onButtonClicked(int x)
	{
		if (x == 1)
		{
			if (!hasStarted)
				hasStarted = true;
			
			isPaused = false;
		}

		if (x == 3)
		{
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}
	}
}
