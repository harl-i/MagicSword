using System.Collections;
using UnityEngine;

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

    private void OnEnable()
    {
        SceneDialogue.OnDialogShow += HandleDialogueShow;
        OneTimeCheckScreenSize();
    }

    private void OnDisable()
    {
        SceneDialogue.OnDialogShow -= HandleDialogueShow;
    }

    private void Start()
    {
        StartCoroutine(CheckScreenSize());
    }

    public void OneTimeCheckScreenSize()
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

    private IEnumerator CheckScreenSize()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

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
    }

    private void HandleDialogueShow(bool isShow)
    {
        if (isShow)
        {
            StopCoroutine(CheckScreenSize());
        }
        else
        {
            StartCoroutine(CheckScreenSize());
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
}
