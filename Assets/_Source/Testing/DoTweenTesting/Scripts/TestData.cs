using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class TestData
{
    [SerializeField] private float _a;
    [SerializeField] private Vector3 _b;
    [SerializeReference] private List<AbstractTag> _tags;

    public int d;

    public TestData(float a, Vector3 b, int d, List<AbstractTag> tags)
    {
        _a = a;
        _b = b;
        this.d = d;
        _tags = tags;
    }

    public override string ToString()
    {
        StringBuilder tagsString = new StringBuilder();

        foreach (AbstractTag tag in _tags)
        {
            tagsString.Append($"Type: {tag.GetType().Name}. Value: ");

            if (tag is StringTag stringTag)
                tagsString.AppendLine(stringTag.Value);
            else if (tag is FloatTag floatTag)
                tagsString.AppendLine(floatTag.Value.ToString());
        }

        return $"{_a} \n{_b} \n{d} \n{tagsString}";
    }
}

[Serializable]
public abstract class AbstractTag
{
}

public class FloatTag : AbstractTag
{
    [SerializeField] private float _value;

    public FloatTag(float value)
    {
        _value = value;
    }

    public float Value => _value;
}

public class StringTag : AbstractTag
{
    [SerializeField] private string _value;

    public StringTag(string value)
    {
        _value = value;
    }

    public string Value => _value;
}