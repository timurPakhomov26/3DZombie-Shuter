using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
   public  bool  IsBuyed = false;
   public WeaponInfo WeaponInfoo;
   public GameObject weaponGameobject;
   public Transform muzzleFlash;
   public GameObject PricePanel;
   public Text PriceCount;
   public AudioSource _shotSound;
   
}
