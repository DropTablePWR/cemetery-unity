using System;
using Backend;
using JetBrains.Annotations;
[Serializable]
public class Field : AData
{
    public string fieldEnum;
    [CanBeNull] public Grave tombstone;

    public Field(string fieldEnum, [CanBeNull] Grave tombstone)
    {
        this.fieldEnum = fieldEnum;
        this.tombstone = tombstone;
    }
}