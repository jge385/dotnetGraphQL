using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input, 
            [ScopedService] AppDbContext context,
            // topic event sender & cancellation token are used for subscription
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var platform = new Platform{
                Name = input.Name
            };

            context.Platforms.Add(platform);
            // cancellation token is used for subscription
            await context.SaveChangesAsync(cancellationToken);

            // this is used for subscription
            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);

            return new AddPlatformPayload(platform);
        }


        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input, [ScopedService] AppDbContext context)
        {
            var command = new Command{
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            context.Commands.Add(command);
            await context.SaveChangesAsync();

            return new AddCommandPayload(command);
        }


        [UseDbContext(typeof(AppDbContext))]
        public async Task<DeletePlatformPayload> DeletePlatformAsync(DeletePlatformInput input, [ScopedService] AppDbContext context)
        {

            var platform = context.Platforms.FirstOrDefault(p => p.Id == input.Id);

            context.Platforms.Remove(platform);
            await context.SaveChangesAsync();

            return new DeletePlatformPayload(platform);
        }


        [UseDbContext(typeof(AppDbContext))]
        public async Task<UpdatePlatformPayload> UpdatePlatformAsync(UpdatePlatformInput input, [ScopedService] AppDbContext context)
        {

            var platform = context.Platforms.FirstOrDefault(p => p.Id == input.Id);

            platform.Name = input.Name;

            context.Update(platform);
 
            await context.SaveChangesAsync();

            return new UpdatePlatformPayload(platform);
        }
    }
}