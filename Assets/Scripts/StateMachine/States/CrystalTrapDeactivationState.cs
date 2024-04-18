using UnityEngine;


public class CrystalTrapDeactivationState : State
{
    [SerializeField] private CrystalDestroyer _firstCrystalDeactivator;
    [SerializeField] private CrystalDestroyer _secondCrystalDeactivator;

    private void OnEnable()
    {
        _firstCrystalDeactivator.Destroy();
        _secondCrystalDeactivator.Destroy();
    }
}
