using UnityEngine;

[CreateAssetMenu(fileName = "new Ability", menuName = "Ability Data / new Item")]
public class DataAbility : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private Sprite _icon;

    public string Title => _title;
    public Sprite Icon => _icon;

}
