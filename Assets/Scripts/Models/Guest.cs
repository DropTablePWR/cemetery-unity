using System;

[Serializable]
public class Guest
{
    public int id;
    public String firstName;
    public String lastName;
    public String birthDate;
    public String deathDate;

    public Guest(int id, string firstName, string lastName, String birthDate, String deathDate)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.birthDate = birthDate;
        this.deathDate = deathDate;
    }
}