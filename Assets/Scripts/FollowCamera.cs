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

    void AssignPlayer()
    {
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
        {
            cinemachineFollow.Follow = player.transform;
        }
    }
}