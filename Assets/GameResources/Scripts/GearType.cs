using UnityEngine;

[CreateAssetMenu(fileName = "New Gear Type", menuName = "GearType")]
public class GearType : ScriptableObject
{
    [SerializeField] private string typeName;
    
    /// <summary>
    /// Возвращает название типа
    /// </summary>
    public string TypeName => typeName;
}
