using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : new() {

    Stack<T> freeObjects;

    public Pool()
    {
        freeObjects = new Stack<T>();
    }

    public T Obtain()
    {
        return freeObjects.Count == 0 ? new T() : freeObjects.Pop();
    }

    public void Free(T Object)
    {
        freeObjects.Push(Object);
        Reset(Object);
    }

    public void FreeAll(List<T> Objects)
    {
        foreach (T Object in Objects)
        {
            Free(Object);
        }
    }

    public void Reset(T Object)
    {
        if(Object is IPoolable)
        {
            IPoolable poolable = (IPoolable) Object;
            poolable.Reset();
        }
    }
}
