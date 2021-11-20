using System;
using Backend;

[Serializable]
public class Guest : AData
{
    public int id;
    public string firstName;
    public string lastName;
    public string birthDate;
    public string deathDate;

    public Guest(int id, string firstName, string lastName, string birthDate, string deathDate)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.birthDate = birthDate;
        this.deathDate = deathDate;
    }
}