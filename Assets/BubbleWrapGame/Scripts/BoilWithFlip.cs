using UnityEngine;
using System.Collections.Generic;

public class BoilWithFlip : MonoBehaviour
{
    public List<Sprite> spriteList1;  // First list of sprites
    public List<Sprite> spriteList2;  // Second list of sprites

    public float changeSpeed = 1f;  // Speed at which the sprite changes
    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;

    private int currentListIndex = 0;  // 0 for list1, 1 for list2
    private List<Sprite> currentList;  // Reference to the current list

    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCam = Camera.main;

        // Set the initial list
        SetCurrentList();
    }

    // Update is called once per frame
    void Update()
    {
        // Cycle through the sprites based on the current list
        float timeElapsed = Time.time % changeSpeed;

        if (timeElapsed < Time.deltaTime)
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % currentList.Count;
            spriteRenderer.sprite = currentList[currentSpriteIndex];
        }

        // Check for mouse click to toggle sprite lists
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Toggle the sprite list
                ToggleSpriteList();
            }
        }
    }

    // Set the current sprite list based on the index
    void SetCurrentList()
    {
        if (currentListIndex == 0)
        {
            currentList = spriteList1;
        }
        else
        {
            currentList = spriteList2;
        }
    }

    // Toggle between the two lists
    void ToggleSpriteList()
    {
        currentListIndex = (currentListIndex + 1) % 2;  // Switch between 0 and 1
        SetCurrentList();  // Update the current list reference
        currentSpriteIndex = 0;  // Reset sprite index when switching lists
        spriteRenderer.sprite = currentList[currentSpriteIndex];  // Update sprite
    }
}
