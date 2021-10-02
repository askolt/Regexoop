| Название | Значение |
|---|---|
| Обазятельный | Нет |
| Наследуемый | Нет |
| Тип | bool |
| Значение по умолчанию | true |

Параметр `result` является флагом и отвечает за то, будет ли переменная отдавать результат своей работы или нет.

**Обращение к переменной с параметром ** `result=false` **библиотекой генерируется исключение**

```csharp
var rules = new { 
    pattern = "abc{variable}",
	variable = new {
		pattern = "dbc",
		result = false
	}
};
// Input: abcdbc
// Output: abc
```

Использование параметра `result` в корне правил возможно, но бессмысленно, т.к. ответом будет пустая строка.

```csharp
var rules = new { 
    pattern = "abc{variable}",
	result = false,
	variable = new {
		pattern = "dbc",
		result = false
	}
};
// Input: abcdbc
// Output:
```
