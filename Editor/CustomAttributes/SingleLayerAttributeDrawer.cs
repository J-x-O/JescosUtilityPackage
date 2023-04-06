using UnityEditor;
using UnityEngine;

namespace JescoDev.Utility.CustomAttributes.Editor {
    
    [CustomPropertyDrawer(typeof(SingleLayerAttribute))]
    public class SingleLayerAttributeDrawer: PropertyDrawer  {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            property.intValue = EditorGUI.LayerField(position, property.intValue);
            EditorGUI.EndProperty( );
        }

    }
}