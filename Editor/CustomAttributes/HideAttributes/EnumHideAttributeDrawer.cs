using JescoDev.Utility.CustomAttributes.HideAttributes;
using UnityEditor;

namespace JescoDev.Utility.CustomAttributes.Editor.HideAttributes {

    [CustomPropertyDrawer(typeof(EnumHideAttribute))]
    public class EnumHideAttributeDrawer : HideAttributeDrawer<EnumHideAttribute> {

        protected override bool SolveAttributeValue(EnumHideAttribute castedAttribute, SerializedProperty target) {

            int value = target.enumValueIndex;
            
            foreach (int i in castedAttribute.ComparedEnumValue) {
                if (value == i) return false;
            }
            
            return true;
        }
    }

}