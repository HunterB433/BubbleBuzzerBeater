using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimming : MonoBehaviour
{
    private Animator animator;
    private const string IS_SWIMMING = "IsSwimming";
    [SerializeField] private PlayerMove player;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake() {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update() {
        animator.SetBool(IS_SWIMMING, player.IsSwimming());
    }
}
