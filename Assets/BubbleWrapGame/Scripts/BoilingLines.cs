using UnityEngine;

public class BoilingLines : MonoBehaviour
{
    public Sprite[] sprites;

    public float changeSpeed = 1f;

    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[currentSpriteIndex];

    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = Time.time % changeSpeed;

        if (timeElapsed < Time.deltaTime)
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
    }
}
