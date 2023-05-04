using UnityEngine;
using System;

public class EnemyesPoolController : MonoBehaviour
{
  // [SerializeField] private  AIEnemyesPool _enemyesPool;
   public static float ZombyCountKoefficent = 1;
   [SerializeField] private Transform[] _enemyesSpawns;
   [SerializeField] private int _random;
   [SerializeField] private int _creteZombieValue;
   public int CreateZombieValue => _creteZombieValue;
   [SerializeField] private int _createHeadZombieValue;
   public int CreateHeadZombieValue => _createHeadZombieValue;
   [SerializeField] private GameObject _zombiePrefab;
   [SerializeField] private GameObject _headZombiePrefab;
   private float _timeForZombie;
   private float _timeForHeadZombie;
   private int _zombyCount = 0;
   private int _headZombieCount = 0;
   

  private void Update() 
   {
     Spawn();
   }
   private void Spawn()
   {
       _timeForZombie += Time.deltaTime;
       if(_timeForZombie > 4f && _zombyCount < Mathf.CeilToInt(_creteZombieValue * ZombyCountKoefficent))
       {
          _random = UnityEngine.Random.Range(0,_enemyesSpawns.Length - 1);
          Instantiate(_zombiePrefab,_enemyesSpawns[_random].position,Quaternion.identity);
         _zombyCount++;
         _timeForZombie = 0;
       }
   }

   /* private void Spawn()
   {
        CreateZombie(_timeForZombie,_zombyCount,_creteZombieValue,_zombiePrefab);
        CreateZombie(_timeForHeadZombie,_headZombieCount,_createHeadZombieValue,_headZombiePrefab);
   }*/

   private void CreateZombie(float time, int zombieCount, int createValue,GameObject zombiePrefab)
   {
      
          if(time > 4f && zombieCount < createValue )
           {
             _random = UnityEngine.Random.Range(0,_enemyesSpawns.Length - 1);
             Instantiate(zombiePrefab,_enemyesSpawns[_random].position,Quaternion.identity);
            zombieCount++;
            time = 0;                  
          
           }
   }
}
