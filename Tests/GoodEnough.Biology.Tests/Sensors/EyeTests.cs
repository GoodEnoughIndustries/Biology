using GoodEnough.Biology.Human.Sensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
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
            using var eye = new HumanEye(width: image.Width, height: image.Height);

            var writer = eye.SensoryFlow.Writer;
            if (image.TryGetSinglePixelSpan(out var pixels))
            {
                for (var i = 0; i < pixels.Length; i++)
                {
                    writer.TryWrite(pixels[i]);
                }

                writer.TryComplete();
            }
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
