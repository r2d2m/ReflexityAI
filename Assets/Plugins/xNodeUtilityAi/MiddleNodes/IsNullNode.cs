using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Plugins.xNodeUtilityAi.MiddleNodes {
    public class IsNullNode : MiddleNode {
        
        [Input(ShowBackingValue.Never, ConnectionType.Override)] public Object ValueIn;
        [Output] public bool ValueOut;
        
        public override object GetValue(NodePort port) {
            if (port.fieldName == nameof(ValueOut)) {
                return GetInputValue<Object>(nameof(ValueIn)) == null;
            }
            return null;
        }
        
    }
}