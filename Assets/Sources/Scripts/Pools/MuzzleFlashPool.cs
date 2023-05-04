using UnityEngine;

public class MuzzleFlashPool : MonoBehaviour
{
    [SerializeField] private int _poolCount =3;
    [SerializeField] private bool _autoExpand = false;
    [SerializeField] private MuzzleFlash _partcilePrefab;

    private PoolMono<MuzzleFlash> _pool;
    

    private void Start() 
    {
      _pool = new PoolMono<MuzzleFlash>(_partcilePrefab,_poolCount,transform);
      _pool.AutoExpand = _autoExpand;    
    }

    public void Create(Vector3 position,Vector3 direction)
    {
       var  bullet = _pool.GetFreeElement();
       bullet.transform.position = position;
     
    } 
}
