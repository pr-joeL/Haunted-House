using UnityEngine;
using System.Collections; // Required for Coroutines
using UnityEngine.UI; // Required for UI elements

public class PlayerController : MonoBehaviour
{
    public float baseSpeed = 1f;
    public float boostSpeedMultiplier = 3f;
    public float boostDuration = 2f;
    public float boostCooldown = 5f;
    public Image boostIcon; // Assign your UI Image for the boost icon in the Inspector
    public Sprite boostReadySprite; // Sprite for when boost is ready
    public Sprite boostActiveSprite; // Sprite for when boost is active
    public Sprite boostCooldownSprite; // Sprite for when boost is on cooldown

    private float currentSpeed;
    private bool canBoost = true;
    private bool isBoosting = false;

    void Start()
    {
        currentSpeed = baseSpeed;
        UpdateBoostIcon(boostReadySprite); // Set initial icon state
    }

    void Update()
    {
        // Player Movement (assuming basic movement)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * currentSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Speed Boost Logic
        if (Input.GetKey(KeyCode.LeftShift) && canBoost && !isBoosting)
        {
            StartCoroutine(ActivateSpeedBoost());
        }
    }

    IEnumerator ActivateSpeedBoost()
    {
        isBoosting = true;
        canBoost = false;
        currentSpeed = baseSpeed * boostSpeedMultiplier;
        UpdateBoostIcon(boostActiveSprite);

        yield return new WaitForSeconds(boostDuration);

        isBoosting = false;
        currentSpeed = baseSpeed;
        UpdateBoostIcon(boostCooldownSprite);

        yield return new WaitForSeconds(boostCooldown);

        canBoost = true;
        UpdateBoostIcon(boostReadySprite);
    }

    void UpdateBoostIcon(Sprite newSprite)
    {
        if (boostIcon != null)
        {
            boostIcon.sprite = newSprite;
        }
    }
}
