using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public Transform playerHoloPosition;
    public GameObject playerHolo;

    private PlayerMovement playerMovement;
    private bool enterPressed = false;  
    private bool arrowPressed = false;
    private Vector3 gravityDir;
    private float rotateAngleX;
    private float rotateAngleY;
    private float rotateAngleZ;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void HandleGravity()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enterPressed = true;
        }

        // Arrow Key Pressed
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {            
            PreviewHolo(0f);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            PreviewHolo(180f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviewHolo(270f);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PreviewHolo(90f);
        }

        // Arrow Key Up
        if (Input.GetKeyUp(KeyCode.UpArrow) || 
            Input.GetKeyUp(KeyCode.DownArrow) || 
            Input.GetKeyUp(KeyCode.LeftArrow) || 
            Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerHolo.gameObject.SetActive(false);
            arrowPressed = false;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            enterPressed = false;
        }

        if (enterPressed && arrowPressed)
        {
            playerMovement.gravityDirection = gravityDir;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, -gravityDir);
        }
    }

    private void PreviewHolo(float angle)
    {
        GetPreviewAngle(angle);
        arrowPressed = true;
        playerHolo.transform.position = playerHoloPosition.position;
        playerHolo.transform.rotation = Quaternion.Euler(rotateAngleX, rotateAngleY, rotateAngleZ);
        playerHolo.gameObject.SetActive(true);
    }

    void GetPreviewAngle(float angle)
    {
        GravityDirection gravityDirection = playerMovement.GetGravityDirection(playerMovement.gravityDirection);

        switch (gravityDirection)
        {
            case GravityDirection.Up:
                rotateAngleX = 270f;
                rotateAngleY = angle;
                rotateAngleZ = 0f;
                switch(angle)
                {
                    case 0f:
                        gravityDir = Vector3.back;
                        break;
                    case 90f:
                        gravityDir = Vector3.left;
                        break;
                    case 180f:
                        gravityDir = Vector3.forward;
                        break;
                    case 270f:
                        gravityDir = Vector3.right;
                        break;
                    default:
                        break;
                }
                break;
            case GravityDirection.Down:
                rotateAngleX = 90f;
                rotateAngleY = angle;
                rotateAngleZ = 0f;
                switch (angle)
                {
                    case 0f:
                        gravityDir = Vector3.forward;
                        break;
                    case 90f:
                        gravityDir = Vector3.right;
                        break;
                    case 180f:
                        gravityDir = Vector3.back;
                        break;
                    case 270f:
                        gravityDir = Vector3.left;
                        break;
                    default:
                        break;
                }
                break;
            case GravityDirection.Left:
                rotateAngleX = 0f;
                rotateAngleY = 270f;
                rotateAngleZ = angle;
                switch (angle)
                {
                    case 0f:
                        gravityDir = Vector3.down;
                        break;
                    case 90f:
                        gravityDir = Vector3.back;
                        break;
                    case 180f:
                        gravityDir = Vector3.up;
                        break;
                    case 270f:
                        gravityDir = Vector3.forward;
                        break;
                    default:
                        break;
                }
                break;
            case GravityDirection.Right:
                rotateAngleX = 0f;
                rotateAngleY = 90f;
                rotateAngleZ = angle;
                switch (angle)
                {
                    case 0f:
                        gravityDir = Vector3.up;
                        break;
                    case 90f:
                        gravityDir = Vector3.forward;
                        break;
                    case 180f:
                        gravityDir = Vector3.down;
                        break;
                    case 270f:
                        gravityDir = Vector3.back;
                        break;
                    default:
                        break;
                }
                break;
            case GravityDirection.Forward:
                rotateAngleX = 0f;
                rotateAngleY = 0f;
                rotateAngleZ = angle;
                switch (angle)
                {
                    case 0f:
                        gravityDir = Vector3.up;
                        break;
                    case 90f:
                        gravityDir = Vector3.right;
                        break;
                    case 180f:
                        gravityDir = Vector3.down;
                        break;
                    case 270f:
                        gravityDir = Vector3.left;
                        break;
                    default:
                        break;
                }
                break;
            case GravityDirection.Backward:
                rotateAngleX = 180f;
                rotateAngleY = 0f;
                rotateAngleZ = angle;
                switch (angle)
                {
                    case 0f:
                        gravityDir = Vector3.down;
                        break;
                    case 90f:
                        gravityDir = Vector3.left;
                        break;
                    case 180f:
                        gravityDir = Vector3.up;
                        break;
                    case 270f:
                        gravityDir = Vector3.right;
                        break;
                    default:
                        break;
                }
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }
    }
}

