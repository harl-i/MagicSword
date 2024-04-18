using UnityEngine;

public class ScorpionDefeatedTransition : Transition
{
    [SerializeField] private GameObject _scorpion;

    private void Update()
    {
        if (_scorpion.activeSelf == false)
        {
            NeedTransit = true;
        }
    }
}
