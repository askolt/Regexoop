Domain validation

``` csharp
var rules = new { 
	pattern = "{second_level}{first_level}",
	start = "end",
	first_level = new {
		pattern = "[a-z]",
		minLength = 2,
		maxLength = 3,
    },
	second_level = new {
		pattern = "[a-zA-Z0-9-].",
		minLength = 2,
		maxLength = 63,
		maxRepeat = 5,
		minRepeat = 0,
		symbolsRules = new {
			"-" = new { repeat: 0 }
		}
	}
};

var er = new EasyRegex(rules).Input("https://docs.microsoft.com").Find()
Console.WriteLine(er); //docs.microsoft.com
```