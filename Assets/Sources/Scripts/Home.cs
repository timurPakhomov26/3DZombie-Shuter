using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public static Action OnDie;
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _healthPoint = 20f;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Camera _camera;
    public float _currentHealthPoint;


    private void Start() 
    {
       _currentHealthPoint = _healthPoint;  
    }

    private void Update() 
    {
        SetHealthBar();
        RotateCanvas();
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
       _healthBar.fillAmount = (_currentHealthPoint / _healthPoint);
    }

    private void RotateCanvas()
   {
      var toTarget = _camera.transform.position - _canvas.transform.position;
      var toTargetXZ = new Vector3(toTarget.x,0,toTarget.z);
      _canvas.transform.rotation = Quaternion.LookRotation(toTargetXZ);   
   }
}
