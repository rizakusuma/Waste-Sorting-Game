using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public TrashType trashType;
    public float fallSpeed;
    public float rotateSpeed;

    [Range(0, 10)] public float minFallSpeed;
    [Range(0, 10)] public float maxFallSpeed;

    [Range(0, 180)] public float minRotateSpeed;
    [Range(0, 180)] public float maxRotateSpeed;

    private Rigidbody2D rb2;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        RandomizeProperties();
    }

    private void RandomizeProperties()
    {
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);

        int leftRightMult = Random.value >= 0.5 ? 1 : -1;
        rotateSpeed = leftRightMult * Random.Range(minRotateSpeed, maxRotateSpeed);

        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void Update()
    {
        FallController();
        RotateController();
    }

    private void FallController()
    {
        if (rb2)
        {
            rb2.velocity = new Vector2(0, -fallSpeed);
        }
    }

    private void RotateController()
    {
        if (rb2)
        {
            rb2.angularVelocity = rotateSpeed;
        }
    }
}
