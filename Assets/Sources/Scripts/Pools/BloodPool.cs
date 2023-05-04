using UnityEngine;

public class BloodPool : MonoBehaviour
{
   [SerializeField] private int _poolCount =3;
    [SerializeField] private bool _autoExpand = false;
    [SerializeField] private Blood _partcilePrefab;

    private PoolMono<Blood> _pool;
    

    private void Start() 
    {
      _pool = new PoolMono<Blood>(_partcilePrefab,_poolCount,transform);
      _pool.AutoExpand = _autoExpand;    
    }

    public void Create(Vector3 position,Vector3 direction)
    {
       var  bullet = _pool.GetFreeElement();
       bullet.transform.position = position;
     
    } 
}
