# Метод Input и взаимодейсвие с Find(), Each(), Check()

В этой статье описана принципы добавления, хранения и удаления текста для работы. Для общего ознакомления читайте статью *Режимы работы*.

## Input()
Метод `Input()` принимает строку для поиска текста по шаблону. 

```csharp
var rules = new { 
    pattern = "abc"
};
var res = EasyRegex(rules).Input("111111abc111111").Find();
// storageText: 111111abc111111
// res: abc
```

В качестве аргумента принимает один строковой аргумент. Данные помещаются в глобальный параметр `inputText`.  Метод `Input` можно использовать несколько раз. Последние добавленные значения будут добавлены в конец. 

```csharp
var rules = new { 
    pattern = "aaabbccc"
};
var res = EasyRegex(rules).Input("aaa").Input("bb").Input("ccc").Find();
// storageText: aaabbccc
// res: aaabbccc
```

> Данный метод отвечает только за добавление текста в хранилище.

## Find()

После применения метода `Find()`, происходит поиск текста во внутреннем хранилище `inputText`.  По завершению работы внутреннее хранилище очищается.

## Each()

В этом режиме библиотека возвращает первый результат и замораживает своё состояние до следующего вызова метода `Each()`. Текст в `inputText` удаляется по первое совпадение. 

```csharp
var rules = new { 
    pattern = "aaa",
	caseInsensitvie = true
};
var res = EasyRegex(rules).Input("aaahuehueAAAaaa");
var foundText;
while(findText = er.Each())
{
	Console.WriteLine(foundText);
}
// storageText: aaahuehueAAAaaa
// res: aaa
// storageText: huehueAAAaaa
// res: AAA
// storageText: aaa
// res: aaa
```

Данный метод удобно использовать для больших данных. Ведь метод `Input()` можно использовать как перед итерацией `Each()`, так и после. Это экономит память. 

## Check()

Аналогично методу `Find()`, после своей работы очищает `inputText`.