using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    [Header("Scene Names")]
    [SerializeField] private string tutorialSceneName = "TutorialScene";
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private string optionsSceneName = "OptionsScene";

    [Header("Transition Settings")]
    [SerializeField] private float transitionDelay = 0.3f;
    [SerializeField] private AudioClip buttonClickSound;

    private AudioSource audioSource;

    private void Awake()
    {
        // AudioSource ������Ʈ �������� �Ǵ� �߰�
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && buttonClickSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        // ��ư �̺�Ʈ ������ ���
        if (tutorialButton != null)
            tutorialButton.onClick.AddListener(OnTutorialButtonClicked);

        if (startGameButton != null)
            startGameButton.onClick.AddListener(OnStartGameButtonClicked);

        if (optionsButton != null)
            optionsButton.onClick.AddListener(OnOptionsButtonClicked);

        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    // Ʃ�丮�� ��ư Ŭ��
    public void OnTutorialButtonClicked()
    {
        PlayButtonSound();
        LoadSceneWithDelay(tutorialSceneName);
    }

    // ���� ���� ��ư Ŭ��
    public void OnStartGameButtonClicked()
    {
        PlayButtonSound();
        LoadSceneWithDelay(gameSceneName);
    }

    // �ɼ� ��ư Ŭ��
    public void OnOptionsButtonClicked()
    {
        PlayButtonSound();
        LoadSceneWithDelay(optionsSceneName);
    }

    // ���� ���� ��ư Ŭ��
    public void OnQuitButtonClicked()
    {
        PlayButtonSound();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // ������ �� �� �ε�
    private void LoadSceneWithDelay(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("�� �̸��� �������� �ʾҽ��ϴ�!");
            return;
        }

        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private System.Collections.IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // ��ư �ִϸ��̼��̳� ���� ��� �ð��� ���� ������
        yield return new WaitForSeconds(transitionDelay);

        // �� �ε�
        SceneManager.LoadScene(sceneName);
    }

    // ��ư Ŭ�� ���� ���
    private void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    private void OnDestroy()
    {
        // �޸� ���� ������ ���� ������ ����
        if (tutorialButton != null)
            tutorialButton.onClick.RemoveListener(OnTutorialButtonClicked);

        if (startGameButton != null)
            startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);

        if (optionsButton != null)
            optionsButton.onClick.RemoveListener(OnOptionsButtonClicked);

        if (quitButton != null)
            quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }
}