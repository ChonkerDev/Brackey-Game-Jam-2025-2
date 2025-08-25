using UnityEngine;

public class EnemyPatrolPoint : MonoBehaviour
{
    public float MoveToNextPositionDelayInSeconds;

    public EnemyPatrolPointData GeneratePatrolPointData() {
        return new EnemyPatrolPointData() {
            moveToNextPositionDelayInSeconds = MoveToNextPositionDelayInSeconds,
            WorldPosition = transform.position
        };
    }
}
