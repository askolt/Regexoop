using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Regexoop;
using Regexoop.src;


namespace ConsoleRegexoop
{
    public class UnitSimpleTest
    {
        [Fact]
        public void SimpleText()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello",
                Start = Rule.Direction.start
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello World").Find();
            Assert.Single(res);
            foreach (string re in res)
            {
                Assert.Equal("Hello", re);
            }

            res = new Regexoop.Regexoop(test).Input("Hello WorHellold").Find();
            Assert.True(res.Count == 2);
            foreach (string re in res)
            {
                Assert.Equal("Hello", re);
            }
        }

        [Fact]
        public void CallVariableAndSimpleText()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world}",
                Start = Rule.Direction.start,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World",
                                                                //Start = Rule.Direction.start
                                              }
                                          }
            };

            List<string> res = new Regexoop.Regexoop(test).Input("Hello World World").Find();
            Assert.Single(res);
            foreach (string re in res)
            {
                Assert.Equal("Hello World", re);
            }
        }

        [Fact]
        public void TwoCallVariableAndSimpleText()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world}",
                Start = Rule.Direction.start,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World"
                                              }
                                          }
            };

            List<string> res = new Regexoop.Regexoop(test).Input("Hello World Hello World").Find();
            Assert.True(res.Count == 2);
            foreach (string re in res)
            {
                Assert.Equal("Hello World", re);
            }
        }

        [Fact]
        public void MultipleCallVariableAndSimpleText()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world} Hello {world}",
                Start = Rule.Direction.start,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World"
                                              }
                                          }
            };

            List<string> res = new Regexoop.Regexoop(test).Input("Hello World Hello World").Find();
            Assert.True(res.Count == 1);
            foreach (string re in res)
            {
                Assert.Equal("Hello World Hello World", re);
            }
        }

        [Fact]
        public void ErrorBadPatternCase1()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world} Hel1lo {world}",
                Start = Rule.Direction.start,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World"
                                              }
                                          }
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello World Hello World").Find();
            Assert.True(res.Count == 0);
            foreach (string re in res)
            {
                Assert.Equal("", re);
            }
        }

        [Fact]
        public void ErrorBadPatternCase2()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world} Hello {world}",
                Start = Rule.Direction.start,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World"
                                              }
                                          }
            };

            List<string> res = new Regexoop.Regexoop(test).Input("He1llo World Hello World").Find();
            Assert.True(res.Count == 0);
            foreach (string re in res)
            {
                Assert.Equal("", re);
            }
        }

        [Fact]
        public void ErrorBadInput()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world} Hello {world}",
                Start = Rule.Direction.start,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World"
                                              }
                                          }
            };

            List<string> res = new Regexoop.Regexoop(test).Input("Hello World Hello Wo1rld").Find();
            //Assert.True(res.Count == 0);
            foreach (string re in res)
            {
                Assert.Equal("", re);
            }
        }

        [Fact]
        public void RecursiveSelfWithoutVariables()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello{root}",
                Start = Rule.Direction.start,
                LoopVariable = 2
            };

            List<string> res = new Regexoop.Regexoop(test).Input("    HelloHelloHello    HelloHelloHello").Find();
            foreach (string re in res)
            {
                Assert.Equal("HelloHelloHello", re);
            }
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void RecursiveErrorCase2()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world}",
                Start = Rule.Direction.start,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World{root}"
                                              }
                                          }
            };

            Action act = () => new Regexoop.Regexoop(test).Input("Hello WorldHello").Find();
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Variable root not found.", exception.Message);
        }

        [Fact]
        public void RecursiveCase1()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello{world}",
                Start = Rule.Direction.start,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = " World{world}",
                                                                LoopVariable = 1
                                              }
                                          }
            };

            List<string> res =  new Regexoop.Regexoop(test).Input("    Hello World World     Hello World World").Find();
            Assert.True(res.Count == 2);
            foreach (string re in res)
            {
                Assert.Equal("Hello World World", re);
            }
        }

        [Fact]
        public void RangeCase1()
        {
            //case 1
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world} Hell[0o] {world}",
                Start = Rule.Direction.start,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World"
                                              }
                                          }
            };

            List<string> res = new Regexoop.Regexoop(test).Input("Hello World Hello World").Find();
            Assert.True(res.Count == 1);
            foreach (string re in res)
            {
                Assert.Equal("Hello World Hello World", re);
            }
        }

        [Fact]
        public void RangeCase2()
        {
            //case 2
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello [1-37-9][0-9][0-9]1",
                Start = Rule.Direction.start
            };

            List<string> res = new Regexoop.Regexoop(test).Input("Hello 3331").Find();
            Assert.True(res.Count == 1);
            foreach (string re in res)
            {
                Assert.Equal("Hello 3331", re);
            }
        }

        [Fact]
        public void RangeCase3()
        {
            //case 3
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "[fdafsdHffah]ello",
                Start = Rule.Direction.start
            };

            List<string> res = new Regexoop.Regexoop(test).Input("Hello World Hello Wo1rld").Find();
            Assert.True(res.Count == 2);
            foreach (string re in res)
            {
                Assert.Equal("Hello", re);
            }
        }

    }
}
