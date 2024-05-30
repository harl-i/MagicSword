using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DetectTransition), true)]
[CanEditMultipleObjects]
public class DetectTransitionEditor : Editor
{
    private SerializedProperty _targetState;
    private SerializedProperty _targetLayer;
    private SerializedProperty _detectionZoneType;
    private SerializedProperty _zoneMovementType;
    private SerializedProperty _detectionRadius;
    private SerializedProperty _detectionRectangleSize;
    private SerializedProperty _detectReactionDelay;
    private SerializedProperty _isCooldownActive;
    private SerializedProperty _cooldown;
    private SerializedProperty _offsetY;
    private SerializedProperty _offsetX;


    private void OnEnable()
    {
        _targetState = serializedObject.FindProperty("_targetState");
        _targetLayer = serializedObject.FindProperty("_targetLayer");
        _detectionZoneType = serializedObject.FindProperty("_detectionZoneType");
        _zoneMovementType = serializedObject.FindProperty("_zoneMovementType");
        _detectionRadius = serializedObject.FindProperty("_detectionRadius");
        _detectionRectangleSize = serializedObject.FindProperty("_detectionRectangleSize");
        _detectReactionDelay = serializedObject.FindProperty("_detectReactionDelay");
        _isCooldownActive = serializedObject.FindProperty("_isCooldownActive");
        _cooldown = serializedObject.FindProperty("_cooldown");
        _offsetY = serializedObject.FindProperty("_offsetY");
        _offsetX = serializedObject.FindProperty("_offsetX");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_targetState);
        EditorGUILayout.PropertyField(_targetLayer);

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

        DrawLine();

        EditorGUILayout.PropertyField(_detectReactionDelay);

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowRectangleZoneFields()
    {
        EditorGUILayout.PropertyField(_detectionRectangleSize);
        EditorGUILayout.PropertyField(_zoneMovementType);
        EditorGUILayout.PropertyField(_offsetY);
        EditorGUILayout.PropertyField(_offsetX);
        EditorGUILayout.PropertyField(_isCooldownActive);
        if (_isCooldownActive.boolValue)
        {
            EditorGUILayout.PropertyField(_cooldown);
        }
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
