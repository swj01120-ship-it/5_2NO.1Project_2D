using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class FourDrumRhythmGame : MonoBehaviour
{
    public Transform[] drumTransforms = new Transform[4];
    public GameObject notePrefab;

    public GameObject perfectEffect;
    public GameObject goodEffect;
    public GameObject missEffect;

    public int comboThreshold = 10;

    public int perfectScore = 100;
    public int goodScore = 30;

    public float perfectRange = 0.3f;
    public float goodRange = 0.6f;

    public float noteFallSpeed = 5f;
    public float spawnInterval = 1f;

    private float spawnTimer = 0f;
    private List<Note> activeNotes = new List<Note>();

    public DrumScoreManager scoreManager; // 연결된 ScoreManager 오브젝트

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) JudgeNote(0);
        if (Input.GetKeyDown(KeyCode.S)) JudgeNote(1);
        if (Input.GetKeyDown(KeyCode.D)) JudgeNote(2);
        if (Input.GetKeyDown(KeyCode.F)) JudgeNote(3);

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnRandomNote();
            spawnTimer = 0f;
        }

        for (int i = activeNotes.Count - 1; i >= 0; i--)
        {
            Note note = activeNotes[i];
            if (note != null)
            {
                note.MoveDown(noteFallSpeed * Time.deltaTime);
                if (drumTransforms != null && drumTransforms.Length > note.lane)
                {
                    if (note.transform.position.y <= drumTransforms[note.lane].position.y)
                    {
                        ShowEffect(missEffect, drumTransforms[note.lane].position);
                        Destroy(note.gameObject);
                        activeNotes.RemoveAt(i);

                        scoreManager.RegisterJudgement(note.lane, "Miss");
                    }
                }
            }
        }
    }

    void SpawnRandomNote()
    {
        if (drumTransforms == null || drumTransforms.Length == 0 || notePrefab == null) return;

        int lane = Random.Range(0, drumTransforms.Length);
        Vector3 spawnPos = drumTransforms[lane].position + new Vector3(0f, 7f, 0f);
        GameObject obj = Instantiate(notePrefab, spawnPos, Quaternion.identity);
        Note note = obj.GetComponent<Note>();
        if (note != null)
        {
            note.Init(lane, Time.time);
            activeNotes.Add(note);
        }
    }

    void JudgeNote(int lane)
    {
        if (activeNotes == null || activeNotes.Count == 0) return;
        if (drumTransforms == null || drumTransforms.Length <= lane) return;

        for (int i = activeNotes.Count - 1; i >= 0; i--)
        {
            Note note = activeNotes[i];
            if (note.lane == lane)
            {
                float drumY = drumTransforms[lane].position.y;
                float sDist = Mathf.Abs(note.transform.position.y - drumY);

                if (sDist <= perfectRange)
                {
                    scoreManager.RegisterJudgement(lane, "Perfect");
                    scoreManager.PlayComboEffect(lane);
                    ShowEffect(perfectEffect, drumTransforms[lane].position);

                    Destroy(note.gameObject);
                    activeNotes.RemoveAt(i);
                    return;
                }
                else if (sDist <= goodRange)
                {
                    scoreManager.RegisterJudgement(lane, "Good");
                    scoreManager.PlayComboEffect(lane);
                    ShowEffect(goodEffect, drumTransforms[lane].position);

                    Destroy(note.gameObject);
                    activeNotes.RemoveAt(i);
                    return;
                }
                else
                {
                    scoreManager.RegisterJudgement(lane, "Miss");
                    return;
                }
            }
        }
    }

    void ShowEffect(GameObject effectPrefab, Vector3 pos)
    {
        if (effectPrefab == null) return;

        GameObject effect = Instantiate(effectPrefab, pos, Quaternion.identity);

        // DOTween으로 간단한 크기 애니메이션 (커졌다가 원래 크기)
        effect.transform.localScale = Vector3.one;
        Sequence seq = DOTween.Sequence();
        seq.Append(effect.transform.DOScale(1.3f, 0.3f));
        seq.Append(effect.transform.DOScale(1.0f, 0.3f));

        float delay = 0.5f;
        if (effectPrefab.name.Contains("Perfect")) delay = 0.8f;
        else if (effectPrefab.name.Contains("Good")) delay = 0.7f;
        else if (effectPrefab.name.Contains("Miss")) delay = 0.5f;

        Destroy(effect, delay);
    }
}
