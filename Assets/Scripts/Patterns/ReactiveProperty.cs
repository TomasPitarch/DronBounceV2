using System;
using System.Collections.Generic;

public class ReactiveProperty<T>
{
    public event Action<T> OnValueChanged
    {
        add
        {
            _onValueChanged += value;
            value?.Invoke(_value);
        }
        remove
        {
            _onValueChanged -= value;
        }
    }
    private Action<T> _onValueChanged;
    private T _value;
    private readonly EqualityComparer<T> _comparer = EqualityComparer<T>.Default;
    
    public T Value
    {
        get => _value;
        set
        {
            if (_comparer.Equals(_value, value)) return;
            _value = value;
            _onValueChanged?.Invoke(value);
        }
    }

    public ReactiveProperty(T value)
    {
        _value = value;
    }
    
    public void SetValueWithoutNotify(T newValue)
    {
        _value = newValue;
    }
}
