using UnityEditor;

[CustomEditor(typeof(NextSceneLoader), true)]
[CanEditMultipleObjects]
public class NextSceneLoaderEditor : Editor
{
    private SerializedProperty _isDelayNeeded;
    private SerializedProperty _delay;

    private void OnEnable()
    {
        _isDelayNeeded = serializedObject.FindProperty("_isDelayNeeded");
        _delay = serializedObject.FindProperty("_delay");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();


        EditorGUILayout.PropertyField(_isDelayNeeded);
        if (_isDelayNeeded.boolValue)
        {
            EditorGUILayout.PropertyField(_delay);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
