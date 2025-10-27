using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI detailText;
    public Image iconImage;

    [Header("Buttons")]
    public Button nextButton;
    public Button prevButton;
    public Button startButton;
    public Button skipButton;

    [Header("Progress Indicators")]
    public Image[] progressDots;

    [Header("Tutorial Steps")]
    public TutorialStep[] steps;

    [Header("Animation")]
    public CanvasGroup cardCanvasGroup;

    private int currentStep = 0;

    void Start()
    {
        // 버튼 이벤트 연결
        nextButton.onClick.AddListener(NextStep);
        prevButton.onClick.AddListener(PrevStep);
        startButton.onClick.AddListener(StartGame);
        skipButton.onClick.AddListener(StartGame);

        // 첫 화면 표시
        ShowStep(0);
    }

    void ShowStep(int index)
    {
        currentStep = index;
        TutorialStep step = steps[index];

        // 텍스트 업데이트
        titleText.text = step.title;
        descriptionText.text = step.description;
        detailText.text = step.detail;

        // 아이콘 업데이트 (있으면)
        if (iconImage != null && step.icon != null)
        {
            iconImage.sprite = step.icon;
        }

        // 버튼 표시 제어
        prevButton.gameObject.SetActive(index > 0);
        nextButton.gameObject.SetActive(index < steps.Length - 1);
        startButton.gameObject.SetActive(index == steps.Length - 1);
        skipButton.gameObject.SetActive(index < steps.Length - 1);

        // 진행 표시 업데이트
        UpdateProgressDots();
    }

    void UpdateProgressDots()
    {
        for (int i = 0; i < progressDots.Length; i++)
        {
            if (i == currentStep)
            {
                // 활성 점: 길게, 분홍색
                progressDots[i].rectTransform.sizeDelta = new Vector2(48f, 8f);
                progressDots[i].color = new Color(1f, 0.4f, 0.8f);
            }
            else
            {
                // 비활성 점: 작고 둥글게, 흰색 반투명
                progressDots[i].rectTransform.sizeDelta = new Vector2(8f, 8f);
                progressDots[i].color = new Color(1f, 1f, 1f, 0.3f);
            }
        }
    }

    void NextStep()
    {
        if (currentStep < steps.Length - 1)
        {
            ShowStep(currentStep + 1);
        }
    }

    void PrevStep()
    {
        if (currentStep > 0)
        {
            ShowStep(currentStep - 1);
        }
    }

    void StartGame()
    {
        // 게임 씬으로 이동
        SceneManager.LoadScene("GameScene");
    }
}