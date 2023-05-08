using UnityEngine;
using System;
using System.Collections;

public enum WeaponType
{
   Automatic,
   NonAutomatic
}
public class Weapon : MonoBehaviour
{
    public static Action OnShot;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _range;
    [SerializeField] private MuzzleFlashPool _muzzleFlashPool;
    [SerializeField] private BloodPool _bloodPool;
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
       
       if(_items[_index].WeaponInfoo.Type == WeaponType.Automatic)
       {
          AutomaticShoot();
       }
       else if(_items[_index].WeaponInfoo.Type == WeaponType.NonAutomatic)
       {
          NonAutomaticShoot();
       }

    }
    private void AutomaticShoot()
    {
       if(Input.GetMouseButton(0) && _time >= _items[_index].WeaponInfoo.RateOfFire)
       {
          _items[_index]._shotSound.Play();

           BulletFly();
           OnShot?.Invoke();
           _time = 0;
       }  
       if(Input.GetMouseButtonUp(0))
       {
          _items[_index]._shotSound.Stop();

       } 
    }

    private void NonAutomaticShoot()
    {
       if(Input.GetMouseButtonDown(0) && _time >= _items[_index].WeaponInfoo.RateOfFire)
       {
          _items[_index]._shotSound.Play();

           BulletFly();
           OnShot?.Invoke();
           _time = 0;
       } 
        if(Input.GetMouseButtonUp(0))
       {
         _items[_index]._shotSound.Stop();

       }  
    }

    private void BulletFly()
    {   

       _muzzleFlashPool.Create(_items[_index].muzzleFlash.position,_items[_index].muzzleFlash.forward);
       RaycastHit hit;
       int zombieDieLayer = 1 << 7;
       int allWithoutDieLAyer = ~zombieDieLayer;
       
      if(Physics.Raycast(_camera.transform.position,_camera.transform.forward,out hit,_range,allWithoutDieLAyer)) 
      {
          if(hit.transform == null)
            return;

          if(hit.transform.GetComponent<IDamageFromWeapon>() != null)
          {
             hit.transform.GetComponent<IDamageFromWeapon>().TakeDamage(_items[_index].WeaponInfoo.Damage);
             _bloodPool.Create(hit.point,Vector3.zero);
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

        if(_items[index].IsBuyed == false)
            return;
       
        _items[_index].weaponGameobject.SetActive(true);

        if(_previusItemIndex != -1)
        {
           _items[_previusItemIndex].weaponGameobject.SetActive(false);
        }

        _previusItemIndex = _index;
     }

     
    
}
