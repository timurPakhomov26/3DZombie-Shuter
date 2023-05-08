using UnityEngine;
using System;

public class EnemyesPoolController : MonoBehaviour
{
 
   //public static float ZombyCountKoefficent = 1;       in PlayerData
   [SerializeField] private Transform[] _enemyesSpawns;
   [SerializeField] private int _random;
   [SerializeField] private int _creteZombieValue;
   public int CreateZombieValue => _creteZombieValue;
   [SerializeField] private int _createHeadZombieValue;
   public int CreateHeadZombieValue => _createHeadZombieValue;
   [SerializeField] private GameObject _zombiePrefab;
   [SerializeField] private GameObject _headZombiePrefab;
   [SerializeField] private float _timeBettwenCreate = 0;
   private float _timeForZombie;
   private float _timeForHeadZombie;
   private int _zombyCount = 0;
   private int _headZombieCount = 0;

   
  

  private void Start() 
  {
    _timeForZombie = _timeBettwenCreate;  
  }
  private void Update() 
   {
     SpawnZombie();
     SpawnHead();
   }
   private void SpawnZombie()
   {
       _timeForZombie += Time.deltaTime;
       if(_timeForZombie > _timeBettwenCreate && _zombyCount < Mathf.CeilToInt(_creteZombieValue * PlayerData.ZombyCountKoefficent))
       {
          _random = UnityEngine.Random.Range(0,_enemyesSpawns.Length - 1);
          Instantiate(_zombiePrefab,_enemyesSpawns[_random].position,Quaternion.identity);
         _zombyCount++;
         _timeForZombie = 0;
       }
   }
   private void SpawnHead()
   {
      _timeForHeadZombie += Time.deltaTime;

       if(_timeForHeadZombie > _timeBettwenCreate && _headZombieCount < Mathf.CeilToInt(_createHeadZombieValue * PlayerData.ZombyCountKoefficent))
       {
          _random = UnityEngine.Random.Range(0,_enemyesSpawns.Length - 1);
          Instantiate(_headZombiePrefab,_enemyesSpawns[_random].position,Quaternion.identity);
         _headZombieCount++;
         _timeForHeadZombie = 0;
       }
   }

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
