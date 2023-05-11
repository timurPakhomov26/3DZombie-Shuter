using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchLook : MonoBehaviour
{

/*private Touch touch;
private Vector2 oldTouchPosition;
private Vector2 NewTouchPosition; 
[SerializeField]
private float keepRotateSpeed = 10f;

private void Update()
{
     RotateThings(); 
}
private void RotateThings()
{
  if (Input.touchCount > 0)
    {
        touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            oldTouchPosition = touch.position;
        }

        else if (touch.phase == TouchPhase.Moved)
        {
            NewTouchPosition = touch.position;
        }

        Vector2 rotDirection = oldTouchPosition - NewTouchPosition;
        Debug.Log(rotDirection);
        if (rotDirection.x < 0 )
        {
            RotateRight();
        }

        else if (rotDirection.x > 0 )
        {
            RotateLeft();
        }
    }
}

void RotateLeft()
    {
        transform.rotation = Quaternion.Euler(0f, -1.5f * keepRotateSpeed, 0) * transform.rotation;
    }

void RotateRight()
{
    transform.rotation = Quaternion.Euler(0f, 1.5f * keepRotateSpeed, 0) * transform.rotation;
}

}*/
 /* private Vector3 _startPos;
  [SerializeField] private float _speed = 5f;
  [SerializeField] private Transform _player;
    private void Update()
    {
        
          foreach( Touch touch in Input.touches)
          {
             if(touch.phase == TouchPhase.Moved)
             {
               _player.transform.Rotate(touch.deltaPosition.y * 0.4f,-touch.deltaPosition.x * 0.4f,
                            0,Space.World);

             }
          }
        
        }
    }*/








 



    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private Transform _myCamera;
    [SerializeField] private Transform _player;
    [SerializeField] private RectTransform _joystick;
    Vector3 firstPoint;
    Vector3 secondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;


    private void Start() 
    {
       yAngle = transform.localRotation.eulerAngles.y;  
    }
    private void Update() 
    {
        
      foreach(Touch touch in Input.touches)
      {

        if(touch.position.x != _joystick.transform.position.x && touch.position.y != _joystick.transform.position.y)
       {

         if(touch.position.x > Screen.width / 4  && touch.phase == TouchPhase.Began) 
         {
            firstPoint = touch.position;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;
         }
         if(touch.position.x > Screen.width / 4  && touch.phase == TouchPhase.Moved) 
         {
            secondPoint = touch.position;
             xAngle = xAngleTemp -(secondPoint.y - firstPoint.y) * 90 / Screen.height ;
             yAngle = yAngleTemp +  (secondPoint.x - firstPoint.x) * 180 / Screen.width ;
            xAngle = Mathf.Clamp(xAngle,-80,80);
            transform.localRotation = Quaternion.Euler(xAngle,0,0);  
            _player.transform.rotation = Quaternion.Euler(0,yAngle,0);  
         }

       }
      }
    }
}

