using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public static Action OnShot;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private MuzzleFlashPool _muzzleFlashPool;
    [SerializeField] private BloodPool _bloodPool;
    [SerializeField] private float _shotDelay;
    [SerializeField] private Animator _animator;

    [SerializeField] private Item[] _items;

    [SerializeField] private UIController _uiController;
    private int _index;
    private int _previusItemIndex = -1;
    private float _time;
    private PlayerStates _playerStates;
    
   private void OnEnable() 
   {
      PlayerMovement.OnPlayerMove += AnimRun; 
   }

   private void OnDisable() 
   {
       
      PlayerMovement.OnPlayerMove -= AnimRun; 
   }

   private void Start() 
   {
       EquipItem(_uiController.WeaponIndex);   
   }

    private void Update() 
    {

      for(int i = 0; i < _items.Length; i++)
      {
         if(Input.GetKeyDown((i + 1).ToString()) == false) 
             continue;

         EquipItem(i);
         break;
      }

       _time += Time.deltaTime;
       if(Input.GetMouseButtonDown(0) && _time >= _shotDelay)
       {
          _animator.SetTrigger("Shot");

           Shoot();
          OnShot?.Invoke();
           _time = 0;
       }    
    }

    private void Shoot()
    {   

       _muzzleFlashPool.Create(_items[_index].muzzleFlash.position,_items[_index].muzzleFlash.forward);
       RaycastHit hit;

      if(Physics.Raycast(_camera.transform.position,_camera.transform.forward,out hit,_range)) 
      {
          if(hit.transform == null)
            return;

          if(hit.transform.GetComponent<IDamageFromWeapon>() != null)
          {
             hit.transform.GetComponent<IDamageFromWeapon>().TakeDamage(_damage);
             _bloodPool.Create(hit.point,Vector3.zero);
            // _bulletHolePool.Create(hit.point,hit.normal);
          }
          

          
      }
    }

     private void AnimRun(bool f)
     {
       _animator.SetBool("Run",f);
     }

     private void EquipItem(int index)
     {
        _index = index;

        if(_index == _previusItemIndex)
           return;

        if(_index >= _items.Length)
           return;

        _items[_index].weaponGameobject.SetActive(true);

        if(_previusItemIndex != -1)
        {
           _items[_previusItemIndex].weaponGameobject.SetActive(false);
        }

        _previusItemIndex = _index;
     }
    
}
