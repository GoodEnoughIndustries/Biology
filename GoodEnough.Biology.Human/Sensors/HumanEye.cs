using GoodEnough.Biology.Organs.Eyes;
using GoodEnough.Biology.Organs.Eyes.Tissues;
using GoodEnough.Biology.Sensors;
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
    public class HumanEye : BaseEye, IDisposable
    {
        private readonly Task sensoryTask;
        private CancellationToken token = CancellationToken.None;
        private readonly CancellationTokenSource cts = new();

        private int currentColumn;
        private int currentRow;

        public HumanEye(int width, int height)
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

            this.token = this.cts.Token;

            this.Width = width;
            this.Height = height;

            // This channel is pixel data which is consumed in the SenseLoop.
            // TODO: Might need to have positional data as well?
            this.SensoryFlow = Channel.CreateUnbounded<Rgba32>(new UnboundedChannelOptions
            {
                SingleReader = false,
                SingleWriter = true,
            });

            // This task is an endless loop that sucks in input from the SensoryFlow channel.
            this.sensoryTask = Task.Run(SenseLoop);

            this.Retina = new (width: this.Width, height: this.Height);
        }

        public int Width { get; init; }
        public int Height { get; init; }

        public Channel<Rgba32> SensoryFlow { get; init; }

        public async Task SenseLoop()
        {
            while (!this.token.IsCancellationRequested)
            {
                await foreach (var value in this.SensoryFlow.Reader.ReadAllAsync(this.token))
                {
                }
            }
        }

        public Retina Retina { get; init; }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.token.IsCancellationRequested)
            {
                this.cts.Cancel();
                this.cts.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
