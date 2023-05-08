using UnityEngine;

[CreateAssetMenu(menuName  = "Weapon/New weapon")]
public class WeaponInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private WeaponType _type;
    public WeaponType Type => _type;
    [SerializeField] private int _damage;
    public int Damage => _damage;
    [SerializeField] private int _price;
    public int Price => _price;
    [SerializeField] private float _rateOfFire;
    public float RateOfFire => _rateOfFire;
    
}
