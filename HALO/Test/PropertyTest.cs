using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace HALO.test
{
    [TestFixture]
    public class PropertyTest
    {
        [TestCase]
        public void Property()
        {
            var intProp = new Property<int>("test", 0);
            Assert.AreEqual(intProp.Value, 0);
            Assert.AreEqual(intProp.Name, "test");
            intProp.Value = 1;
            Assert.AreEqual(intProp.Value, 1);
            intProp = new Property<int>(0);
            Assert.AreEqual(intProp.Name, null);
            Assert.AreEqual(intProp.Value, 0);
            intProp.Value = 2;
            Assert.AreEqual(intProp.Value, 2);
        }

        [TestCase]
        public void PropertySubscription()
        {
            var intProp = new Property<int>(0);
            bool called = false;
            Action<int> subscriber = (arg) =>
            {
                Assert.AreEqual(arg, intProp.Value);
                called = true;
            };
            intProp.OnUpdate += subscriber;
            Assert.That(!called);
            intProp.Value = 1;
            Assert.That(called);
        }
    }
}
