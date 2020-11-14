using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace GoodEnough.Biology.Abstractions.Sensors
{
    public interface ISensor : IComponent
    {
        Task Sense(ChannelReader<byte> information, CancellationToken token = default);
    }
}
