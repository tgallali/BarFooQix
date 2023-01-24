namespace FooBarQix
{
    public class FooBarQixShould
    {

        static List<Tuple<int, string>> rules = new List<Tuple<int, string>>()
                                {
                                    new Tuple<int, string>(3, "Foo"),
                                    new Tuple<int, string>(5, "Bar"),
                                    new Tuple<int, string>(7, "Qix"),

                                };


        [Fact]
        public void returns_string()
        {
            Assert.IsType<string>(FooBarQix(1));
        }

        [Fact]
        public void returns_foo_when_divisible_by_3()
        {
            Assert.Equal("Foo", FooBarQix(3));
            Assert.Equal("Foo", FooBarQix(6));
            Assert.Equal("Foo", FooBarQix(9));
        }

        [Fact]
        public void returns_bar_when_divisible_by_5()
        {
            Assert.Equal("Bar", FooBarQix(5));
            Assert.Equal("Bar", FooBarQix(10));

        }

        [Fact]
        public void returns_qix_when_divisible_by_7()
        {
            Assert.Equal("Qix", FooBarQix(7));
            Assert.Equal("Qix", FooBarQix(14));
        }

        [Fact]
        public void returns_input_otherwise()
        {
            Assert.Equal("1", FooBarQix(1));
            Assert.Equal("4", FooBarQix(4));
        }


        [Fact]
        public void returns_combinaison()
        {
            Assert.Equal("FooBar", FooBarQix(3 * 5));
            Assert.Equal("FooQix", FooBarQix(3 * 7));
            Assert.Equal("BarQix", FooBarQix(5 * 7));
            Assert.Equal("FooBarQix", FooBarQix(3 * 5 * 7));
        }


        private string FooBarQix(int v)
        {


           
            //return FooBarQixRecursive(v, string.Empty , false, rules);
            //return FooBarQixOC(v, new List<Tuple<int, string>>() 
            //                    {
            //                        new Tuple<int, string>(3, "Foo"),
            //                        new Tuple<int, string>(5, "Bar"),
            //                        new Tuple<int, string>(7, "Qix"),

            //                    });

            return FooBarQixStream(v, rules);
        }

        private string FooBarQixV1(int v)
        {
            string result = string.Empty;
            bool ruleMatched = false;

            if (v % 3 == 0)
            {
                result += "Foo";
                ruleMatched = true;
            }

            if (v % 5 == 0)
            {
                result += "Bar";
                ruleMatched = true;
            }

            if (v % 7 == 0)
            {
                result += "Qix";
                ruleMatched = true;
            }

            if (ruleMatched)
                return result;

            return v.ToString();

        }

        private string FooBarQixOC(int v, List<Tuple<int, string>> rules)
        {

            string result = string.Empty;
            bool ruleMatched = false;

            rules.ForEach(r =>
                {
                    if (v % r.Item1 == 0)
                    {
                        result += r.Item2;
                        ruleMatched = true;
                    }
                }
            );

            if (ruleMatched)
                return result;

            return v.ToString();

        }

        private string FooBarQixRecursive(int v, string result , bool ruleMatched, List<Tuple<int, string>> rules)
        {

            if (!rules.Any())
            {
                if (ruleMatched)
                    return result;

                return v.ToString();
            }

            var firstRule = rules.First();
            if (v % firstRule.Item1 == 0)
            {
                return FooBarQixRecursive(v, result + firstRule.Item2, true, rules.GetRange(1, rules.Count - 1));
            }

            return FooBarQixRecursive(v, result, ruleMatched, rules.GetRange(1, rules.Count - 1));
        }

        private string FooBarQixStream(int v, List<Tuple<int, string>> rules)
        {
            return rules.Where(rule => v % rule.Item1 == 0)
                        .Select(r => r.Item2)
                        .DefaultIfEmpty(v.ToString())
                        .Aggregate("", Concat());
        }

        private static Func<string, string, string> Concat()
        {
            return (acc, x) => acc + x;
        }
    }
}