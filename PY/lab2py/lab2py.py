class Unit:
    #конструктор
    def __init__(self, name, hp):
        self.name = name
        self.hp = hp

    def attack(self):
        print(f'{self.name} атакует')

#наследование от Unit
class Archer(Unit):
    def attack(self):
        print(f'{self.name} стреляет из лука')

if __name__ == "__main__":
    hero = Archer("Лучник", 100)
    hero.attack()