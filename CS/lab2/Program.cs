using System;

namespace game;

public class Unit
{
    public string Name;
    public int Hp;


    public Unit(string name, int hp)
    {
        Name = name;
        Hp = hp;
    }
    //виртуал позволяет переопределять
    public virtual void Attack()
    {
        Console.WriteLine($"{Name} атакует");
    }
}
//наследование от юнита через двоеточие
public class Archer : Unit
{
    public Archer(string name, int hp) : base(name, hp)
    {
    }

    //переопределение через оверрайд
    public override void Attack()
    {
        Console.WriteLine($"{Name} стреляет из лука");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Archer archer = new Archer("Лучник", 100);
        archer.Attack();
        Unit warior = new Unit("Воин", 150);
        warior.Attack();
    }
}