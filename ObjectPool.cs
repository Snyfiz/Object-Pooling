using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : class
{
    private readonly Stack<T> _Stack = new Stack<T>();
    private readonly Func<T> _OnCreate;
    private readonly Action<T> _OnGet;
    private readonly Action<T> _OnRelease;
    private int _MaxSize = 100;

    public ObjectPool(Func<T> pOnCreate, Action<T> pOnGet, Action<T> pOnRelease, int pMaxSize)
    {
        _OnCreate = pOnCreate ?? throw new ArgumentNullException(nameof(pOnCreate));
        _OnGet = pOnGet;
        _OnRelease = pOnRelease;
        _MaxSize = pMaxSize;
    }

    public T GetObj()
    {
        T lItem;

        if(_Stack.Count > 0)
            lItem = _Stack.Pop();

        else
        {
            lItem = _OnCreate();
            _MaxSize = Mathf.Max(_MaxSize, _Stack.Count) + 1;
        }

        _OnGet?.Invoke(lItem);
        return lItem;
    }

    public void ReleaseObj(T pItem)
    {
        if(_Stack.Count < _MaxSize)
            _Stack.Push(pItem);

        _OnRelease?.Invoke(pItem);
    }
}
