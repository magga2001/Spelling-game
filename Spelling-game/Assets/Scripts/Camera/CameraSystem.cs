using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;

    private GameObject target;

    public void AdjustFightingCamera(Transform player, Transform enemy)
    {
        //Getting the middle position between player and current enemy for the camera to adjust to
        var pos = (player.position + enemy.position) / 2;
        target = Instantiate(new GameObject("target"), Vector3.zero, Quaternion.identity);
        target.transform.position = pos;
        cam.Follow = target.transform;

        cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0;
        cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0;
    }

    public void AdjustWalkingCamera(Transform player)
    {
        cam.Follow = player;
        //To adjust the camera to have some area for the player to move before moving camera all the time
        cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0.15f;
        cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0.1f;
        Destroy(target);
    }
}
