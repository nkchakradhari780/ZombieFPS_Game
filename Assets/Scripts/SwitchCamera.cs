using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera to Assign")]
    public GameObject aimCamera;
    public GameObject aimCanvas;
    public GameObject thirdPersonCamera;
    public GameObject thirdPersonCanvas;

    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            aimCamera.SetActive(true);
            aimCanvas.SetActive(true);
            thirdPersonCamera.SetActive(false);
            thirdPersonCanvas.SetActive(false);
        }
        else
        {
            aimCamera.SetActive(false);
            aimCanvas.SetActive(false);
            thirdPersonCamera.SetActive(true);
            thirdPersonCanvas.SetActive(true);
        }
    }
}
