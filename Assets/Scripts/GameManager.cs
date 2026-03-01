using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    GameObject  player;
    private void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

        FollowCamera cam = FindAnyObjectByType<FollowCamera>();
        if (cam != null)
            cam.AssignPlayer(player);
    }
    private void Update()
    {
        if(player.transform.position.y<-50){
            Die();
        }
    }

    void Die()
    {
        Destroy(player);
        SpawnPlayer();
    }
}