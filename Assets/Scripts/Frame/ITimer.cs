using System;

namespace Frame
{
    public interface ITimer
    {
        bool IsLoop { get; protected set; }
    }
}
