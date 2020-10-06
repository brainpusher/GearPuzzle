using System.Collections;
using UnityEngine;

public class GamePanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject targetPanelToSwitchState;
    [SerializeField] private BoxCollider2D boxColliderToSwitch;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private SoundManager soundManager;
    
    public void OnMouseDown()
    {
        SwitchPanel();
    }

    public void SwitchPanel()
    {
        soundManager.PlaySound(targetPanelToSwitchState.activeSelf ? Sounds.CloseMiniGame : Sounds.OpenMiniGame);
        StartCoroutine(AppearingRoutine());
    }
    
    /// <summary>
    /// Корутина, в которой проигрывается анимация партиклей для закрытия и открытия панели с мини игрой
    /// </summary>
    /// <returns></returns>
    private IEnumerator AppearingRoutine()
    {
        particleSystem.Play();

        StartCoroutine(ScaleRoutine(1f, !targetPanelToSwitchState.activeSelf));
        yield return  new WaitForSeconds(particleSystem.main.duration);
        boxColliderToSwitch.enabled = !targetPanelToSwitchState.activeSelf;
    }
    
    /// <summary>
    /// Корутина, в которой проигрывается анимация скейла окна, с последующим его выключением
    /// </summary>
    /// <param name="time"></param>
    /// <param name="scaleUp"></param>
    /// <returns></returns>
    private IEnumerator ScaleRoutine(float time, bool scaleUp)
    {
        Vector3 originalScale = targetPanelToSwitchState.transform.localScale;
        Vector3 destinationScale = scaleUp ? Vector3.one :  new Vector3(0.8f,0.8f,0.8f);
        if(scaleUp)
            targetPanelToSwitchState.SetActive(!targetPanelToSwitchState.activeSelf);
        
        float currentTime = 0.0f;
         
        do
        {
            targetPanelToSwitchState.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);
       
        if(!scaleUp)
            targetPanelToSwitchState.SetActive(!targetPanelToSwitchState.activeSelf);
    }
    
}
