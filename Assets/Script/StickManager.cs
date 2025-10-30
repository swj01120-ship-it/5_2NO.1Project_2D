using UnityEngine;

public class StickManager : MonoBehaviour
{
    public Animator left_stick;   // ���� ��ƽ Animator
    public Animator right_stick;  // ������ ��ƽ Animator

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            left_stick.SetTrigger("Drum_A");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            left_stick.SetTrigger("Drum_S");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            right_stick.SetTrigger("Drum_D");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            right_stick.SetTrigger("Drum_F");
        }
    }
}
