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

    // виртуал позволяет переопределять
    public virtual void Attack()
    {
        Console.WriteLine($"{Name} атакует");
    }

    // отнимает здоровье
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0; // чтобы в минус не ушло
        }
        Console.WriteLine($"{Name} получает {damage} урон, осталось {Hp} hp");
    }

    //жив или нет
    public bool IsAlive()
    {
        return Hp > 0;
    }
}

    // наследование от юнита через двоеточие
public class Archer : Unit
{
    public Archer(string name, int hp) : base(name, hp)
    {
    }

    // переопределение через оверрайд
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

        //новые функции
        archer.TakeDamage(40);
        archer.TakeDamage(70);

        if (archer.IsAlive())
        {
            Console.WriteLine($"{archer.Name} жив");
        }
        else
        {
            Console.WriteLine($"{archer.Name} погиб");
        }
    }
}