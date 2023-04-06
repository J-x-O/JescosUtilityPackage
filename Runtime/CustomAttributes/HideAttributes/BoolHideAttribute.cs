using System;

namespace JescoDev.Utility.CustomAttributes.HideAttributes {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct,Inherited = true)]
    public class BoolHideAttribute : HideAttribute {

        public BoolHideAttribute(string conditionalSourceField) : base(conditionalSourceField) { }

        public BoolHideAttribute(string conditionalSourceField, bool hideInInspector)
            : base(conditionalSourceField, hideInInspector) { }
        
        public BoolHideAttribute(string conditionalSourceField, bool hideInInspector, bool invert)
            : base(conditionalSourceField, hideInInspector, invert) { }
    }

}