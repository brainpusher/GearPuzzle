using System;
using UnityEngine;

public class Axis : MonoBehaviour
{
    public event  Action GearChanged = delegate {  };
    
    [SerializeField] private GearType gearType;
    [SerializeField] private GearItem gearItem;

    private GearItem defaultGearItem = null;
    
    /// <summary>
    /// Вызываем метод Swap, который меняет местами предметы в ячейках
    /// </summary>
    /// <param name="item"></param>
    public void Swap(GearItem item)
    {
        SwapItems(this,item.GetCell);
    }

    private void Start()
    {
        if (gearItem != null)
            gearItem.SetDefaults(transform);
    }
    
    /// <summary>
    /// На OnEnable происходит сохранение позиции шестернки
    /// и при повторном открытии окна позиция ставится в изначальную
    /// </summary>
    private void OnEnable()
    {
        if (defaultGearItem == null)
        {
            defaultGearItem = gearItem;
        }
        else
        {
            gearItem = defaultGearItem;
            gearItem.SetDefaults(transform);
        }
    }

    /// <summary>
    /// Возвращает шестерню, которая находится на текущей оси
    /// </summary>
    public GearItem Gear => gearItem;
    
    /// <summary>
    /// Обновляем шестеренку на новую для текущей оси
    /// </summary>
    private void UpdateItem()
    {
        gearItem = GetComponentInChildren<GearItem>();
        GearChanged?.Invoke();
    }
    
    /// <summary>
    /// Проверка на соответствие типа шестерни типу оси
    /// Возвращает true , если шестерня установлена на правильной оси
    /// </summary>
    /// <returns></returns>
    public bool IsGearOnRightAxis()
    {
        return gearItem.GetGearType.TypeName.Equals(gearType.TypeName);
    }
    
    /// <summary>
    /// Меняем шестеренки местами между двумя осями
    /// </summary>
    /// <param name="firstAxis"></param>
    /// <param name="secondAxis"></param>
    private void SwapItems(Axis firstAxis, Axis secondAxis)
    {
        if ((firstAxis != null) && (secondAxis != null))
        {
            GearItem firstItem = firstAxis.Gear;
            GearItem secondItem = secondAxis.Gear;

            if (firstItem != null)
            {
                firstItem.transform.SetParent(secondAxis.transform, false);
                firstItem.transform.localPosition = Vector3.zero;
            }
            if (secondItem != null)
            {
                secondItem.transform.SetParent(firstAxis.transform, false);
                secondItem.transform.localPosition = Vector3.zero;
            }
            firstAxis.UpdateItem();
            secondAxis.UpdateItem();
        }
    }
}
