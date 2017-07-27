using System;
using System.Collections.Generic;
using Xunit;
using static Fjak.Uncertainty.Optional;

namespace Fjak.Uncertainty.Tests
{
    public class OptionTest
    {
        [Fact]
        public void IsNone()
        {
            Option<string> none = None;
            Option<string> some = "foo";
            Assert.True(none.IsNone);
            Assert.False(some.IsNone);
        }

        [Fact]
        public void IsSome()
        {
            Option<string> none = None;
            Option<string> some = "foo";
            Assert.False(none.IsSome);
            Assert.True(some.IsSome);
        }

        [Fact]
        public void NoneIsNone()
        {
            Assert.Equal((Option<string>) None, None);
            Assert.Equal(None, (Option<string>) None);
            Assert.Equal((Option<int>) None, None);
            Assert.Equal(None, (Option<int>) None);
        }

        [Fact]
        public void NullableWithoutValueIsNone()
        {
            var o = Option((int?) null);
            Assert.True(o.IsNone);
        }

        [Fact]
        public void NullableWithValueIsReducedToNonNullable()
        {
            int? i = 42;
            var o = Option(i);
            Assert.Equal(42, o);
        }

        [Fact]
        public void OptionInDictionaryWorksAsExpected()
        {
            var v = new Dictionary<Option<int>, string>
            {
                [None] = "none",
                [Some(42)] = "42"
            };
            Assert.Equal("42", v[Some(42)]);
            Assert.Equal("42", v[42]);
            Assert.Equal("none", v[None]);
        }

        [Fact]
        public void OptionOfOptionIsSimplifiedToOption()
        {
            Option<int> i = 42;
            var i2 = Option(i);
            Assert.Equal(42, i2);
        }

        [Fact]
        public void OrDefaultNone()
        {
            Option<string> foo = None;
            Assert.Equal("default", foo.OrDefault("default"));
        }

        [Fact]
        public void OrDefaultSome()
        {
            Option<string> foo = "foo";
            Assert.Equal("foo", foo.OrDefault("default"));
        }

        [Fact]
        public void SelectNoneIsNone()
        {
            Option<string> none = None;
            Assert.True(none.Select(v => "Hello").IsNone);
        }

        [Fact]
        public void SelectSome()
        {
            Option<string> some = "karl";
            Assert.Equal(4, some.Select(v => v.Length));
        }

        [Fact]
        public void SomeObjIsSomeObj()
        {
            Option<string> s = "karl";
            Option<string> s2 = new Some<string>("karl");
            Assert.Equal(s, s2);
            Assert.Equal(s2, s);
        }

        [Fact]
        public void SomePrimitiveIsSomePrimitive()
        {
            Option<int> i = 42;
            Option<int> i2 = Some(42);
            Assert.Equal(i, i2);
            Assert.Equal(i2, i);
        }

        [Fact]
        public void SomeStructIsSomeStruct()
        {
            Option<DateTimeOffset> d = new DateTimeOffset(2017, 7, 27, 20, 37, 42, TimeSpan.FromHours(2));
            Option<DateTimeOffset> d2 =
                new Some<DateTimeOffset>(new DateTimeOffset(2017, 7, 27, 20, 37, 42, TimeSpan.FromHours(2)));
            Assert.Equal(d, d2);
            Assert.Equal(d2, d);
        }

        [Fact]
        public void SomeThrowsOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => Some((int?) null));
        }

        [Fact]
        public void WhereNoneIsNone()
        {
            Option<string> none = None;
            Assert.True(none.Where(s => s.Length < 8).IsNone);
        }

        [Fact]
        public void WhereSomeIsSome()
        {
            Option<string> s = "foo";
            Assert.Equal("foo", s.Where(v => v.Length < 8));
        }

        [Fact]
        public void SelectIsNullSafe()
        {
            Option<string> s = "foo";
            Assert.True(s.Select(_ => (string) null).IsNone);
        }

        [Fact]
        public void Bind()
        {
            Option<int> Foo(string s)
            {
                return s.Length;
            }
            Option<string> v = "foo";
            Assert.Equal(3, v.Bind(Foo));
        }

        [Fact]
        public void BindIsNullSafe()
        {
            Option<int> Foo(string s)
            {
                return null;
            }
            Option<string> v = "foo";
            Assert.Equal(None, v.Bind(Foo));
        }
    }
}