﻿using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.xNodeUtilityAi.Framework;
using XNode;
using Object = UnityEngine.Object;

namespace Plugins.xNodeUtilityAi.AbstractNodes {
    [NodeTint(120, 255, 120), NodeWidth(400)]
    public abstract class EntryNode : Node {
        
        [Input(ShowBackingValue.Never)] public TaggedData Data;
        
        protected AbstractAIComponent _context;
        
        public void SetContext(AbstractAIComponent context) {
            _context = context;
        }

        protected T GetData<T>() where T : Object {
            List<TaggedData> taggedDatas = GetInputValues<TaggedData>("Data").ToList();
            taggedDatas.RemoveAll(data => data == null);
            taggedDatas = taggedDatas.Where(data => data.Data is T).ToList();
            if (taggedDatas.Count > 1)
                throw new Exception("Multiple Data found for type " + typeof(T) + " in " + name + 
                                    ", you should consider using GetData with a dataTag as parameter");
            if (taggedDatas.Count > 0)
                return taggedDatas.First().Data as T;
            return null;
        }

        protected T GetData<T>(string dataTag) where T : Object {
            List<TaggedData> taggedDatas = GetInputValues<TaggedData>("Data").ToList();
            taggedDatas.RemoveAll(data => data == null);
            taggedDatas = taggedDatas.Where(data => data.Data is T && data.DataTag == dataTag).ToList();
            if (taggedDatas.Count > 1)
                throw new Exception("Multi Data found for type " + typeof(T) + " and tag " + dataTag + 
                                    " in " + name + ", don't use the same dataTag twice as input");
            if (taggedDatas.Count > 0)
                return taggedDatas.First().Data as T;
            return null;
        }
        
    }
}