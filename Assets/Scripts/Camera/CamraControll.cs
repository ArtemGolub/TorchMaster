using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CamraControll : MonoBehaviour
{
    private CinemachineFreeLook _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineFreeLook>();
        _camera.transform.position = new Vector3(-2, 14, -8);
    }

    public void InitCamera(Transform player)
    {
        _camera.LookAt = player;
        _camera.Follow = player;
    }
}
