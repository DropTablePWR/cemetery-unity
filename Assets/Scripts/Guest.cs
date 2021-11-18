using System;

public class Guest
{
    public String name;
    public DateTime birthDate;
    public DateTime deadDate;

    public Guest(DateTime deadDate, DateTime birthDate, string name)
    {
        this.deadDate = deadDate;
        this.birthDate = birthDate;
        this.name = name;
    }
}