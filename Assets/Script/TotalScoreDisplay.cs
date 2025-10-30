using UnityEngine;
using TMPro;

public class TotalScoreDisplay : MonoBehaviour
{
    public static TotalScoreDisplay Instance;

    public TextMeshProUGUI totalScoreText;
    private int totalScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ���� �����Ϸ��� ���
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (totalScoreText != null)
        {
            totalScoreText.text = "Total Score: " + totalScore.ToString();
        }
    }
}
