using Enemies;
using StateMachine;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Shooting), true)]
[CanEditMultipleObjects]
public class ShootingEditor : Editor
{
    private SerializedProperty ShootPoint;
    private SerializedProperty _container;
    private SerializedProperty _capacity;
    private SerializedProperty TowardsBullet;
    private SerializedProperty StraightBullet;
    private SerializedProperty HomingBullet;
    private SerializedProperty ShowTurretComponent;

    private void OnEnable()
    {
        ShootPoint = serializedObject.FindProperty("ShootPoint");
        _container = serializedObject.FindProperty("_container");
        _capacity = serializedObject.FindProperty("_capacity");
        TowardsBullet = serializedObject.FindProperty("TowardsBullet");
        StraightBullet = serializedObject.FindProperty("StraightBullet");
        HomingBullet = serializedObject.FindProperty("HomingBullet");
        ShowTurretComponent = serializedObject.FindProperty("ShowTurretComponent");
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
                    EditorGUILayout.PropertyField(StraightBullet);
                    break;
                case ShootingEnemyType.Turret:
                    EditorGUILayout.PropertyField(TowardsBullet);
                    EditorGUILayout.PropertyField(ShowTurretComponent);
                    break;
                case ShootingEnemyType.TowardsTurret:
                    EditorGUILayout.PropertyField(TowardsBullet);
                    EditorGUILayout.PropertyField(ShowTurretComponent);
                    break;
                case ShootingEnemyType.Gargoyle:
                case ShootingEnemyType.Scorpion:
                case ShootingEnemyType.Snowman:
                    EditorGUILayout.PropertyField(HomingBullet);
                    break;
                case ShootingEnemyType.Archer:
                    EditorGUILayout.PropertyField(TowardsBullet);
                    break;
                default:
                    break;
            }
        }

        DrawLine();

        EditorGUILayout.PropertyField(ShootPoint);
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
