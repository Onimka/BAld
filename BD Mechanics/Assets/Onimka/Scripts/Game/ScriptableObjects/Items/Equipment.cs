using UnityEngine;

[CreateAssetMenu(fileName = "new Equipment", menuName = "Inventory Item Data / Equipment Item")]
public class Equipment : Item
{

    public EquipmentType TypeEquipment => _typeEquipment;

    [SerializeField] private EquipmentType _typeEquipment;


    public void Use()
    {
        base.Use();
    }

}

public enum EquipmentType
{
    Head, Chest, Legs, Weapon, Shield, Feet
}

