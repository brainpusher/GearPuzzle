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
    /// Корутина, в которой проигрывается анимация закрытия и открытия панели с мини игрой
    /// с последующим выключением и включением окна миниигры
    /// </summary>
    /// <returns></returns>
    private IEnumerator AppearingRoutine()
    {
        particleSystem.Play();
        yield return  new WaitForSeconds(particleSystem.main.duration);
        targetPanelToSwitchState.SetActive(!targetPanelToSwitchState.activeSelf);
        boxColliderToSwitch.enabled = !targetPanelToSwitchState.activeSelf;
    }
}
