using UnityEngine;

public class GearItem : MonoBehaviour
{
    [SerializeField] private GearType gearType;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private float gearRotatinSpeed = 80f;
    
    private bool isDragging;
    private Axis cell = null;
    private bool isTriggered = false;
    
    public GearType GetGearType => gearType;
    public float GetGearSpeed => gearRotatinSpeed;
    
    /// <summary>
    /// Метод для получения оси, в которой находится итем
    /// ДА, GetComponent - плохо, однако так как каждая шестерня
    /// может находится только на одной оси (как бы вся игра об этом),
    /// то считаю, что в данном случае такое использование вполне уместно
    /// </summary>
    public Axis GetCell => GetComponentInParent<Axis>();

    /// <summary>
    /// При нажатии на шестерню ставим индикатор isDragging в true
    /// и проигрываем звук захвата шестерни
    /// </summary>
    public void OnMouseDown()
    {
        isDragging = true;
        soundManager.PlaySound(Sounds.GearClicked);
    }

    public void OnMouseDrag()
    {
        if (isDragging) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
            Debug.Log("Current Gear Transform Position: " + transform.localPosition);
        }
    }
    
    /// <summary>
    /// Когда отпустили левую кнопку мыши ставим индикатор isDragging в false
    /// и проигрываем звук освобождения шестерни от нашего цепкого указателя
    /// а так же, если мы отпустили шестерню недалеко от другой, то у ячейки, в которой
    /// находится другая шестерня, вызывается метод Swap
    /// </summary>
    public void OnMouseUp()
    {
        isDragging = false;
        soundManager.PlaySound(Sounds.GearReleased);
        transform.localPosition = Vector3.zero;
        if(isTriggered)
            cell.Swap(this);
            
    }
    
    /// <summary>
    /// Проверяем есть есть ли пересечения у данной шестерни с другой осью
    /// во время перетаскивания
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {   
        cell =  other.gameObject.GetComponent<Axis>();
        isTriggered = true;
    }

    /// <summary>
    /// Выставляем шестерню в дефолтную позицию относительно
    /// родительского трансформа
    /// </summary>
    /// <param name="parentTransform"></param>
    public void SetDefaults(Transform parentTransform)
    {
        transform.parent = parentTransform;
        transform.localPosition = Vector3.zero;
    }
}
