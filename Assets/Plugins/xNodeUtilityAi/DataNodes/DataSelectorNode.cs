using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.xNodeUtilityAi.Framework;
using UnityEngine;
using XNode;
using Object = UnityEngine.Object;

namespace Plugins.xNodeUtilityAi.DataNodes {
    public class DataSelectorNode : DataNode {
        
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Inherited)] public Object Data;
        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.Inherited)] public Object Output;

        [HideInInspector] public SerializableInfo SelectedSerializableInfo;
        [HideInInspector] public List<SerializableInfo> SerializableInfos = new List<SerializableInfo>();
        [HideInInspector] public int ChoiceIndex;
        
        public override void OnCreateConnection(NodePort from, NodePort to) {
            base.OnCreateConnection(from, to);
            if (to.fieldName == nameof(Data) && to.node == this) {
                Tuple<string, Type, object> inputValue = GetInputValue<Tuple<string, Type, object>>(nameof(Data));
                SerializableInfos.AddRange(inputValue.Item2
                    .GetFields(SerializableInfo.DefaultBindingFlags)
                    .Select(info => new SerializableInfo(info)));
                SerializableInfos.AddRange(inputValue.Item2
                    .GetProperties(SerializableInfo.DefaultBindingFlags)
                    .Select(info => new SerializableInfo(info)));
            }
        }
        
        public override void OnRemoveConnection(NodePort port) {
            base.OnRemoveConnection(port);
            if (port.fieldName == nameof(Data) && port.node == this) {
                SerializableInfos.Clear();
            }
        }
        
        public override object GetValue(NodePort port) {
            if (port.fieldName == nameof(Output)) {
                Tuple<string, Type, object> inputValue = GetInputValue<Tuple<string, Type, object>>(nameof(Data));
                return Application.isPlaying
                    ? SelectedSerializableInfo.GetRuntimeValue(inputValue.Item3)
                    : SelectedSerializableInfo.GetEditorValue();
            }
            return null;
        }

    }
}