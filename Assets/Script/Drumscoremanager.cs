using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class DrumScoreManager : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] comboTexts = new TextMeshProUGUI[4];
    public ParticleSystem[] comboEffects = new ParticleSystem[4];

    private int[] currentScores = new int[4];
    private int[] currentCombos = new int[4];

    public float displayTime = 0.7f;
    private Coroutine[] popupCoroutines = new Coroutine[4];

    // ���� �� ȣ���ϴ� �Լ�
    public void RegisterJudgement(int drumIndex, string judgementType)
    {
        int scoreToAdd = 0;

        bool isGoodOrPerfect = false;  // �޺� ����Ʈ ���� üũ��

        switch (judgementType)
        {
            case "Perfect":
                scoreToAdd = 100;
                currentCombos[drumIndex]++;
                isGoodOrPerfect = true;
                break;
            case "Good":
                scoreToAdd = 50;
                currentCombos[drumIndex]++;
                isGoodOrPerfect = true;
                break;
            case "Miss":
                scoreToAdd = 0;
                currentCombos[drumIndex] = 0;
                break;
        }

        currentScores[drumIndex] += scoreToAdd;

        if (scoreToAdd > 0 && TotalScoreDisplay.Instance != null)
            TotalScoreDisplay.Instance.AddScore(scoreToAdd);

        UpdateScoreUI(drumIndex);
        UpdateComboUI(drumIndex);

        string message = (scoreToAdd > 0) ? "+" + scoreToAdd.ToString() : "MISS";

        ShowScorePopup(drumIndex, message, judgementType);

        // Perfect �Ǵ� Good �� ���� �޺� ����Ʈ ���
        if (isGoodOrPerfect)
        {
            PlayComboEffect(drumIndex);
        }
    }

    private void UpdateScoreUI(int drumIndex)
    {
        scoreTexts[drumIndex].text = "Score: " + currentScores[drumIndex];
        AnimateScoreText(drumIndex);
    }

    private void UpdateComboUI(int drumIndex)
    {
        comboTexts[drumIndex].text = currentCombos[drumIndex] > 0 ? currentCombos[drumIndex] + " Combo" : "";
    }

    // ����Ʈ ���(���� �޺� ���� �� ȣ��)
    public void PlayComboEffect(int drumIndex)
    {
        if (comboEffects != null && comboEffects.Length > drumIndex && comboEffects[drumIndex] != null)
        {
            Debug.Log($"�ݺ� ����Ʈ ����: �巳 {drumIndex}");
            comboEffects[drumIndex].Stop();
            comboEffects[drumIndex].Play();
        }
    }

    private void ShowScorePopup(int drumIndex, string message, string judgementType)
    {
        if (popupCoroutines[drumIndex] != null)
            StopCoroutine(popupCoroutines[drumIndex]);

        popupCoroutines[drumIndex] = StartCoroutine(ScorePopupRoutine(drumIndex, message, judgementType));
    }

    private IEnumerator ScorePopupRoutine(int drumIndex, string message, string judgementType)
    {
        TextMeshProUGUI popupText = scoreTexts[drumIndex];

        if (message == "MISS")
        {
            popupText.gameObject.SetActive(false);
            yield break;
        }

        Color perfectColor = new Color(1f, 0.65f, 0f);
        Color goodColor = new Color(0f, 0.4f, 0.6f);

        if (judgementType == "Perfect")
            popupText.color = perfectColor;
        else if (judgementType == "Good")
            popupText.color = goodColor;

        popupText.fontWeight = FontWeight.Bold;
        popupText.fontSize = 52;

        popupText.text = message;
        popupText.gameObject.SetActive(true);

        Vector3 originalScale = popupText.transform.localScale;
        Vector3 originalPosition = popupText.transform.localPosition;

        float timer = 0f;
        float moveUpDistance = 30f;

        while (timer < displayTime)
        {
            float progress = timer / displayTime;
            popupText.transform.localScale = Vector3.Lerp(originalScale * 1.1f, originalScale, progress);
            Color c = popupText.color;
            c.a = Mathf.Lerp(1f, 0f, progress);
            popupText.color = c;
            popupText.transform.localPosition = Vector3.Lerp(originalPosition, originalPosition + Vector3.up * moveUpDistance, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        popupText.color = new Color(popupText.color.r, popupText.color.g, popupText.color.b, 1f);
        popupText.transform.localScale = originalScale;
        popupText.transform.localPosition = originalPosition;
        popupText.fontSize = 48;
        popupText.fontWeight = FontWeight.Regular;
        popupText.gameObject.SetActive(false);
    }

    private void AnimateScoreText(int drumIndex)
    {
        TextMeshProUGUI txt = scoreTexts[drumIndex];

        Vector3 originalScale = txt.transform.localScale;
        Sequence seq = DOTween.Sequence();
        seq.Append(txt.transform.DOScale(originalScale * 1.3f, 0.15f));
        seq.Append(txt.transform.DOScale(originalScale, 0.15f));
        seq.Join(txt.DOColor(Color.red, 0.15f));
        seq.Append(txt.DOColor(txt.color, 0.15f)); // ���� ������ �ٽ� ����
    }
}
