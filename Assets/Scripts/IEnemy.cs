using UnityEngine;

public enum EnemyStatus
{
    Inactive,

    Active
}
public interface IEnemy
{
    public EnemyStatus GetEnemyStatus();
}
