using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Regexoop;
using Regexoop.src;

namespace testsRegexoop
{
    public class StartEndTest
    {
        [Fact]
        public void EscapeSymbolsCase1()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello \nWorld",
                Start = Rule.Direction.start
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello \nWorld").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello \nWorld", re);
            }
            Assert.Single(res);
        }

        [Fact]
        public void EscapeSymbolsCase2()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello \\*World",
                Start = Rule.Direction.start
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello \\*World Hello \\*WorldHello \\*World").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello \\*World", re);
            }
            Assert.True(res.Count == 3);
        }

        [Fact]
        public void StartEndCase1()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello",
                Start = Rule.Direction.end
            };
            test.PrepareRule();
            Assert.Equal("olleH", test.Pattern);
        }

        [Fact]
        public void StartEndCase2()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world}",
                Start = Rule.Direction.end
            };
            test.PrepareRule();
            Assert.Equal("{world} olleH", test.Pattern);
        }

        [Fact]
        public void StartEndCase3()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world}[0-9123]     \n {world}  olleH",
                Start = Rule.Direction.end
            };
            test.PrepareRule();
            Assert.Equal("Hello  {world} \n     [0-9123]{world} olleH", test.Pattern);
        }

        [Fact]
        public void StartEndCase4()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world}",
                Start = Rule.Direction.end,
                Variables = new List<Rule>()
                {
                    new BasicRule()
                    {
                        Name = "world",
                        Pattern = "test",
                    }
                }
            };
            test.PrepareRule();
            test.Variables[0].PrepareRule();
            Assert.Equal("{world} olleH", test.Pattern);
            Assert.Equal("tset", test.Variables[0].Pattern);
        }

        [Fact]
        public void StartEndCase5()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world}",
                Start = Rule.Direction.end,
                Variables = new List<Rule>()
                {
                    new BasicRule()
                    {
                        Name = "world",
                        Pattern = "test"
                    }
                }
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello test").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello test", re);
            }
            Assert.Single(res);
        }

        [Fact]
        public void RangeDirectionEndCase1()
        {
            //case 1
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world} Hell[0o] {world} Mur",
                Start = Rule.Direction.end,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World",
                                                                Start = Rule.Direction.start, //no effect
                                              }
                                          }
            };

            List<string> res = new Regexoop.Regexoop(test).Input("Hello World Hello World Mur").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello World Hello World Mur", re);
            }
            Assert.True(res.Count == 1);
        }

        [Fact]
        public void RangeDirectionEndCase2()
        {
            //case 1
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello {world} Hell[0o] {world} Mur",
                Start = Rule.Direction.end,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = "World",
                                              }
                                          }
            };

            List<string> res = new Regexoop.Regexoop(test).Input("  Hello World Hello World Mur   ").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello World Hello World Mur", re);
            }
            Assert.True(res.Count == 1);
        }

        [Fact]
        public void DirectionRecursiveCase1()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello{world}",
                Start = Rule.Direction.end,
                Variables = new List<Rule> {
                                              new BasicRule() { Name = "world",
                                                                Pattern = " World{world}",
                                                                LoopVariable = 1,
                                                                Start = Rule.Direction.end,
                                              }
                                          }
            };

            List<string> res = new Regexoop.Regexoop(test).Input("Hello World World     Hello World World").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello World World", re);
            }
            Assert.True(res.Count == 2);
        }
    }
}
