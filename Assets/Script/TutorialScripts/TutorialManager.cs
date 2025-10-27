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
        // ��ư �̺�Ʈ ����
        nextButton.onClick.AddListener(NextStep);
        prevButton.onClick.AddListener(PrevStep);
        startButton.onClick.AddListener(StartGame);
        skipButton.onClick.AddListener(StartGame);

        // ù ȭ�� ǥ��
        ShowStep(0);
    }

    void ShowStep(int index)
    {
        currentStep = index;
        TutorialStep step = steps[index];

        // �ؽ�Ʈ ������Ʈ
        titleText.text = step.title;
        descriptionText.text = step.description;
        detailText.text = step.detail;

        // ������ ������Ʈ (������)
        if (iconImage != null && step.icon != null)
        {
            iconImage.sprite = step.icon;
        }

        // ��ư ǥ�� ����
        prevButton.gameObject.SetActive(index > 0);
        nextButton.gameObject.SetActive(index < steps.Length - 1);
        startButton.gameObject.SetActive(index == steps.Length - 1);
        skipButton.gameObject.SetActive(index < steps.Length - 1);

        // ���� ǥ�� ������Ʈ
        UpdateProgressDots();
    }

    void UpdateProgressDots()
    {
        for (int i = 0; i < progressDots.Length; i++)
        {
            if (i == currentStep)
            {
                // Ȱ�� ��: ���, ��ȫ��
                progressDots[i].rectTransform.sizeDelta = new Vector2(48f, 8f);
                progressDots[i].color = new Color(1f, 0.4f, 0.8f);
            }
            else
            {
                // ��Ȱ�� ��: �۰� �ձ۰�, ��� ������
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
        // ���� ������ �̵�
        SceneManager.LoadScene("GameScene");
    }
}