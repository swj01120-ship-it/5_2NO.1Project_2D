using UnityEngine;

public class Note : MonoBehaviour
{
    public int lane;        // ��Ʈ�� �������� ���� ��ȣ (0~3)
    public float spawnTime; // ��Ʈ�� ������ �ð� (���� �ð� ����)

    // ������ �� �����ϴ� �ʱⰪ �Լ�
    public void Init(int lane, float spawnTime)
    {
        this.lane = lane;
        this.spawnTime = spawnTime;
    }

    // �� �����Ӹ��� ��Ʈ�� ����߸��� �Լ�
    public void MoveDown(float distance)
    {
        transform.position += Vector3.down * distance;
    }
}