using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Shooting), true)]
[CanEditMultipleObjects]
public class ShootingEditor : Editor
{
    private SerializedProperty _shootPoint;
    private SerializedProperty _container;
    private SerializedProperty _capacity;
    private SerializedProperty _towardsBullet;
    private SerializedProperty _straightBullet;
    private SerializedProperty _homingBullet;
    private SerializedProperty _showTurretComponent;
    private SerializedProperty _lookAtBullet;

    private void OnEnable()
    {
        _shootPoint = serializedObject.FindProperty("_shootPoint");
        _container = serializedObject.FindProperty("_container");
        _capacity = serializedObject.FindProperty("_capacity");
        _towardsBullet = serializedObject.FindProperty("_towardsBullet");
        _straightBullet = serializedObject.FindProperty("_straightBullet");
        _homingBullet = serializedObject.FindProperty("_homingBullet");
        _showTurretComponent = serializedObject.FindProperty("_showTurretComponent");
        _lookAtBullet = serializedObject.FindProperty("_lookAtBullet");
    }

    public override void OnInspectorGUI()
    {
        Shooting shootingComponentTarget = (Shooting)target;
        ShootState shootState = shootingComponentTarget.GetComponent<ShootState>();

        ShootingEnemyType enemyType;

        serializedObject.Update();
        if (shootState != null)
        {
            enemyType = shootState.EnemyType;

            switch (enemyType)
            {
                case ShootingEnemyType.Spider:
                    EditorGUILayout.PropertyField(_straightBullet);
                    break;
                case ShootingEnemyType.Turret:
                    EditorGUILayout.PropertyField(_towardsBullet);
                    EditorGUILayout.PropertyField(_showTurretComponent);
                    break;
                case ShootingEnemyType.TowardsTurret:
                    EditorGUILayout.PropertyField(_lookAtBullet);
                    EditorGUILayout.PropertyField(_showTurretComponent);
                    break;
                case ShootingEnemyType.Gargoyle:
                case ShootingEnemyType.Scorpion:
                    EditorGUILayout.PropertyField(_homingBullet);
                    break;
                case ShootingEnemyType.Archer:
                    EditorGUILayout.PropertyField(_towardsBullet);
                    break;
                default:
                    break;
            }
        }

        DrawLine();

        EditorGUILayout.PropertyField(_shootPoint);
        EditorGUILayout.PropertyField(_container);
        EditorGUILayout.PropertyField(_capacity);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(shootingComponentTarget);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawLine()
    {
        EditorGUILayout.Space(5);
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);
        EditorGUILayout.Space(5);
    }
}
