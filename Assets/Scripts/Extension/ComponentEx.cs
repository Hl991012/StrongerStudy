using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extension
{
    public static class ComponentEx
    {
        /// <summary>
        /// 修改当前Transform的所有Layer
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="layer"></param>
        public static void ChangeAllChildLayer(this Transform transform, int layer)
        {
            if(transform == null) return;
            if (transform == null)
            {
                return;
            }
            transform.gameObject.layer = layer;
            for (int i = 0, imax = transform.childCount; i < imax; ++i)
            {
                var child = transform.GetChild(i);
                ChangeAllChildLayer(child, layer);
            }
        }

        /// <summary>
        /// 根据名字找到当前物体下的子物体
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform transform, string name)
        {
            if (transform == null)
            {
                return null;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                var childTrans = transform.GetChild(i);
                if (childTrans.name == name)
                {
                    return childTrans;
                }

                var find = FindChildByName(childTrans, name);
                if (find != null)
                {
                    return find;
                }
            }

            return null;
        }
    }   
}
