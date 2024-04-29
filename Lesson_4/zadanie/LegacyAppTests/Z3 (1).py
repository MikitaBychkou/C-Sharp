# -*- coding: utf-8 -*-

''''
Zmodyfikuj funkcje kalkulator tak, żeby uwzględniała te błędy:
TypeError: Jeżli args nie zawiera liczb lub kwargs nie zawiera znaków
ZeroDivisionError
IndexError: Jeśli ilość kwargs jest równa lub większa niż args
MyError(zdefiniowany przez Ciebie): Jeśli znak jest inny niż +-*/ wyświetl komunikat o niepoprawnym znaku
'''


def kalkulator(*args, **kwargs):
    result = args[0]
    temp_liczba=args[1]

    values = kwargs.values()

    for index, dzialanie in enumerate(values):
        match dzialanie:
          case '+':
            result+=args[index+1]
          case '-':
            result-=args[index+1]
          case '*':
            result*=args[index+1]
          case '/':
            result/=args[index+1]
          case _:
              pass
    return result

print(kalkulator(1,3,2,działanie_1='+',działanie_2="-"))