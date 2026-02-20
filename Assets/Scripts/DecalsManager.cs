using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DecalsManager : MonoBehaviour
{
    [Header("Ustawienia raycasta")]
    [SerializeField] private float rayLength = 100f;          
    [SerializeField] private LayerMask hitLayers = ~0;    

    public Camera mainCamera;
    public GameObject Decal;
    public List<Material> DecalColors;



    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform; 
    }

    private void Update()
    {
       PerformRaycast();
    }

    private void PerformRaycast()
    {
        if (mainCamera == null || playerTransform == null) return;

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

        Vector3 directionToTarget = (targetPoint - playerTransform.position).normalized;

        RaycastHit playerHit;
        if (Physics.Raycast(playerTransform.position, directionToTarget, out playerHit, rayLength, hitLayers))
        {

           

            Decal.transform.rotation = Quaternion.LookRotation(directionToTarget);

            Decal.SetActive(true);
            string CurrentEnemy = playerHit.collider.tag;
            Decal.transform.position = playerHit.point;
            switch (CurrentEnemy)
            {
                case "GreenEnemy":
                    Decal.GetComponent<DecalProjector>().material = DecalColors[0];
                    break;
                case "RedEnemy":
                    Decal.GetComponent<DecalProjector>().material = DecalColors[1];
                    break;
            }

        }
        else
        {
            Decal.SetActive(false);

            Debug.Log($"Nic nie trafiono w zasiêgu {rayLength}");
        }

             Vector3 rayEnd = playerTransform.position + directionToTarget * rayLength;
            Debug.DrawLine(playerTransform.position, rayEnd, Color.red, 2f);
    }
}
