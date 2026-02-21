using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Player _player;


    private void Start()
    {
        _player = Dependencies.Instance.GetDependancy<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GreenEnemy"))
        {
            Destroy(other.gameObject);
            _player.RefillNeon("Green", 0.005f);
            Destroy(gameObject);
        }

        if (other.CompareTag("RedEnemy"))
        {
            Destroy(other.gameObject);
            _player.RefillNeon("Red", 0.005f);
            Destroy(gameObject);
        }

        if (other.CompareTag("NonEnemy"))
        {
            Destroy(other.gameObject);
            _player.LoseLife();
            Destroy(gameObject);
        }

        else { Destroy(gameObject); }
    }
}
