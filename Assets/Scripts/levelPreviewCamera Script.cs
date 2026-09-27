using System.Collections;
using Unity.Cinemachine;

using UnityEngine;
using UnityEngine.InputSystem;

public class levelPreviewCameraScript : MonoBehaviour
{
   [SerializeField] private CinemachineCamera virtualCamera;
   [SerializeField] private float normalLensSize = 5f;
   [SerializeField] private float previewLensSize = 12f;
   [SerializeField] private float zoomSpeed = 8f;

   [SerializeField] private Transform playerTransform;
   [SerializeField] private Transform levelCenterTarget;
   [SerializeField] private MonoBehaviour PlayerMovementScript;
   [SerializeField] private GameObject startPromptUI;
   private bool isPreviewing  = true;

    private void Awake()
    {
        Time.timeScale = 1f;
        if(virtualCamera == null)
        {
            virtualCamera = GetComponent<CinemachineCamera>();
        }
    }

    private void Start()
    {

        if(PlayerMovementScript != null)
        {
            PlayerMovementScript.enabled = false;
        }


         if(levelCenterTarget != null && virtualCamera != null)
        {
            virtualCamera.Target.TrackingTarget = levelCenterTarget;

            Vector3 centerPos = levelCenterTarget.position;
            centerPos.z = virtualCamera.transform.position.z;
            virtualCamera.transform.position = centerPos;

            virtualCamera.ForceCameraPosition(centerPos, Quaternion.identity);
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

        
         while(virtualCamera != null && !Mathf.Approximately(virtualCamera.Lens.OrthographicSize, normalLensSize))
        {
            virtualCamera.Lens.OrthographicSize = Mathf.MoveTowards(
                virtualCamera.Lens.OrthographicSize, normalLensSize, zoomSpeed * Time.deltaTime
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
