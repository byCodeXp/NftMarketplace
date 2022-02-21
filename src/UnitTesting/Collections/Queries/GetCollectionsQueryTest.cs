using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Collections;
using Application.Features.Collections.Queries;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace UnitTesting.Collections.Queries;

public class GetCollectionsQueryTest
{
    private readonly IUnitOfWork _unitOfWork;

    private static ICollection<CollectionEntity> TestCollections => new []
    {
        new CollectionEntity { Name = "Collection #1" },
        new CollectionEntity { Name = "Collection #2" },
        new CollectionEntity { Name = "Collection #3" },
        new CollectionEntity { Name = "Collection #4" },
        new CollectionEntity { Name = "Collection #5" },
    };

    public GetCollectionsQueryTest()
    {
        var builder = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("GetCollectionsQueryTest");
        var context = new DataContext(builder.Options);
        _unitOfWork = new UnitOfWork(context);

        foreach (var collection in TestCollections)
        {
            _unitOfWork.CollectionRepository.AddCollection(collection);
        }

        _unitOfWork.Completed(CancellationToken.None);
    }

    [Test]
    public async Task TotalCount()
    {
        var query = new GetCollectionsQuery
        {
            Page = 1,
            PerPage = 2
        };

        var handler = new GetCollectionsHandler(_unitOfWork);

        CollectionsVm response = await handler.Handle(query, CancellationToken.None);
        
        Assert.AreEqual(5, response.TotalCount, "Total count must be 5");
    }

    [Test]
    public async Task CountItemsCaseFirst()
    {
        var query = new GetCollectionsQuery
        {
            Page = 1,
            PerPage = 4
        };
        
        var handler = new GetCollectionsHandler(_unitOfWork);
        
        CollectionsVm response = await handler.Handle(query, CancellationToken.None);
        
        Assert.AreEqual(4, response.Collections.Count, "Collections count must be 4");
    }
    
    [Test]
    public async Task CountItemsCaseSecond()
    {
        var query = new GetCollectionsQuery
        {
            Page = 2,
            PerPage = 4
        };
        
        var handler = new GetCollectionsHandler(_unitOfWork);
        
        CollectionsVm response = await handler.Handle(query, CancellationToken.None);
        
        Assert.AreEqual(1, response.Collections.Count, "Collections count must be 1");
    }
}