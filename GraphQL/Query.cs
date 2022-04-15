using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;

namespace CommanderGQL.GraphQL
{
    public class Query
    {
        // [service] is from the hotchocolate framework
        // 这样写会有 parallel platforms problem 即 cannot be multithreaded
        // public IQueryable<Platform> GetPlatform([Service] AppDbContext context)
        // {
        //     return context.Platforms;
        // }


        // 这样解决 parallel platforms problem  即 can be multithreaded 
        // this particular method has to get a dbcontext from the pool, execute the query and then return
        // scopedservice = service created once per client request
        //[UseDbContext(typeof(AppDbContext))]
        // Use projection = ask to also get child objects information 
        // know child objects by the diagram (foreign key)


        // do not need use projection if we use Type for documentation
        // need this if we use annotation for documentation 
        //[UseProjection]



        // this is used to query platform object
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
        {
            return context.Platforms;
        }



        // 这样解决 parallel platforms problem  即 can be multithreaded 
        // this particular method has to get a dbcontext from the pool, execute the query and then return
        // scopedservice = service created once per client request
        // [UseDbContext(typeof(AppDbContext))]
        // Use projection = ask to also get child objects information 
        // know child objects by the diagram (foreign key)



        // do not need use projection if we use Type for documentation
        // need this if we use annotation for documentation 
        //[UseProjection]



        // this is used to query command object
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommand([ScopedService] AppDbContext context)
        {
            return context.Commands;
        }
    }
}