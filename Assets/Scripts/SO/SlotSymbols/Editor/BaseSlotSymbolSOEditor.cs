#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(BaseSlotSymbolSO), true)]
public class BaseSlotSymbolSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BaseSlotSymbolSO slotSymbol = (BaseSlotSymbolSO)target;
        slotSymbol.SymbolSprite = (Sprite)EditorGUILayout.ObjectField("Sprite", slotSymbol.SymbolSprite, typeof(Sprite), false);
    }
}
#endif
