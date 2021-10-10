using GoodEnough.Biology.Human.Sensors;
using SixLabors.ImageSharp;
using FluentAssertions;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Xunit;

namespace GoodEnough.Biology.Tests.Sensors
{
    public class EyeTests : IDisposable
    {
        private bool disposedValue;
        private BodyBuilder builder;

        public EyeTests()
        {
        }

        [Fact]
        public void FirstTest()
        {
            using var imageStream = new FileStream(@"C:\Projects\GoodEnough.Biology\Assets\tanuki.jpg", FileMode.Open);
            using var image = Image.Load<Rgba32>(imageStream);
            using var cts = new CancellationTokenSource();

            using var eye = new HumanEye(width: image.Width, height: image.Height, cts.Token);

            var writer = eye.SensoryFlow.Writer;

            if (image.TryGetSinglePixelSpan(out var pixels))
            {
                for (var i = 0; i < pixels.Length; i++)
                {
                    // writer.TryWrite((pixels[i]);
                }

                //writer.TryComplete();
            }
            //Task.Delay(50000).Wait();
            //
            //cts.Cancel();
            //
            //Span<Rgba32> retinaSpan = MemoryMarshal.Cast<byte, Rgba32>(eye.Retina.Grid.GetSpan());
            //
            //retinaSpan.ToArray().Should().ContainInOrder(pixels.ToArray());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }

                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
