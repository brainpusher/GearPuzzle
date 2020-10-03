using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private List<Axis> axes;
    [SerializeField] private float winAnimationDuration = 2f;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GamePanelSwitcher gamePanelSwitcher;
    
    private int gameWeight = 0;
    
    private void Start()
    {
        foreach (Axis axis in axes)
        {
            if(axis.IsGearOnRightAxis()) 
                gameWeight ++;
        }
    }

    private void OnEnable()
    {
        foreach (Axis axis in axes)
        {
            axis.GearChanged += ChangeGameWeight;
        }
    }

    private void OnDisable()
    {
        foreach (Axis axis in axes)
        {
            axis.GearChanged -= ChangeGameWeight;
        }
    }

    /// <summary>
    /// В данном методе идет проверка на то, соответсвуют ли шестерни
    /// своим осям, и если да, то запускаются победные действия
    /// </summary>
    private void ChangeGameWeight()
    { 
        gameWeight = 0; 
        foreach (Axis axis in axes) 
        { 
            if(axis.IsGearOnRightAxis()) 
                gameWeight ++; 
        } 
        if (gameWeight == axes.Count)
        {
            DisplayWin();
        }
    }

    /// <summary>
    /// Метод, который активирует победные действия
    /// </summary>
    private void DisplayWin()
    {
        PlayWinAnimation();
        PlayWinSound();
    }

    /// <summary>
    /// Проигрывает победный звук
    /// </summary>
    private void PlayWinSound()
    {
        soundManager.PlaySound(Sounds.WinSound);
    }
    
    /// <summary>
    /// Запускает победную анимацию
    /// ВНИМАНИЕ!!! В данном случае важно, в каком пордяке поданы аргументы
    /// в список axes из инспектора, так как используется метод, при котором
    /// направление вращения шестерни определяется исходя из порядка аргументов.
    /// То есть две соседние шестерни должны вращаться в разных направлениях.
    /// </summary>
    private void PlayWinAnimation()
    {
        int sign = 1;
        int i = 0;
        foreach (Axis axis in axes)
        {
            sign *= (i % 2 == 0) ? 1 : -1;
            StartCoroutine(GearRotationRoutine(axis.Gear.transform, winAnimationDuration,sign));
            i++;
        }
    }
    
    /// <summary>
    /// Корутина, в которой осуществляется анимацию прокручивания шестерни
    /// </summary>
    /// <param name="gearTransform"></param>
    /// <param name="duration"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    private  IEnumerator GearRotationRoutine(Transform gearTransform,float duration, int direction)
    {
        float startRotation = gearTransform.eulerAngles.z;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while ( t  < duration )
        {
            t += Time.deltaTime;
            float zRotation = direction * Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
            gearTransform.eulerAngles = new Vector3(gearTransform.eulerAngles.x,  gearTransform.eulerAngles.y, 
                zRotation);
            yield return null;
            gearTransform.eulerAngles = Vector3.zero;
        }
        gamePanelSwitcher.SwitchPanel();
    }
}
