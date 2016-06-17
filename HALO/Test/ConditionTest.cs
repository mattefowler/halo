using HALO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    class ConditionTest
    {
        [Test]
        public void condition()
        {
            var intProp = new Property<int>(0);

            var isNegative = intProp < 0;
            var isNonpositive = intProp <= 0;
            var isPositive = intProp > 0;
            var isNonnegative = intProp >= 0;
            var isZero = intProp == 0;
            var isNonZero = intProp != 0;

            Assert.False(isNegative.Value);
            Assert.False(isPositive.Value);
            Assert.False(isNonZero.Value);
            Assert.True(isZero.Value);
            Assert.True(isNonnegative.Value);
            Assert.True(isNonpositive.Value);

            intProp.Value = 2;

            Assert.False(isNegative.Value);
            Assert.True(isPositive.Value);
            Assert.True(isNonZero.Value);
            Assert.False(isZero.Value);
            Assert.True(isNonnegative.Value);
            Assert.False(isNonpositive.Value);

            var inRange = (intProp > -1) & (intProp < 1);
            Assert.False(inRange.Value);
            bool inRangeUpdateCalled = false;
            bool? isInRange = null;

            inRange.OnUpdate += (value) =>
            {
                inRangeUpdateCalled = true;
                isInRange = value;
                Assert.That(
                    value ?
                    intProp.Value > -1 && intProp.Value < 1 :
                    intProp.Value <= -1 || intProp.Value >= 1
                );
            };

            intProp.Value = 0;
            Assert.That(inRangeUpdateCalled);
            Assert.True(isInRange);

            isInRange = null;
            inRangeUpdateCalled = false;

            intProp.Value = 2;
            Assert.That(inRangeUpdateCalled);
            Assert.False(isInRange);

        }
    }
}
