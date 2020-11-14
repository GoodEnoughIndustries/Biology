using GoodEnough.Biology.Abstractions.Sensors;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace GoodEnough.Biology.Sensors
{
    public class BaseSensor : ISensor, IDisposable
    {
        private bool disposedValue;

        public Guid Id { get; } = Guid.NewGuid();

        public virtual Task Sense(ChannelReader<byte> information, CancellationToken token = default)
            => throw new NotImplementedException();

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
