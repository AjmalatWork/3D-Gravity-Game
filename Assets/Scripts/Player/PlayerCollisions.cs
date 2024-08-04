using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public int noOfCollectibles = 5;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            HoloCube currentHoloCube = collision.gameObject.GetComponent<HoloCube>();
            if (currentHoloCube.gameObject.activeSelf)
            {
                noOfCollectibles--;
                currentHoloCube.OnCollect();
            }
        }
    }
}
