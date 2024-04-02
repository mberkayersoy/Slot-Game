#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PayLineSO))]
public class PayLineSOEditor : Editor
{
    private static readonly Color undefinedColor = Color.red;
    private static readonly Color definedColor = Color.green;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PayLineSO payLineSO = (PayLineSO)target;

        EditorGUILayout.LabelField("Pay Line:");

        for (int row = 0; row < payLineSO.RowCount; row++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int column = 0; column < payLineSO.ColumnCount; column++)
            {
                EditorGUI.BeginChangeCheck();
                int selectedIndex = (int)payLineSO.GetCell(column, row);
                Color color = (selectedIndex == (int)PayLineCell.UnDefined) ? undefinedColor : definedColor;
                GUI.backgroundColor = color;

                string[] options = { "Undefined", "Defined" };
                selectedIndex = EditorGUILayout.Popup(selectedIndex, options, GUILayout.Width(70), GUILayout.Height(20));

                GUI.backgroundColor = Color.white;

                if (EditorGUI.EndChangeCheck())
                {
                    payLineSO.SetCell(column, row, (PayLineCell)selectedIndex);
                    EditorUtility.SetDirty(payLineSO);
                }
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif
