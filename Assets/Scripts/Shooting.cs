using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Timer = 0;
    public Camera mainCamera;
    public LayerMask hitLayers = ~0;
    public float shootingForce = 2;
    void Start()
    {
        
    }

    public void FixedUpdate()
    {
        Timer += Time.fixedDeltaTime;
    }
    void Update()
    {
        Vector3 directionToTarget =  PerformRaycast();
        if (Input.GetMouseButtonDown(0) && Timer >2)
        {
            Timer = 0;
            GameObject bulletShot = Instantiate(BulletPrefab, gameObject.transform.position, Quaternion.LookRotation(directionToTarget));
            bulletShot.transform.Rotate(new Vector3(90, 0, 0));
            bulletShot.GetComponent<Rigidbody>().AddForce(bulletShot.transform.up * shootingForce, ForceMode.Impulse);
        }
    }

    public Vector3 PerformRaycast()
    {
        // Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Ray cameraRay = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit cameraHit;
        Vector3 targetPoint;

        if (Physics.Raycast(cameraRay, out cameraHit, Mathf.Infinity, hitLayers))
        {
            targetPoint = cameraHit.point;
        }
        else
        {
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float enter;
            if (groundPlane.Raycast(cameraRay, out enter))
            {
                targetPoint = cameraRay.GetPoint(enter);
            }
            else
            {
                targetPoint = cameraRay.GetPoint(1000f);
            }
        }

        return (targetPoint - gameObject.transform.position).normalized;
    }
}
