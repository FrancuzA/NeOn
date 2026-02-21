using UnityEngine;

public partial class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        /* float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
         float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

         xRotation -= mouseY;
         xRotation = Mathf.Clamp(xRotation, -90f, 90f);

         transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
         playerBody.Rotate(Vector3.up * mouseX);*/

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(new Vector3(0f, -90f, 0f));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(new Vector3(0f, 90f, 0f));
        }
    }
}