using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    //private int _playerHealthCount;

    private GameObject[] heartContainers;
    private Image[] heartFills;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void OnEnable()
    {
        Player.HealthHasChanged += UpdateHeartsHUD;
    }

    private void OnDisable()
    {
        Player.HealthHasChanged -= UpdateHeartsHUD;
    }

    private void Awake()
    {
        heartContainers = new GameObject[3]; // переделать на загрузку из параметров игрока
        heartFills = new Image[3]; // переделать на загрузку из параметров игрока

        InstantiateHeartContainers(3); // переделать на загрузку из параметров игрока
        UpdateHeartsHUD(3); // переделать на загрузку из параметров игрока
    }

    public void UpdateHeartsHUD(int count)
    {
        SetHeartContainers(count);
        SetFilledHearts(count);
    }

    void SetHeartContainers(int count)
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < count)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts(int count)
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < count)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (count % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(count);
            heartFills[lastPos].fillAmount = count % 1;
        }
    }

    void InstantiateHeartContainers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
