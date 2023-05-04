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
   [SerializeField]private bool _isGrounded;
    private float _currentStaminaValue;
   
   
   private Vector3 _velocity;
   

   private void Start() 
   {
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


      var x = Input.GetAxis("Horizontal");
      var z = Input.GetAxis("Vertical");

      if((x != 0 || z != 0) && _isGrounded == true)
          OnPlayerMove?.Invoke(true);
      else
          OnPlayerMove?.Invoke(false);
         
            
         
      Vector3 move = transform.right * x + transform.forward * z;

      if(_isGrounded)
      {
         if(Input.GetKey(KeyCode.LeftShift) && _currentStaminaValue > 1f)
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
       
      Jump();

      _controller.Move(_velocity * Time.deltaTime);
   }

   private void Jump()
   { 
       if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
       {
           _currentStaminaValue -= 7f;
           _velocity.y = Mathf.Sqrt(_jumpHeight * GRAVITY_SCALE * -5f);

       } 
     
      _velocity.y += GRAVITY_SCALE * Time.deltaTime;
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
      _controller.Move(move * speed * Time.deltaTime);
   }
}
