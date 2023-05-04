using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
   public static int _coinsValue = 0;
   [SerializeField] private GameObject _winPanel;
   [SerializeField] private GameObject _losePanel;

   [SerializeField] private Text _allZombie;
   [SerializeField] private Text _killedZombie;
   [SerializeField] private  int _kileedZombieValue;
     
   [SerializeField] private Text _coins;
   [SerializeField] private EnemyesPoolController _enemyController;

   [SerializeField] private PlayerMovement _playerMovement;
   [SerializeField] private Weapon _weapon;
   [SerializeField] private int _weaponIndex;
   [SerializeField] private MouseLook _mouseLook;
   [SerializeField] private GameObject _startPanel;
   private int _allZombiesCount;
   private int _deadZombies = 0;
   public int WeaponIndex => _weaponIndex;
         
    
     private void OnEnable() 
     {
       PlayerUI.OnDie += Lose; 
       Home.OnDie += Lose;
       AIEnemy.OnZombieDied += AddStats;
       AIEnemy.OnZombieDied += ReloadText;   
     }

     private void OnDisable() 
     {
       PlayerUI.OnDie -= Lose; 
       Home.OnDie -= Lose;
       AIEnemy.OnZombieDied -= AddStats;
       AIEnemy.OnZombieDied -= ReloadText;   
        
     }

     private void Awake() 
     {
       _deadZombies = 0;

       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
       SetState(0,true,false);
       SetText();
      _allZombiesCount = Mathf.CeilToInt((_enemyController.CreateZombieValue + _enemyController.CreateHeadZombieValue) * 
         EnemyesPoolController.ZombyCountKoefficent);
      _allZombie.text = _allZombiesCount.ToString();
      
       
     }
    private void Start() 
    {
       _winPanel.SetActive(false);
       _losePanel.SetActive(false);
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
       _kileedZombieValue++;
       SetText();
    }
    private void AddStats()
    {
       _deadZombies ++;
       _coinsValue += 10;
    }


    private void Win()
    {
      _winPanel.SetActive(true);
       SetState(0,false,false);

    }

    private void Lose()
    {
      _losePanel.SetActive(true);
      SetState(0,false,false);
    }


    private void SetState(int timeScale,bool startPanel,bool scripts)
    {
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
       Time.timeScale = timeScale;  
       _startPanel.SetActive(startPanel);
       _playerMovement.enabled = scripts; 
       _weapon.enabled = scripts; 
       _mouseLook.enabled = scripts;
    }

    private void SetText()
    {
      _allZombie.text = _allZombiesCount.ToString();
      _coins.text = _coinsValue.ToString(); 
      _killedZombie.text = _kileedZombieValue.ToString() + " /";
    }

   public void StartGame()
   {
       SetState(1,false,true);
   }

    
    public void ReloadLevelWin()
    {

      EnemyesPoolController.ZombyCountKoefficent += 0.6f;
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
}
