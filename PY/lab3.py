import pandas as pd

print("чтение с параметрами")
# sheet_name='' лист
# usecols="A,D" - Дата и Выручка
# skiprows=1 - пропускаем первую строку
# nrows=3 - читаем только 3 строки
# names - задаем свои имена колонка
df_params = pd.read_excel('a.xlsx', 
                          sheet_name='Продажи', 
                          usecols="A,D", 
                          skiprows=1, 
                          nrows=3,
                          names=['Дата операции', 'Доход'])
print(df_params)


print("чтение сразу нескольких листов")
# список листов
all_sheets = pd.read_excel('a.xlsx', sheet_name=['Продажи', 'Расходы'])

print("\nДанные с листа 'Расходы':")
print(all_sheets['Расходы'].head()) #первые строки второго листа