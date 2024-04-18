using UnityEngine;

public class CrystalTrapActivationState : State
{
    [SerializeField] private CrystalTrap _crystalFromFirstGrowPoint;
    [SerializeField] private CrystalTrap _crystalFromSecondGrowPoint;

    private void OnEnable()
    {
        _crystalFromFirstGrowPoint.gameObject.SetActive(true);
        _crystalFromSecondGrowPoint.gameObject.SetActive(true);
    }
}