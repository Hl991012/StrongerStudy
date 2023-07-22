using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CommonFrame
{
    public interface IPoolableObject
    {
        [SerializeField]
        string id { get; set; }
    }
}



