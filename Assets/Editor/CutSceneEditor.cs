using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CutScene), true)]
[CanEditMultipleObjects]
public class CutSceneEditor : Editor
{
    private SerializedProperty _needFlashEffect;
    private SerializedProperty _sceneForFlashEffect;
    private SerializedProperty _flashBangEffect;
    private SerializedProperty _images;
    private SerializedProperty _textDisplay;
    private SerializedProperty _texts;
    private SerializedProperty _typingSpeed;
    private SerializedProperty _nextScene;

    private void OnEnable()
    {
        _needFlashEffect = serializedObject.FindProperty("_needFlashEffect");
        _sceneForFlashEffect = serializedObject.FindProperty("_sceneForFlashEffect");
        _flashBangEffect = serializedObject.FindProperty("_flashBangEffect");
        _images = serializedObject.FindProperty("_images");
        _textDisplay = serializedObject.FindProperty("_textDisplay");
        _texts = serializedObject.FindProperty("_texts");
        _typingSpeed = serializedObject.FindProperty("_typingSpeed");
        _nextScene = serializedObject.FindProperty("_nextScene");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Flashbang Effect", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_needFlashEffect);
        if (_needFlashEffect.boolValue)
        {
            EditorGUILayout.PropertyField(_sceneForFlashEffect);
            EditorGUILayout.PropertyField(_flashBangEffect);
        }

        DrawLine();

        EditorGUILayout.LabelField("Images", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_images);

        DrawLine();

        EditorGUILayout.LabelField("Dialogue", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_textDisplay);
        EditorGUILayout.PropertyField(_typingSpeed);
        EditorGUILayout.PropertyField(_texts);

        DrawLine();

        EditorGUILayout.PropertyField(_nextScene);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawLine()
    {
        EditorGUILayout.Space(5);
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);
        EditorGUILayout.Space(5);
    }
}
