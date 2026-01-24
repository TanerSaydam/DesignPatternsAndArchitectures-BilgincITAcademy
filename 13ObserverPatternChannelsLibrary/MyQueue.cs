using System.Threading.Channels;

namespace _13ObserverPatternChannelsLibrary;

public sealed class MyQueue
{
    public Channel<MyInfo> _channel = Channel.CreateBounded<MyInfo>(new BoundedChannelOptions(1)
    { });
}

public sealed record MyInfo(string Name);
