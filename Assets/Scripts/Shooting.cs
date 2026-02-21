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
    // Update is called once per frame
    void Update()
    {
        Vector3 directionToTarget =  PerformRaycast();
        if (Input.GetMouseButtonDown(0) && Timer >2)
        {
            Timer = 0;
            GameObject bulletShot = Instantiate(BulletPrefab, gameObject.transform.position, Quaternion.LookRotation(directionToTarget));
            bulletShot.GetComponent<Rigidbody>().AddForce(bulletShot.transform.forward * shootingForce, ForceMode.Impulse);
        }
    }

    public Vector3 PerformRaycast()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
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
