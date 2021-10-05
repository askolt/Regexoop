source: https://stackoverflow.com/questions/46155

``` csharp
var rules = new { 
	pattern = "{name}@{second_level}{first_level}",
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
	},
	name = new {
		pattern = "[a-zA-Z0-9-!#$%&'*+-/=?^_`\{\}|]",
		minLength = 2,
		maxLength = 64,
		symbolsRules = new {
			"[-!#$%&'*+-/=?^_`\{\}|]" = new { repeat: 0 }
		}
	}
};

var er = new EasyRegex(rules);
er = er.Input("only-stuff-secure-mail@domain.net").Find();
Console.WriteLine(er); //only-stuff-secure-mail@domain.net
```