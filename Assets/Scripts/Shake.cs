using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private SwordThrow _swordThrow;
    [SerializeField] private Transform _bladeEnd;
    [SerializeField] private float _shakeTime = 0.5f;
    [SerializeField] private float _shakeAmount = 0.05f;
    [SerializeField] private float _shakeSpeed = 2f;

    private bool _isStuckInWall;

    private void OnEnable()
    {
        _swordThrow.StuckInWall += OnStuckInWall;
    }

    private void OnDisable()
    {
        _swordThrow.StuckInWall -= OnStuckInWall;
    }

    private void Update()
    {
        if (_isStuckInWall)
        {
            StartCoroutine(ShakeSword());
        }
    }

    private void OnStuckInWall(bool isStuckInWall)
    {
        _isStuckInWall = isStuckInWall;
    }

    private IEnumerator ShakeSword()
    {
        while (_isStuckInWall)
        {
            float offsetX = Random.Range(-_shakeAmount, _shakeAmount);
            float offsetY = Random.Range(-_shakeAmount, _shakeAmount);
            float offsetZ = Random.Range(-_shakeAmount, _shakeAmount);

            Vector3 bladeEndPosition = _bladeEnd.localPosition;
            Vector3 bladeEndRotation = _bladeEnd.localRotation.eulerAngles;

            bladeEndPosition += new Vector3(offsetX, offsetY, offsetZ) * Time.deltaTime;
            bladeEndRotation += new Vector3(offsetX * _shakeSpeed, offsetY * _shakeSpeed, offsetZ * _shakeSpeed) * Time.deltaTime;

            _bladeEnd.localPosition = bladeEndPosition;
            _bladeEnd.localRotation = Quaternion.Euler(bladeEndRotation);

            transform.RotateAround(_bladeEnd.position, _bladeEnd.forward, offsetX);

            yield return null;
        }

        _isStuckInWall = false;
    }
}
