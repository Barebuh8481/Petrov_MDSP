class Unit:
    #конструктор
    def __init__(self, name, hp):
        self.name = name
        self.hp = hp

    def attack(self):
        print(f'{self.name} атакует')

    #отнимает здоровье
    def take_damage(self, damage):
        self.hp -= damage
        if self.hp < 0:
            self.hp = 0
        print(f'{self.name} получает {damage} урона осталось hp: {self.hp}')

    #возврат здоровья
    def is_alive(self):
        return self.hp > 0

#наследование от Unit
class Archer(Unit):
    def attack(self):
        print(f'{self.name} стреляет из лука')

if __name__ == "__main__":
    hero = Archer("Лучник", 100)
    hero.attack()
    
    #новые функции
    hero.take_damage(40)
    hero.take_damage(70)
    
    if hero.is_alive():
        print(f"{hero.name} жив")
    else:
        print(f"{hero.name} погиб")