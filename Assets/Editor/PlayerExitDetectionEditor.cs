using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerExitDetectionTransition), true)]
[CanEditMultipleObjects]
public class PlayerExitDetectionEditor : Editor
{
    private SerializedProperty _targetState;
    private SerializedProperty _playerLayer;
    private SerializedProperty _detectionZoneType;
    private SerializedProperty _detectionRadius;
    private SerializedProperty _detectionRectangleSize;
    private SerializedProperty _offsetY;

    private void OnEnable()
    {
        _targetState = serializedObject.FindProperty("_targetState");
        _detectionZoneType = serializedObject.FindProperty("_detectionZoneType");
        _playerLayer = serializedObject.FindProperty("_playerLayer");
        _detectionRadius = serializedObject.FindProperty("_detectionRadius");
        _detectionRectangleSize = serializedObject.FindProperty("_detectionRectangleSize");
        _offsetY = serializedObject.FindProperty("_offsetY");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_targetState);
        EditorGUILayout.PropertyField(_playerLayer);

        DrawLine();

        EditorGUILayout.PropertyField(_detectionZoneType);
        switch (_detectionZoneType.enumValueIndex)
        {
            case (int)DetectionZoneType.Circle:
                ShowCircleZoneFields();
                break;
            case (int)DetectionZoneType.Rectangle:
                ShowRectangleZoneFields();
                break;
            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowRectangleZoneFields()
    {
        EditorGUILayout.PropertyField(_detectionRectangleSize);
        EditorGUILayout.PropertyField(_offsetY);
    }

    private void ShowCircleZoneFields()
    {
        EditorGUILayout.PropertyField(_detectionRadius);
    }

    private void DrawLine()
    {
        EditorGUILayout.Space(5);
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);
        EditorGUILayout.Space(5);
    }
}
