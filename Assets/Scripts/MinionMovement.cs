using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    public float speed = 3f;
    private PlayerMovement player;
    private Transform playerTransform;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        playerTransform = player.gameObject.transform;
        transform.LookAt(playerTransform);
    }
    void Update()
    {
        if (player != null)
        {
            

            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }
}