using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public  enum PlayerStates
{
   Move,
   Attack
}

public class PlayerMovement : MonoBehaviour
{
   public const float  GRAVITY_SCALE = -11f;
   public delegate void OnPlayerDo(bool f);
   public static OnPlayerDo OnPlayerMove;
   [SerializeField] private Transform _groundCheck;
   [SerializeField] private float _groundDistance;
   [SerializeField] private LayerMask _groundMask;
   [SerializeField] private CharacterController _controller;
   [SerializeField] private float _speed;
   [SerializeField] private float _speedInAir;
   [SerializeField] private float _acceleration;
   [SerializeField] private float _jumpHeight;


   [SerializeField] private Image _staminaBar;
   [SerializeField] private float _staminaValue;
   [SerializeField] private bool _isGrounded;
    private float _currentStaminaValue;
    [SerializeField] private float _runBustSpeed = 1;
    private float _currentRunBustSpeed;
    private Vector3 _velocity;

    [Header("Mobile")] 
    [SerializeField] private FixedJoystick _joystick;
    private Vector3 _moveMobile;
    private bool _isMobileJumpButtonDown = false;
    private bool _isMobileAccelerationButton = false;

   

   private void OnEnable() 
   {
      UIController.OnUseRunBust += SpeedBust;
   }

   private void OnDisable() 
   {
      UIController.OnUseRunBust -= SpeedBust;
        
   }

   private void Start() 
   {
      _currentRunBustSpeed = _runBustSpeed;
      _currentStaminaValue = _staminaValue;    
   }
   private void Update() 
   {
      Move(); 
      ChangeStamina();
   }

   private void Move()
   {
     
     _isGrounded = Physics.CheckSphere(_groundCheck.position,_groundDistance,_groundMask);

     if(_isGrounded && _velocity.y < 0)
     {
        _velocity.y = -2f;
     }

    
    if(DeviceTypes.Instance.CurrentDeviceType == DeviceTypeWEB.Desktop)
     {
       var x = Input.GetAxis("Horizontal");
       var z = Input.GetAxis("Vertical");

      Vector3 move = transform.right * x + transform.forward * z;

      JumpPC();
      _controller.Move(_velocity * Time.deltaTime);

      if(_isGrounded)
      {
         if(Input.GetKey(KeyCode.LeftShift) && _currentStaminaValue > 2f)
         {
            SetMove(move,_acceleration);
           _currentStaminaValue -= 7 * Time.deltaTime *2f;
         }
         else
         {
           SetMove(move,_speed);
            _currentStaminaValue += 7 * Time.deltaTime * 0.9f;
         }
      }     
      
      else  
      {
        SetMove(move,_speedInAir);
      }    

         

     }
     else if(DeviceTypes.Instance.CurrentDeviceType == DeviceTypeWEB.Mobile)
     {
       _moveMobile = new Vector3(_joystick.Horizontal * Time.deltaTime, 0f, _joystick.Vertical * Time.deltaTime); 
     
       JumpMobile();

      _controller.Move(_velocity * Time.deltaTime);

       if(_isGrounded)
      {
         if(_isMobileAccelerationButton == true && _currentStaminaValue > 2f)
         {
            SetMove(_moveMobile,_acceleration * 5f);
           _currentStaminaValue -= 7 * Time.deltaTime *2f;
         }
         else
         {
           SetMove(_moveMobile,_speed * 7f);
            _currentStaminaValue += 7 * Time.deltaTime * 0.9f;
         }   
     }     
    } 
  
   }
  
   private void JumpPC()
   { 
       if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
       {
           _currentStaminaValue -= 7f;
           _velocity.y = Mathf.Sqrt(_jumpHeight * GRAVITY_SCALE * -5f);

       } 
     
      _velocity.y += GRAVITY_SCALE * Time.deltaTime;
   }

   

    private void JumpMobile()
    {
        if(_isGrounded && _isMobileJumpButtonDown == true)
        {
           _currentStaminaValue -= 7f;
           _velocity.y = Mathf.Sqrt(_jumpHeight * GRAVITY_SCALE * -5f);
           _isMobileJumpButtonDown = false;
        }

        _velocity.y += GRAVITY_SCALE * Time.deltaTime;
    }

    public void JumpMobileButton()
    {
       _isMobileJumpButtonDown = true;
    }

    public void EnterMobileAcceleration()
    {
        _isMobileAccelerationButton = true;
    }
    
   
   private void ChangeStamina()
    {
       if(_currentStaminaValue > _staminaValue)
          _currentStaminaValue = _staminaValue;

       if(_currentStaminaValue < 0)
          _currentStaminaValue = 0;
       _staminaBar.fillAmount = _currentStaminaValue / _staminaValue;
    }


   private void SetMove(Vector3 move,float speed)
   {
      _controller.Move(move * _currentRunBustSpeed * speed * Time.deltaTime);
   }

   private IEnumerator SpeedBustCoroutine()
   {
       _currentRunBustSpeed += 0.5f;
       yield return new WaitForSeconds(15f);
       _currentRunBustSpeed = _runBustSpeed;
   }
   private void SpeedBust()
   {
      SpeedBustCoroutine();
   }
}
