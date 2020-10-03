using UnityEngine;

public class BackgroundResizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        ResizeSpriteToScreen();
    }
    
    private void ResizeSpriteToScreen() 
    {
        transform.localScale = Vector3.one;
 
        float width = spriteRenderer.sprite.bounds.size.x;
        float height = spriteRenderer.sprite.bounds.size.y;
        
        float worldScreenHeight = Camera.main.orthographicSize*2f;
        float worldScreenWidth = worldScreenHeight/Screen.height*Screen.width;
 
        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;

        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;
    }
}
