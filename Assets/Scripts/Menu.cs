using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	[SerializeField] private InputActionAsset inputActions;
	[SerializeField] private RectTransform panel;
	[SerializeField] private RectTransform optionsPanel;
	[SerializeField] private Button play;
	[SerializeField] private Button options;
	[SerializeField] private Button quit;
	[SerializeField] private Button back;

	private InputAction menuAction;
	public bool isPaused;
	public bool inControls = false;
	private bool hasStarted;
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
		back.onClick.AddListener(() => onButtonClicked(4));
	}

	private void Update()
	{
		if (menuAction.WasPressedThisFrame() && hasStarted)
		{
			isPaused = !isPaused;
			if (inControls)
				inControls = false;
		}

		if (isPaused)
		{
			if (inControls)
			{
				panel.gameObject.SetActive(false);
				optionsPanel.gameObject.SetActive(true);
			}
			else
			{
				panel.gameObject.SetActive(true);
				optionsPanel.gameObject.SetActive(false);
			}
			Time.timeScale = 0;
		}
		else
		{
			panel.gameObject.SetActive(false);
			optionsPanel.gameObject.SetActive(false);
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

		if (x == 2)
			inControls = true;

		if (x == 3)
		{
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
				Application.Quit();
			#endif
		}

		if (x == 4)
			inControls = false;
	}
}
