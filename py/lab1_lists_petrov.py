# Создайте список минимум из 10 аргументов, некоторые из них должны повторяться
# Выведите предпоследний элемент, и несколько элементов по выбору с помощью индекса
# Измените элемент в определённой позиции
# Выведите элементы в определённом диапазоне
# Добавьте элемент в список
# Добавьте элемент в середину списка
# Посчитайте количество определённых элементов  в списке
# Скопируйте список, измените первый и сравните их в выводе

#1 Создайте список минимум из 10 аргументов,
# некоторые из них должны повторяться
def main ():
    items = [
        "a",          # 0
        "aardvark",   # 1
        "aback",      # 2
        "abacus",     # 3
        "abaft",      # 4
        "abalone",    # 5
        "abandon",    # 6
        "abase",      # 7
        "a",          # 8
        "abacus"      # 9
    ]

    print (f'list: {items}')
    print("-" * 20)

    # Выведите предпоследний элемент,
    # и несколько элементов по выбору с помощью индекса
    print(f'second to last element: {items[-2]}')
    print(f'selected by index : {items[0], items[4]}')
    print("-" * 20)


    # Измените элемент в определённой позиции
    items[2] = "grape"
    print(f"list with 3th changed: {items}")

    # Выведите элементы в определённом диапазоне
    print(f"elements in range [3:6]: {items[3:6]}")
    print("-" * 20)

    # Добавьте элемент в список
    items.append("Lemon")
    print(f"after append into end: {items}")

    # Добавьте элемент в середину списка
    mid_index = len(items) // 2
    items.insert(mid_index, "watermelon")
    print(f"after append into middle: {items}")
    print("-" * 20)

    # Посчитайте количество определённых элементов в списке
    count_abacus = items.count("Abacus")
    print(f"abacus count: {count_abacus}")
    print("-" * 20)

    # Скопируйте список, измените первый элеметн и сравните их
    items_copy = items.copy()
    items_copy[0] = "plum"

    print("lists compare:")
    print(f"original: {items}")
    print(f"copy: {items_copy}")

if __name__ == "__main__":
    main()