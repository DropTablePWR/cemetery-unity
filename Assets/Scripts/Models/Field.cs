using System;
using JetBrains.Annotations;
[Serializable]
public class Field
{
    public string fieldEnum;
    [CanBeNull] public Grave tombstone;

    public Field(string fieldEnum, [CanBeNull] Grave tombstone)
    {
        this.fieldEnum = fieldEnum;
        this.tombstone = tombstone;
    }
}