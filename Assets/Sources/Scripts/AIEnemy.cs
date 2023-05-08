using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;


public enum EnemyStates
{
   ChaseHome,
   ChasePlayer,
   AttackPlayer,
   AttackHome
}

public class AIEnemy : MonoBehaviour, IDamageFromWeapon
{
    public static Action OnZombieDied;
   [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _playerAttackField;
    [SerializeField] private float _homeAttackField;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _health;
    [SerializeField] private float _timeToAttack;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private Animator _animator;
    private NavMeshAgent _aiAgent;
    private Transform _player;
    private Transform _home;
    private EnemyStates _currentState;
    private float _time; 
    private float _distanceToPlayer;
    private float _distanceToHome;


   private void Start()
    {
        _aiAgent = gameObject.GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerMovement>().transform;
        _home =   FindObjectOfType<Home>().transform;
    }
    
   private void FixedUpdate()
    {
        SetState();
        SetBehavior();      
    }

    public void TakeDamage(float value)
    {
        _health -= value;

        if(_health <= 0)
        {
           _health = 0;
           Die();
        }
    }

    private void Die()
    {
        gameObject.layer = LayerMask.NameToLayer("DiedZombie");
        _aiAgent.speed = 0;
        _animator.SetTrigger("Die");
        this.enabled = false;
        
        OnZombieDied?.Invoke();
        StartCoroutine(Dissapear());
       //gameObject.SetActive(false);
    }

    private void SetState()
    {
       _distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
       _distanceToHome = Vector3.Distance(_home.transform.position, transform.position);

       if(_distanceToPlayer > (_distanceToHome + 10f))   
       {
         if(_distanceToHome > _homeAttackField)
         {
           _currentState = EnemyStates.ChaseHome;               
         }

         else
         {
           _currentState = EnemyStates.AttackHome;
         }
        
       }  
       else if(_distanceToPlayer <= (_distanceToHome + 10f))
       { 
          if(_distanceToPlayer > _playerAttackField)
          {
              _currentState = EnemyStates.ChasePlayer;                 
          }

          else
          {
              _currentState = EnemyStates.AttackPlayer;
          }
       
       }
      
    }

    private void SetBehavior()
    {
        if(_currentState == EnemyStates.ChaseHome)    
             ChaseHome();

         else if(_currentState == EnemyStates.AttackHome)
            AttackHome();

        else if(_currentState == EnemyStates.AttackPlayer)
            AttackPlayer();

        else 
            ChasePlayer();     

    }

    private void ChasePlayer()
    {
        Chase(_player);
    }

    private void ChaseHome()
    {
         Chase(_home);
    }

    private void AttackPlayer()
    {
        _aiAgent.speed = 0;
        Debug.Log("Attack");
        _time += Time.deltaTime;
        
        if(_time >= _timeToAttack)
        {
           _animator.SetTrigger("Attack"); 
          _player.GetComponent<PlayerUI>().TakeDamage(_damage);
          _time = 0;
        }

    }

    private void AttackHome()
    {
        _aiAgent.speed = 0;
        _aiAgent.isStopped = true;
        _time += Time.deltaTime;
        
        if(_time >= _timeToAttack)
        {
            _animator.SetTrigger("Attack");
          _home.GetComponent<Home>().TakeDamage(_damage);
          _time = 0;
        }
    }

    private void Chase(Transform target)
    {
        _aiAgent.speed = _moveSpeed;
        _animator.SetTrigger("Run");
        _aiAgent.SetDestination(target.transform.position);
    }

    private IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(3f);
       gameObject.SetActive(false);
       transform.position = Vector3.zero;
    }

    public void Hit()
    {
       Debug.Log("Hit");
    }


}
