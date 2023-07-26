// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     [SerializeField]
//     public GameObject enemy;
//     public float respawnTime = 0.5f;

//     private bool isEnemySpawned = false; // 이미 적이 생성되었는지를 확인하는 변수
//     // Start is called before the first frame update
//     void Start()
//     {
//         SpawnEnemy();
//     }

//     void Update()
//     {

//     }
//     // Update is called once per frame
//     private void SpawnEnemy() {
//         if(!isEnemySpawned){
//             Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
//             Instantiate(enemy, spawnPos, Quaternion.identity);
//             isEnemySpawned = true;
//         }
//     }
//     public void RespawnEnemy() {
//         isEnemySpawned = false;
//         Invoke("SpawnEnemy", respawnTime);
//     }
// }
