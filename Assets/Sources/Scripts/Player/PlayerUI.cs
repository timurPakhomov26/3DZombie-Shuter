using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static Action OnDie;
    [SerializeField] private Text _healthBar;
    [SerializeField] private float _healthPoint = 20f;
    public float _currentHealthPoint;

    
    private void OnEnable() 
    {
        UIController.OnUseMedicineChest += MedicineBustUse;
    }

    private void OnDisable() 
    {
          UIController.OnUseMedicineChest -= MedicineBustUse; 
    }

    private void Start() 
    {
       _currentHealthPoint = _healthPoint;  
    }

    private void Update() 
    {
        SetHealthBar();
    }
    public void TakeDamage(float value)
    {
        _currentHealthPoint-= value;

        if(_currentHealthPoint <= 0)
        {
           _currentHealthPoint = 0;
            OnDie?.Invoke();
        }
    }

    private void SetHealthBar()
    {
       _healthBar.text = _currentHealthPoint.ToString();
    }

    private void MedicineBustUse()
    {
      _currentHealthPoint = _healthPoint;
    }


    

}
