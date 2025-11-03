using UnityEngine;

public class Note : MonoBehaviour
{
    public int lane;        // 노트가 떨어지는 라인 번호 (0~3)
    public float spawnTime; // 노트가 생성된 시간 (게임 시간 기준)

    // 생성될 때 설정하는 초기값 함수
    public void Init(int lane, float spawnTime)
    {
        this.lane = lane;
        this.spawnTime = spawnTime;
    }

    // 매 프레임마다 노트를 떨어뜨리는 함수
    public void MoveDown(float distance)
    {
        transform.position += Vector3.down * distance;
    }
}