﻿using System.Collections.Generic;
using System.Linq;
using Plugins.xNodeUtilityAi.Utils.ClassTypeReference;
using UnityEngine;
using XNode;

namespace Plugins.xNodeUtilityAi.Framework {
    [CreateAssetMenu(fileName = "AIBrainGraph", menuName = "ReflexityAI/AIBrainGraph")]
    public class AIBrainGraph : NodeGraph {
        
        [Header("Brain Parameters")] 
        [ClassExtends(typeof(ReflexityAI), Grouping = ClassGrouping.None)]
        public ClassTypeReference ContextType;

        public List<string> HistoricTags = new List<string>();
        public List<string> MemoryTags = new List<string>();

        public IEnumerable<T> GetNodes<T>() {
            return nodes.Where(node => node is T).Cast<T>();
        }
        
        
        
    }
}