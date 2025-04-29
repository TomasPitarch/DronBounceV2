using System;
using System.Collections.Generic;


public class ObjectPool<T> where T : class
{
    private readonly IFactory<T> _factory;
    private readonly List<T> _availableObjects = new List<T>();
    private readonly List<T> _inUseObjects = new List<T>();
    private int _initialSize;
    private int _maxSize;
    
    public ObjectPool(IFactory<T> factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
    public void Configure(int initialSize, int maxSize)
    {
        if (initialSize < 0 || maxSize < initialSize)
        {
            throw new ArgumentException("Invalid pool configuration.");
        }
        _initialSize = initialSize;
        _maxSize = maxSize;
        InitializePool();
    }
    private void InitializePool()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            T obj = _factory.Create();
            _availableObjects.Add(obj);
        }
    } 
    public T Get()
    {
        T obj;
        if (_availableObjects.Count > 0)
        {
            obj = _availableObjects[0];
            _availableObjects.RemoveAt(0);
            _inUseObjects.Add(obj);
           
            return obj;
        }

        if (_maxSize == 0 || _inUseObjects.Count < _maxSize)
        {
            obj = _factory.Create();
            _inUseObjects.Add(obj);
           
            return obj;
        }

        
        return null; 
    }
    public void Release(T obj)
    {
        if (obj == null || !_inUseObjects.Contains(obj))
        {
            return; 
        }

        _inUseObjects.Remove(obj);
        _availableObjects.Add(obj);
    }

    public int AvailableCount => _availableObjects.Count;
    public int InUseCount => _inUseObjects.Count;
    public int MaxSize => _maxSize == 0 ? -1 : _maxSize; 
}