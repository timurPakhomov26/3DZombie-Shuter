using System.Collections;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{

     private void OnEnable() 
    {
       Weapon.OnShot += OffEffect; 
    }

    private void OnDisable() 
    {
       Weapon.OnShot -= OffEffect; 
        
    }
    public void OffEffect()
    {
       StartCoroutine(Off());
    } 

    private IEnumerator Off()
    {
       yield return new WaitForSeconds(0.05f);
       gameObject.SetActive(false);
    }
}
