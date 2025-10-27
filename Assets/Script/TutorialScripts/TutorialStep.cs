using UnityEngine;

[CreateAssetMenu(fileName = "TutorialStep", menuName = "Tutorial/Step")]
public class TutorialStep : ScriptableObject
{
    public string title;
    [TextArea(3, 5)]
    public string description;
    public string detail;
    public Sprite icon; // 아이콘 이미지
}