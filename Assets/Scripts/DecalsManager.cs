using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class DecalsManager : MonoBehaviour
{
    [Header("Ustawienia raycasta")]
    [SerializeField] private float rayLength = 50f;          
    [SerializeField] private LayerMask hitLayers = ~0;
    private const string RED_ENEMY_STRING = "RedEnemy";
    private const string GREEN_ENEMY_STRING = "GreenEnemy";


    public Camera mainCamera;
    public GameObject Decal;
    public GameObject Neons;
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

        //Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
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

        Vector3 directionToTarget = (targetPoint - playerTransform.position).normalized;
        Neons.transform.rotation = Quaternion.LookRotation(directionToTarget);
        RaycastHit playerHit;
        if (Physics.Raycast(playerTransform.position, directionToTarget, out playerHit, rayLength, hitLayers))
        {

           

            Decal.transform.rotation = Quaternion.LookRotation(directionToTarget);
           
            
            string CurrentEnemy = playerHit.collider.tag;
            string CurrentLight = Dependencies.Instance.GetDependancy<LightManager>().CurrentLight.ToString();
            Decal.transform.position = playerHit.point;
            switch (CurrentEnemy)
            {
                case GREEN_ENEMY_STRING:
                    if (CurrentLight == "GreenNeon")
                    {
                        Decal.SetActive(true);
                    }
                        Decal.GetComponent<DecalProjector>().material = DecalColors[0];
                    break;
                case RED_ENEMY_STRING:
                    if (CurrentLight == "RedNeon")
                    {
                        Decal.SetActive(true);


                    }

                    Decal.GetComponent<DecalProjector>().material = DecalColors[1];
                    break;
                case "NonEnemy":
                    Debug.Log("non enemy hit");
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
