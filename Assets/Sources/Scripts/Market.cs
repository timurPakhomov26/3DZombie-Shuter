using System;
using UnityEngine;

public class Market : MonoBehaviour
{
   public static Action OnBuy;
  // public static int CoinsValue = 0;               in PlayerData
   [SerializeField] private Item[] _items;
   [SerializeField] private UIController _uiController;
   private int _cuurentIndex;
   private bool _canBuy = true;

   private void Start() 
   {
      
      _items[0].PricePanel.SetActive(false);
      _items[0].IsBuyed = true;

      for(int i=1; i< _items.Length;i++)
      {
        _items[i].PriceCount.text = _items[i].WeaponInfoo.Price.ToString();   
        if(_items[i].IsBuyed == true)
        {
           _items[i].PricePanel.SetActive(false);
        }    
      }
   }

   private void Update() 
   { 
      BuyWeapon(_uiController.WeaponIndex); 
      if(_cuurentIndex != _uiController.WeaponIndex)
      {
         _canBuy = true;
      }
      else
      {
        _canBuy = false;
      }
   }

   private void BuyWeapon(int index)
   {
      if(PlayerData.CoinsValue >= _items[index].WeaponInfoo.Price && _items[index].PricePanel.activeInHierarchy == true && _canBuy)
      {
        _cuurentIndex = _uiController.WeaponIndex;
         PlayerData.CoinsValue -= _items[index].WeaponInfoo.Price;
         _items[index].IsBuyed = true;
         _items[index].PricePanel.SetActive(false);
         _canBuy = false;
          OnBuy?.Invoke();
         return;
      }
   }


}
