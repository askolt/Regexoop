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
    public class ResultTest
    {
        [Fact]
        public void ResultGetAll()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello{one}{two}",
                Variables = new List<Rule> { 
                    new BasicRule() { Name = "one", Pattern = "Hello{three}"
                        , Variables = new List<Rule> {
                            new BasicRule() { Name = "three", Pattern = "Hello" }
                        } },
                    new BasicRule() { Name = "two", Pattern = "Hello" },
                }
            };
            List<string> res = new Regexoop.Regexoop(test).Input("fdsafdasfdsa dasfasdfaHelloHelloHelloHello fdafa3").Find();
            foreach (string re in res)
            {
                Assert.Equal("HelloHelloHelloHello", re);
            }
            Assert.Single(res);
        }

        [Fact]
        public void ResultFalseOneAllCase1()
        {
            Rule test = new BasicRule()
            {
                Name = "root",
                Pattern = "Hello{one}{two}",
                Variables = new List<Rule> {
                    new BasicRule() { Name = "one", Pattern = " awesome {three}", Result = false
                        , Variables = new List<Rule> {
                            new BasicRule() { Name = "three", Pattern = "man" }
                        } },
                    new BasicRule() { Name = "two", Pattern = "!" },
                }
            };
            List<string> res = new Regexoop.Regexoop(test).Input("fdsafdasfdsa dasfasdfa Hello awesome man!fdafa3").Find();
            foreach (string re in res)
            {
                Assert.Equal("Hello!", re);
            }
            Assert.Single(res);
        }


    }
}
