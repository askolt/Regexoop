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
    public class RepeatTest
    {
        [Fact]
        public void RepeatCase1()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello ",
                Repeat = 1
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello Hello Hello Hello Hello Hello Hello ").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello Hello ", re);
            }
            Assert.True(res.Count == 3);
        }

        [Fact]
        public void RepeatCase2()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello ",
                MinRepeat = 1
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello Hello Hello Hello Hello Hello Hello ").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello Hello Hello Hello Hello Hello Hello ", re);
            }
            Assert.True(res.Count == 1);
        }

        [Fact]
        public void RepeatCase3()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello ",
                MaxRepeat = 2
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello Hello Hello Hello Hello Hello Hello ").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello Hello Hello ", re);
            }
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void RepeatCase4()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello ",
                Repeat = 2
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello Hello Hello Hello Hello Hello Hello ").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello Hello Hello ", re);
            }
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void RepeatCase5()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello ",
                MinRepeat = 1,
                MaxRepeat = 4,
            };
            List<string> res = new Regexoop.Regexoop(test).Input("Hello Hello NONONONO Hello Hello Hello NONONONO").Find();
            Assert.Collection<string>(res,
                elem1 => Assert.Equal("Hello Hello ", elem1),
                elem2 => Assert.Equal("Hello Hello Hello ", elem2)
            );

            /*foreach (string re in res)
            {
                Assert.Equal("Hello Hello Hello ", re);
            }*/
            Assert.True(res.Count == 2);
        }

        [Fact]
        public void ResultWithRepatCase1()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello{one}{two}",
                Variables = new List<Rule> {
                    new BasicRule() { Name = "one", Pattern = " awesome {three}", Result = false, MinRepeat = 1
                        , Variables = new List<Rule> {
                            new BasicRule() { Name = "three", Pattern = "man" }
                        } },
                    new BasicRule() { Name = "two", Pattern = "!", Repeat = 4 },
                }
            };
            List<string> res = new Regexoop.Regexoop(test).Input("fdsafdasfdsa dasfasdfa Hello awesome man awesome man!!!!!!fdafa3").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello!!!!!", re);
            }
            Assert.Single(res);
        }

    }
}
