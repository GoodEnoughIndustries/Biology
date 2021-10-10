using GoodEnough.Biology.Human.Sensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GoodEnough.Biology.TestApp
{
    public class Program
    {
        public static async Task Main()
        {
            using var imageStream = new FileStream(@"C:\Projects\GoodEnough.Biology\Assets\tanuki.jpg", FileMode.Open);
            using var image = Image.Load<Rgba32>(imageStream);
            using var cts = new CancellationTokenSource();

            using var eye = new HumanEye(width: image.Width, height: image.Height, cts.Token);

            var writer = eye.SensoryFlow.Writer;

            while (true)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    for (var x = 0; x < image.Width; x++)
                    {
                        await writer.WriteAsync((x, y, image[x, y]));
                    }
                }
            }
            //Task.Delay(50000).Wait();
            //
            //cts.Cancel();
            //
            //Span<Rgba32> retinaSpan = MemoryMarshal.Cast<byte, Rgba32>(eye.Retina.Grid.GetSpan());

        }
    }
}
