using System.Collections;
using Unity.Cinemachine;

using UnityEngine;
using UnityEngine.InputSystem;

public class levelPreviewCameraScript : MonoBehaviour
{
   [SerializeField] private CinemachineCamera virtualCamera;
   [SerializeField] private float normalLensSize = 5f;
   [SerializeField] private float previewLensSize = 12f;
   [SerializeField] private float zoomSpeed = 2f;

   [SerializeField] private Transform playerTransform;
   [SerializeField] private Transform levelCenterTarget;
   [SerializeField] private MonoBehaviour PlayerMovementScript;
   [SerializeField] private GameObject startPromptUI;
   private bool isPreviewing  = true;

    private void Start()
    {
        if(virtualCamera == null)
        {
            virtualCamera = GetComponent<CinemachineCamera>();
        }

        if(PlayerMovementScript != null)
        {
            PlayerMovementScript.enabled = false;
        }
         if(levelCenterTarget != null && virtualCamera != null)
        {
            virtualCamera.Target.TrackingTarget = levelCenterTarget;
        }
        if(virtualCamera != null)
        {
            virtualCamera.Lens.OrthographicSize = previewLensSize;
        }
        if(startPromptUI != null)
        {
            startPromptUI.SetActive(true);
        }

        

    }

    private bool AnyKeyPressedThisFrame()
    {
        if(Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            return true;
        }
         if(Mouse.current != null && (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame))
        {
            return true;
        }
         return false;
    }

    private void Update()
    {
        if(isPreviewing && AnyKeyPressedThisFrame())
        {
            StartCoroutine(TransitionToGameplay());
        }
    }
    
    private IEnumerator TransitionToGameplay()
    {
        isPreviewing = false;

        if(startPromptUI != null)
        {
            startPromptUI.SetActive(false);
        }

        if(playerTransform != null && virtualCamera != null)
        {
            virtualCamera.Target.TrackingTarget = playerTransform;
        }
         while(virtualCamera != null && Mathf.Abs(virtualCamera.Lens.OrthographicSize - normalLensSize) > 0.05f)
        {
            virtualCamera.Lens.OrthographicSize = Mathf.Lerp(
                virtualCamera.Lens.OrthographicSize, normalLensSize, Time.deltaTime * zoomSpeed
            );
            yield return null;
        }

        if(virtualCamera != null)
        {
            virtualCamera.Lens.OrthographicSize = normalLensSize;
        }

        if(PlayerMovementScript != null)
        {
            PlayerMovementScript.enabled = true;
        }
    }

}
