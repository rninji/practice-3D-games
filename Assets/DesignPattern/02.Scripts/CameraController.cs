using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 delta = new Vector3(0, 6.0f, -5.0f);

    void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player.Instance.transform.position, delta, out hit, delta.magnitude,
                LayerMask.GetMask("Block")))
        {
            // 벽과 충돌 시 카메라 줌인
            float dist = (hit.point - Player.Instance.transform.position).magnitude * 0.8f;
            transform.position = Player.Instance.transform.position + Vector3.up + delta.normalized * dist;
        }
        else
        {
            // 플레이어 따라 이동
            transform.position = Player.Instance.transform.position + delta;
            transform.LookAt(Player.Instance.transform);
        }
    }
    
    
}