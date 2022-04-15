using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;

namespace CommanderGQL.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform) => platform;

        // 上面这种 等于 下面这样写
        // public Platform OnPlatformAdded([EventMessage] Platform platform){
        //     return platform;
        // }
    }
}