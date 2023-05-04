using UnityEngine;

public class AIEnemyesPool : MonoBehaviour
{
   [SerializeField] private int _poolCount =3;
    [SerializeField] private bool _autoExpand = false;
    [SerializeField] private AIEnemy _partcilePrefab;

    private PoolMono<AIEnemy> _pool;

    public int PoolCount => _poolCount;
    

    private void Start() 
    {
      _pool = new PoolMono<AIEnemy>(_partcilePrefab,_poolCount,transform);
      _pool.AutoExpand = _autoExpand;    
    }

    public void Create(Vector3 position,Vector3 direction)
    {
       var  bullet = _pool.GetFreeElement();
       bullet.transform.position = position;
     
    } 
}
