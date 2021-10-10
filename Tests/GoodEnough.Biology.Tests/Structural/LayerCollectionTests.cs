using GoodEnough.Biology.Structural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using FluentAssertions.Common;

namespace GoodEnough.Biology.Tests.Structural
{
    public class LayerCollectionTests
    {
        [Fact]
        public void IndexInt()
        {
            var lc = new Layer<int>(10, 10);
            var idx = 0;
            for (var height = 0; height < 10; height++)
            {
                for (var width = 0; width < 10; width++)
                {
                    lc[height, width] = idx++;
                }
            }

            lc[5, 6] = 10;

            Assert.Equal(10, lc[5, 6]);
        }

        [Fact]
        public void IndexRange()
        {
            using var lc = new Layer<int>(100, 100);
            var idx = 0;
            for (var height = 0; height < 100; height++)
            {
                for (var width = 0; width < 100; width++)
                {
                    lc[height, width] = idx++;
                }
            }

            lc.SetRange(5..10, 0xff);

            var columns = lc[5..10];

            foreach (var column in columns)
            {
                column.Should().OnlyContain(c => c.IsSameOrEqualTo(0xff));
            }
        }
    }
}
