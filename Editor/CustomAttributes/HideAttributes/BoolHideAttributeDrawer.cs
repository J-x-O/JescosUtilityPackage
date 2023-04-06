using JescoDev.Utility.CustomAttributes.HideAttributes;
using UnityEditor;

namespace JescoDev.Utility.CustomAttributes.Editor.HideAttributes {
    
    [CustomPropertyDrawer(typeof(BoolHideAttribute))]
    public class BoolHideAttributeDrawer : HideAttributeDrawer<BoolHideAttribute> {
        protected override bool SolveAttributeValue(BoolHideAttribute castedAttribute, SerializedProperty target) {
            return target.boolValue;
        }
    }
}