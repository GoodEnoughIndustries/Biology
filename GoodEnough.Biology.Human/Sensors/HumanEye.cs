using GoodEnough.Biology.Organs.Eyes;
using GoodEnough.Biology.Organs.Eyes.Tissues;
using GoodEnough.Biology.Organs.Eyes.Tissues.Retinal;
using GoodEnough.Biology.Sensors;
using GoodEnough.Biology.Structural;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace GoodEnough.Biology.Human.Sensors
{
    public class HumanEye : BaseEye
    {
        private readonly Task sensoryTask;
        private readonly CancellationToken token = CancellationToken.None;
        private readonly Layer<Rgba32> pixelLayer;

        public HumanEye(int width, int height, CancellationToken token = default)
            : base()
        {
            if (width <= 0)
            {
                throw new ArgumentException($"Width of {nameof(HumanEye)} visual input must be greater than 0.");
            }

            if (height <= 0)
            {
                throw new ArgumentException($"Height of {nameof(HumanEye)} visual input must be greater than 0.");
            }

            this.token = token;

            this.Width = width;
            this.Height = height;
            this.pixelLayer = new(this.Width, this.Height);

            // This channel is pixel data which is consumed in the SenseLoop.
            // TODO: Might need to have positional data as well?
            this.SensoryFlow = Channel.CreateUnbounded<(int x, int y, Rgba32 pixel)>(new UnboundedChannelOptions
            {
                SingleReader = false,
                SingleWriter = true,
            });

            // This task is an endless loop that sucks in input from the SensoryFlow channel.
            this.sensoryTask = Task.Run(SenseLoop, this.token);

            this.Retina = new(width: this.Width, height: this.Height);
        }

        public int Width { get; init; }
        public int Height { get; init; }

        public Channel<(int x, int y, Rgba32 pixel)> SensoryFlow { get; init; }

        public async Task SenseLoop()
        {
            this.pixelLayer.Changed += (x, y, p) =>
            {
                if (x == 0 && y == 0)
                {
                    // Console.WriteLine($"[{x},{y}]");
                }
            };

            while (!this.token.IsCancellationRequested)
            {
                await foreach (var (x, y, pixel) in this.SensoryFlow.Reader.ReadAllAsync(this.token))
                {
                    // Only set when needed, otherwise we get allocations and memory growth in tight loops.
                    if (this.pixelLayer[x, y] != pixel)
                    {
                        this.pixelLayer[x, y] = pixel;
                    }

                    // if (x == 0 && y == 0)
                    // {
                    //     var img = new Image<Rgba32>(this.Width, this.Height);
                    //     for (int tx = 0; tx < this.Width; tx++)
                    //     {
                    //         for (int ty = 0; ty < this.Height; ty++)
                    //         {
                    //             img[tx, ty] = this.pixelLayer[tx, ty];
                    //         }
                    //     }
                    // 
                    //     img.SaveAsPng(@"C:\Projects\GoodEnough.Biology\t.png");
                    // }
                }
            }
        }

        public Retina Retina { get; init; }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.token.IsCancellationRequested)
            {
            }

            base.Dispose(disposing);
        }
    }
}
