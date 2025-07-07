using UnityEngine;
using UnityEngine.UI;

public class FlashBangEffect : MonoBehaviour
{
    [SerializeField] private Image _flasImage;
    [SerializeField] private float flashDuration = 1f;
    [SerializeField] private float fadeDuration = 1f;

    private bool isFlashBangActive = false;

    void Update()
    {
        if (isFlashBangActive)
        {
            Color color = _flasImage.color;
            color.a -= Time.deltaTime / fadeDuration;
            _flasImage.color = color;

            if (_flasImage.color.a <= 0)
            {
                isFlashBangActive = false;
            }
        }
    }

    public void FlashBanged()
    {
        int randomValue = Random.Range(0, 20);
        
        if(randomValue < 6)
        {
            isFlashBangActive = true;

            Color color = _flasImage.color;
            color.a = 1;
            _flasImage.color = color;
        }
    }
}
