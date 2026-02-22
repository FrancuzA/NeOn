using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    public Material sadFaceMaterial;
    public Material normalFaceMaterial;
    private LightManager _lightManager;
    private Material currentMaterial;

    public void Start()
    {
      _lightManager =  Dependencies.Instance.GetDependancy<LightManager>();
    }

    private void FixedUpdate()
    {
        if(_lightManager.CurrentLight.ToString() == "GreenNeon" && currentMaterial != sadFaceMaterial)
        {
            currentMaterial = sadFaceMaterial;
            gameObject.GetComponent<MeshRenderer>().material = currentMaterial;
        }

        if (_lightManager.CurrentLight.ToString() != "GreenNeon" && currentMaterial == sadFaceMaterial)
        {
            currentMaterial = normalFaceMaterial;
            gameObject.GetComponent<MeshRenderer>().material = currentMaterial;
        }
    }
}
