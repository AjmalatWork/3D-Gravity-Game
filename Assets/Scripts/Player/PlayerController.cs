using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void CheckWinCondition()
    {
        if(playerCollisions.noOfCollectibles == 0)
        {
            timerText.text = "You Win";
            timerText.color = Color.yellow;
            Time.timeScale = 0f;
        }
    }
}