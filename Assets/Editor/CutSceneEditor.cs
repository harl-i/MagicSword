using CutScenes;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CutScene), true)]
[CanEditMultipleObjects]
public class CutSceneEditor : Editor
{
    private SerializedProperty _needFlashEffect;
    private SerializedProperty _sceneForFlashEffect;
    private SerializedProperty _flashBangEffect;
    private SerializedProperty _frames;
    private SerializedProperty _textDisplay;
    private SerializedProperty _typingSpeed;
    private SerializedProperty _localizationManager;
    private SerializedProperty _nextSceneLoader;
    private SerializedProperty _skipCutscene;
    private SerializedProperty _tapToScreenTip;

    private void OnEnable()
    {
        _needFlashEffect = serializedObject.FindProperty("_needFlashEffect");
        _sceneForFlashEffect = serializedObject.FindProperty("_sceneForFlashEffect");
        _flashBangEffect = serializedObject.FindProperty("_flashBangEffect");
        _frames = serializedObject.FindProperty("_frames");
        _textDisplay = serializedObject.FindProperty("_textDisplay");
        _typingSpeed = serializedObject.FindProperty("_typingSpeed");
        _localizationManager = serializedObject.FindProperty("_localizationManager");
        _nextSceneLoader = serializedObject.FindProperty("_nextSceneLoader");
        _skipCutscene = serializedObject.FindProperty("_skipCutscene");
        _tapToScreenTip = serializedObject.FindProperty("_tapToScreenTip");
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

        EditorGUILayout.LabelField("Frames", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_frames);

        DrawLine();

        EditorGUILayout.LabelField("Dialogue", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_textDisplay);
        EditorGUILayout.PropertyField(_typingSpeed);
        EditorGUILayout.PropertyField(_localizationManager);

        DrawLine();

        EditorGUILayout.PropertyField(_nextSceneLoader);

        DrawLine();

        EditorGUILayout.PropertyField(_skipCutscene);
        EditorGUILayout.PropertyField(_tapToScreenTip);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawLine()
    {
        EditorGUILayout.Space(5);
        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);
        EditorGUILayout.Space(5);
    }
}
