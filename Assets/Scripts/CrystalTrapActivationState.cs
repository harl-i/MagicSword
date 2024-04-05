using UnityEngine;

public class CrystalTrapActivationState : State
{
    [SerializeField] private Transform _firstGrowPoint;
    [SerializeField] private Transform _secondGrowPoint;

    private void OnEnable()
    {
        _firstGrowPoint.gameObject.SetActive(true);
        _secondGrowPoint.gameObject.SetActive(true);
    }
}