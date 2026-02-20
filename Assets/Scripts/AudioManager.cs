using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
	private static AudioManager instance;
	private Slider slider;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
		audioSource = GetComponent<AudioSource>();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		BindSliderInScene();
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		BindSliderInScene();
	}

	private void BindSliderInScene()
	{
		if (slider != null)
			slider.onValueChanged.RemoveListener(OnSliderChanged);

		slider = FindFirstObjectByType<Slider>(FindObjectsInactive.Include);

		if (slider == null)
			return;

		slider.SetValueWithoutNotify(audioSource.volume);

		slider.onValueChanged.AddListener(OnSliderChanged);
	}

	private void OnSliderChanged(float value)
	{
		audioSource.volume = value;
	}
}
