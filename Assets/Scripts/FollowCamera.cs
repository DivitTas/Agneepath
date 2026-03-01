using Unity.Cinemachine;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private CinemachineCamera cinemachineFollow;

    private void Awake()
    {
        cinemachineFollow = GetComponent<CinemachineCamera>();
    }

    private void Start()
    {
        AssignPlayer();
    }

    public void AssignPlayer()
    {
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
        {
            cinemachineFollow.Follow = player.transform;
        }
    }

    public void AssignPlayer(GameObject playerObj)
    {
        if (playerObj != null)
        {
            cinemachineFollow.Follow = playerObj.transform;
        }
    }
}