namespace Labo.Validation.Tests
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Labo.Common.Utils;

    using NUnit.Framework;

    [TestFixture]
    public class DefaultPropertyDisplayNameResolverFixture
    {
        public class Customer
        {
            [Display(Name = "First Name")]
            public string Name { get; set; }

            [DisplayName("Email Address")]
            public string Email { get; set; }

            public string Password { get; set; } 
        }

        [Test]
        public void GetDisplayNameThrowsArgumentNullExceptionWhenMemberInfoIsNull()
        {
            DefaultPropertyDisplayNameResolver defaultPropertyDisplayNameResolver = new DefaultPropertyDisplayNameResolver();
            Assert.Throws<ArgumentNullException>(() => defaultPropertyDisplayNameResolver.GetDisplayName(null));
        }

        [Test]
        public void GetDisplayNameShouldReturnDisplayAttributeNamePropertyValue()
        {
            DefaultPropertyDisplayNameResolver defaultPropertyDisplayNameResolver = new DefaultPropertyDisplayNameResolver();
            Assert.AreEqual("First Name", defaultPropertyDisplayNameResolver.GetDisplayName(LinqUtils.GetMemberInfo<Customer, string>(x => x.Name)));
        }

        [Test]
        public void GetDisplayNameShouldReturnDisplayNameAttributeNamePropertyValue()
        {
            DefaultPropertyDisplayNameResolver defaultPropertyDisplayNameResolver = new DefaultPropertyDisplayNameResolver();
            Assert.AreEqual("Email Address", defaultPropertyDisplayNameResolver.GetDisplayName(LinqUtils.GetMemberInfo<Customer, string>(x => x.Email)));
        }

        [Test]
        public void GetDisplayNameShouldReturnMemberNameIfNoDisplayAttributeIsNotUsed()
        {
            DefaultPropertyDisplayNameResolver defaultPropertyDisplayNameResolver = new DefaultPropertyDisplayNameResolver();
            Assert.AreEqual("Password", defaultPropertyDisplayNameResolver.GetDisplayName(LinqUtils.GetMemberInfo<Customer, string>(x => x.Password)));
        }
    }
}
