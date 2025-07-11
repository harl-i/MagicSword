using UnityEngine;
using YG;

public class EnviromentSwitcher : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _mainCamera;

    [Header("UI")]
    [SerializeField] private GameObject _mobileUI;
    [SerializeField] private GameObject _desktopUI;


    [Header("Camera follow component")]
    [SerializeField] private CameraFollow _cameraFollow;

    public float _scaleFactor;

    private void Start()
    {
        if (YG2.envir.isDesktop)
        {
            SwitchToDesktop();
        }
        else if (YG2.envir.isMobile)
        {
            SwitchToMobile();
        }
    }
    private void Update()
    {
        float width = Screen.width;
        float height = Screen.height;


        if (width / height < _scaleFactor)
        {
            SwitchToMobile();
        }
        else if (width / height > _scaleFactor)
        {
            SwitchToDesktop();
        }
    }

    private void SwitchToMobile()
    {
        _desktopUI.SetActive(false);
        _mobileUI.SetActive(true);
        _mainCamera.orthographicSize = 5.61f;

        _cameraFollow.SwitchToMobile();
    }

    private void SwitchToDesktop()
    {
        _desktopUI.SetActive(true);
        _mobileUI.SetActive(false);
        _mainCamera.orthographicSize = 3.2f;

        _cameraFollow.SwitchToDesktop();
    }

    private void ChangePosition(float newPosition, RectTransform background)
    {
        Vector2 backgroundPosition = background.anchoredPosition;
        backgroundPosition.x = newPosition;
        background.anchoredPosition = backgroundPosition;
    }
}
