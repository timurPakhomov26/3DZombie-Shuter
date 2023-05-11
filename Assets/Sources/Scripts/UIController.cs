using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
   public static Action OnUseRunBust;
   public static Action OnUseMedicineChest;
   [SerializeField] private GameObject _winPanel;
   [SerializeField] private GameObject _losePanel;

   [SerializeField] private Text _allZombie;
   [SerializeField] private Text _killedZombie;
   [SerializeField] private  int _kileedZombieValue;
     
   [SerializeField] private Text _coins;
   [SerializeField] private EnemyesPoolController _enemyController;

   [SerializeField] private GameObject _mobileUIPanel;

   [SerializeField] private PlayerMovement _playerMovement;
   [SerializeField] private Weapon _weapon;
   [SerializeField] private int _weaponIndex;
   [SerializeField] private TouchLook _touchLook;
   [SerializeField] private MouseLook _mouseLook;
   [SerializeField] private GameObject _startPanel;
   [SerializeField] private PlayerUI _playerUI;
   [SerializeField] private Home _home;
   [SerializeField] private AudioSource _zobmieAudio;
   [SerializeField] private Item[] _items;
   [SerializeField] private Text _runBust;
   //// public static int RunBustCount;    in PlayerData
   [SerializeField] private Text _medicineChest;
   //// public static int MedicineChestCount;      in PlayerData
   private int _allZombiesCount;
   private int _deadZombies = 0;

   [SerializeField] private Text _runBustPriceText;
   [SerializeField] private Text _medicineSchestPriceText;
   [SerializeField] private int _runBustPrice;
   [SerializeField] private int _medicineChestPrice;
   public int WeaponIndex => _weaponIndex;



     private void OnEnable() 
     {
       PlayerUI.OnDie += Lose; 
       Home.OnDie += Lose;
       AIEnemy.OnZombieDied += AddStats;
       AIEnemy.OnZombieDied += ReloadText;  
       AIEnemy.OnZombieDied += DiedZombie; 
       Market.OnBuy += ReloadText;
     }

     private void OnDisable() 
     {
       PlayerUI.OnDie -= Lose; 
       Home.OnDie -= Lose;
       AIEnemy.OnZombieDied -= AddStats;
       AIEnemy.OnZombieDied -= ReloadText;   
       AIEnemy.OnZombieDied -= DiedZombie; 
       Market.OnBuy -= ReloadText;   
     }

     private void Awake() 
     {
       _deadZombies = 0;
       SetState(0,true,false,false);
       SetText();
      _allZombiesCount = Mathf.CeilToInt((_enemyController.CreateZombieValue + _enemyController.CreateHeadZombieValue) * 
         PlayerData.ZombyCountKoefficent);
      _allZombie.text = _allZombiesCount.ToString();    
     }

    private void Start() 
    {
     // Cursor.lockState = CursorLockMode.Locked;
       _medicineSchestPriceText.text = _medicineChestPrice.ToString();
       _runBustPriceText.text = _runBustPrice.ToString();
       _winPanel.SetActive(false);
       _losePanel.SetActive(false);

    if(DeviceTypes.Instance.CurrentDeviceType == DeviceTypeWEB.Mobile)
     {
      _mouseLook.enabled = false;
      _touchLook.enabled = true;
       Cursor.lockState = CursorLockMode.Confined;
       // Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = true;
        _mobileUIPanel.SetActive(true);
     }
     else
     {
       // Cursor.lockState = CursorLockMode.Confined;
       // Cursor.visible = true;
        _mouseLook.enabled = true;
        _touchLook.enabled = false;
        _mobileUIPanel.SetActive(false);
     }
    }
    

    private void Update() 
    {
       if(_allZombiesCount <= _deadZombies)
       {
          Win();
       }  
    }

    public void SetWeapon(int index)
    {
        _weaponIndex = index;
    }

    private void ReloadText()
    {
       SetText();
    }

    private void DiedZombie()
    {
       
       _kileedZombieValue++;
    }
    private void AddStats()
    {
       _deadZombies ++;
       PlayerData.CoinsValue += 10;
    }

    private void Win()
    {
      _winPanel.SetActive(true);
       SetState(0,false,false,false);

      _zobmieAudio.Stop();
    }

    private void Lose()
    {
      _losePanel.SetActive(true);
      SetState(0,false,false,false);
      _zobmieAudio.Stop();
    }

    private void SetState(int timeScale,bool startPanel,bool scripts,bool mouseLook)
    {
       Time.timeScale = timeScale;  
       _startPanel.SetActive(startPanel);
       _playerMovement.enabled = scripts; 
       _weapon.enabled = scripts; 
       _mouseLook.enabled = mouseLook;
    }

    private void SetText()
    {
      _allZombie.text = _allZombiesCount.ToString();
      _coins.text = PlayerData.CoinsValue.ToString(); 
      _killedZombie.text = _kileedZombieValue.ToString() + " /";
      _runBust.text = PlayerData.RunBustCount.ToString();
      _medicineChest.text = PlayerData.MedicineChestCount.ToString();
    }

   public void StartGame()
   {
      if(_items[WeaponIndex].PricePanel.activeInHierarchy == false)
      { 
         if(DeviceTypes.Instance.CurrentDeviceType == DeviceTypeWEB.Desktop)
         {
           Cursor.lockState = CursorLockMode.Locked;
           SetState(1,false,true,true);

         _zobmieAudio.Play();

         }
         else
         {
          
           SetState(1,false,true,false);
            _zobmieAudio.Play();
         }
      }   
   }
    
    public void ReloadLevelWin()
    {
      PlayerData.ZombyCountKoefficent += 0.6f;
      StartCoroutine(ReloadLevelCoroutine());
    }

    public void ReloadLevelLose()
    {
      StartCoroutine(ReloadLevelCoroutine());
    }

    IEnumerator ReloadLevelCoroutine()
    {
        yield return null; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }

    public void Revival()
    {
      if(DeviceTypes.Instance.CurrentDeviceType == DeviceTypeWEB.Desktop)
      {
        _losePanel.SetActive(false);
        SetState(1,false,true,true);
      }

      else
      {
        _losePanel.SetActive(false);
        SetState(1,false,true,false);

      }
      _playerUI._currentHealthPoint = 30;
      _home._currentHealthPoint = 100;
    }

    public void BuyRunBust()
    {
      BuyBust(_runBustPrice,ref PlayerData.RunBustCount);
    }
    public void BuyMedicineChest()
    {
      BuyBust(_medicineChestPrice, ref PlayerData.MedicineChestCount);
    }

   private void BuyBust(int bustPrice,ref int  bustCount)
   {
      if(PlayerData.CoinsValue >= bustPrice)
      {
       bustCount ++;
       PlayerData.CoinsValue -= bustPrice;
       ReloadText();
      }
   }

   public void UseRunBust()
   {
      if(PlayerData.RunBustCount > 0)
      {
         PlayerData.RunBustCount --;
         ReloadText();
         OnUseRunBust?.Invoke();
      }   
   }

   public void UseMedicinechest()
   {
      if(PlayerData.MedicineChestCount > 0)
      {
        PlayerData.MedicineChestCount --;
        ReloadText();
        OnUseMedicineChest?.Invoke();
      }
      
   }
}
