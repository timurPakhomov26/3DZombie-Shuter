using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T Prefab{get;}
    public bool AutoExpand{get;set;}
    public Transform Container{get;}
    private List<T> _pool;

    public PoolMono(T prefab,int count)
    {
        Prefab = prefab;
        Container = null;
    } 
    public PoolMono(T prefab,int count,Transform container)
    {
       Prefab = prefab;
       Container = container;

       CreatePool(count);
    }

    private void CreatePool(int count)
    {
       _pool = new List<T>();

       for(int i=0;i<count;i++)
       {
           CreateObject();
       }
    }

    private T CreateObject(bool isActiveByDefolt = false)
    {
       var creatingObject = Object.Instantiate(Prefab,Container);
       creatingObject.gameObject.SetActive(isActiveByDefolt);
       _pool.Add(creatingObject);
       return creatingObject;
    }

    public bool HasFreeElement(out T element)
    {
      foreach(var mono in _pool)
      {
        if(mono.gameObject.activeInHierarchy == false)
        {
           element = mono;
           mono.gameObject.SetActive(true);
           return true;
        }
      }
      element = null;
      return false;
    }

    public T GetFreeElement()
    {
       if(HasFreeElement(out var element))     
          return element;
       
       if(AutoExpand == true)     
          return CreateObject(true);
       
       throw new System.Exception($"There is not element in pool of type{typeof(T)}");
    }
}

