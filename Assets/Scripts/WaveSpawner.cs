using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
   public Wave[] Waves;
   
   public static int EnemyAlive;
   public Transform spawnPoint;
   public float timeBetweenWaves = 5f;
   public Text waveCountDownText;

   private float countdown = 2f;

   private int waveIndex = 0;
   
   void Start()
   {
      PlayerStats.WavesCount = Waves.Length;
   }
   void Update()
   {
      if (EnemyAlive > 0)
      {
         return;
      }
      if (countdown <= 0f)
      {
         StartCoroutine(SpawnWave());
         countdown = timeBetweenWaves;
         return;
      }

      countdown -= Time.deltaTime;
      
      countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
      
      waveCountDownText.text = string.Format("{0:00}", countdown);
   }

   IEnumerator SpawnWave()
   {
      if (waveIndex == Waves.Length)
      {
         PlayerStats.isWin = true;
         Debug.Log("WON!");
         this.enabled = false;
      }

      if (PlayerStats.Rounds < Waves.Length)
      {
         PlayerStats.Rounds++;
      }

      Wave wave = Waves[waveIndex];
      
      for (int i = 0; i < wave.count; i++)
      { 
         SpawnEnemy(wave.enemy);
         yield return new WaitForSeconds(1f / wave.rate);
      }
      waveIndex++;
   }

   void SpawnEnemy(GameObject enemy)
   {
      Instantiate(enemy.transform, spawnPoint.position, spawnPoint.rotation);
      EnemyAlive++;
   }
}
