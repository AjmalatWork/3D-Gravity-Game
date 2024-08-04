using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerGravity playerGravity;
    private PlayerCollisions playerCollisions;
    [SerializeField] TextMeshProUGUI timerText;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        playerGravity = GetComponent<PlayerGravity>();
        playerCollisions = GetComponent<PlayerCollisions>();
    }

    void Update()
    {
        HandlePlayerInput();

        CheckWinCondition();
    }

    void HandlePlayerInput()
    {
        movement.HandleMovement();
        playerGravity.HandleGravity();
    }

    void CheckWinCondition()
    {
        if(playerCollisions.noOfCollectibles == 0)
        {
            timerText.text = "You Win";
            timerText.color = Color.yellow;
        }
    }
}