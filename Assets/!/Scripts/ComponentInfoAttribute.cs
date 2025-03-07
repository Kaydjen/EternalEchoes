using System;

[AttributeUsage(AttributeTargets.Class)]
public class ComponentInfoAttribute : Attribute
{
    public string Name { get; private set; } = null;
    public string Description { get; private set; } = null;

    public ComponentInfoAttribute(string name)
    {
        Name = name;
    }
    public ComponentInfoAttribute(string name, string description) : this(name)
    {
        Description = description;
    }
}
