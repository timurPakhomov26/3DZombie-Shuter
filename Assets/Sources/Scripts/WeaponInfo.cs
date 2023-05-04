using UnityEngine;

[CreateAssetMenu(menuName  = "Weapon/New weapon")]
public class WeaponInfo : ScriptableObject
{
    [SerializeField] private string _name;
}
