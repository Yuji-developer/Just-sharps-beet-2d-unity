using UnityEngine;

// インスペクターで設定できるようにする
[System.Serializable]
public class EnemyGroup
{
    public GameObject enemyPrefab; // どの敵を出すか
    public int count;              // 何体出すか
    public float interval;         // 出現の間隔（秒）
    public float beatTime;         // 出現するタイミング（拍数または距離）
}
