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
        // AudioSource 컴포넌트 가져오기 또는 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && buttonClickSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        // 버튼 이벤트 리스너 등록
        if (tutorialButton != null)
            tutorialButton.onClick.AddListener(OnTutorialButtonClicked);

        if (startGameButton != null)
            startGameButton.onClick.AddListener(OnStartGameButtonClicked);

        if (optionsButton != null)
            optionsButton.onClick.AddListener(OnOptionsButtonClicked);

        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    // 튜토리얼 버튼 클릭
    public void OnTutorialButtonClicked()
    {
        PlayButtonSound();
        LoadSceneWithDelay(tutorialSceneName);
    }

    // 게임 시작 버튼 클릭
    public void OnStartGameButtonClicked()
    {
        PlayButtonSound();
        LoadSceneWithDelay(gameSceneName);
    }

    // 옵션 버튼 클릭
    public void OnOptionsButtonClicked()
    {
        PlayButtonSound();
        LoadSceneWithDelay(optionsSceneName);
    }

    // 게임 종료 버튼 클릭
    public void OnQuitButtonClicked()
    {
        PlayButtonSound();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // 딜레이 후 씬 로드
    private void LoadSceneWithDelay(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("씬 이름이 설정되지 않았습니다!");
            return;
        }

        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private System.Collections.IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // 버튼 애니메이션이나 사운드 재생 시간을 위한 딜레이
        yield return new WaitForSeconds(transitionDelay);

        // 씬 로드
        SceneManager.LoadScene(sceneName);
    }

    // 버튼 클릭 사운드 재생
    private void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    private void OnDestroy()
    {
        // 메모리 누수 방지를 위한 리스너 제거
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