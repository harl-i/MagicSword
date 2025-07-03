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
    private SerializedProperty _textsRu;
    private SerializedProperty _textsEn;
    private SerializedProperty _textsTr;
    private SerializedProperty _typingSpeed;
    private SerializedProperty _nextSceneLoader;
    private SerializedProperty _skipCutscene;

    private void OnEnable()
    {
        _needFlashEffect = serializedObject.FindProperty("_needFlashEffect");
        _sceneForFlashEffect = serializedObject.FindProperty("_sceneForFlashEffect");
        _flashBangEffect = serializedObject.FindProperty("_flashBangEffect");
        _images = serializedObject.FindProperty("_images");
        _textDisplay = serializedObject.FindProperty("_textDisplay");
        _textsRu = serializedObject.FindProperty("_textsRu");
        _textsEn = serializedObject.FindProperty("_textsEn");
        _textsTr = serializedObject.FindProperty("_textsTr");
        _typingSpeed = serializedObject.FindProperty("_typingSpeed");
        _nextSceneLoader = serializedObject.FindProperty("_nextSceneLoader");
        _nextSceneLoader = serializedObject.FindProperty("_nextSceneLoader");
        _skipCutscene = serializedObject.FindProperty("_skipCutscene");
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
        EditorGUILayout.PropertyField(_textsRu);
        EditorGUILayout.PropertyField(_textsEn);
        EditorGUILayout.PropertyField(_textsTr);

        DrawLine();

        EditorGUILayout.PropertyField(_nextSceneLoader);

        DrawLine();

        EditorGUILayout.PropertyField(_skipCutscene);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawLine()
    {
        EditorGUILayout.Space(5);
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);
        EditorGUILayout.Space(5);
    }
}
