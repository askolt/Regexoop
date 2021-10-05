| Название | Значение |
|---|---|
| Обазятелный | Нет |
| Наследуемый | Нет |
| Тип | bool |
| Значение по умолчанию | false |

Позволяет игнорировать невидимые символы, включая символы табуляци, пробела и так далее перемещая курсор вперёд.

```csharp
var rules = new { 
    pattern = "[a-z]",
	trim = true
};
// Input: s u p e r p a s w o r d
// Output: superpasword
// Input: 	_невидимые символы табуляции_ 	s u p e r p a s w o r d
// Output: superpasword
```
