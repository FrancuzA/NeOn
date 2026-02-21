using UnityEngine;

public partial class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public float yLookLimit = 45f;
    public float bodySmoothSpeed = 10f;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private float bodyYRotation = 0f;
    private Quaternion targetBodyRotation;

    void Start()
    {
        bodyYRotation = playerBody.eulerAngles.y;
        targetBodyRotation = Quaternion.Euler(0f, bodyYRotation, 0f);
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -yLookLimit, yLookLimit);

        if (Input.GetKeyDown(KeyCode.A)) bodyYRotation -= 90f;
        if (Input.GetKeyDown(KeyCode.D)) bodyYRotation += 90f;

        targetBodyRotation = Quaternion.Euler(0f, bodyYRotation, 0f);
        playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetBodyRotation, bodySmoothSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}