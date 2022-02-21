using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Collections;
using Application.Features.Collections.Queries;
using Application.Types;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace UnitTesting.Collections.Queries;

public class FilterCollectionsQueryTest
{
    private static ICollection<CollectionEntity> TestCollections => new []
    {
        new CollectionEntity { Name = "Collection #1" },
        new CollectionEntity { Name = "Collection #2" },
        new CollectionEntity { Name = "Collection #3" },
        new CollectionEntity { Name = "Collection #4" },
        new CollectionEntity { Name = "Collection #5" },
    };
    
    private readonly IUnitOfWork _unitOfWork;
    
    public FilterCollectionsQueryTest()
    {
        var builder = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("FilterCollectionsQueryTest");
        var context = new DataContext(builder.Options);
        _unitOfWork = new UnitOfWork(context);

        foreach (var collection in TestCollections)
        {
            _unitOfWork.CollectionRepository.AddCollection(collection);
        }

        _unitOfWork.Completed(CancellationToken.None);
    }

    [Test]
    public async Task CountItemsCaseFirst()
    {
        var query = new FilterCollectionsQuery
        {
            Search = "#",
            Sort = SortCollections.Alphabet,
            Reverse = true,
            Page = 1,
            PerPage = 5
        };

        var handler = new FilterCollectionsHandler(_unitOfWork);

        CollectionsVm response = await handler.Handle(query, CancellationToken.None);
        
        Assert.AreEqual(5, response.Collections.Count, "Collection count must be 5");
    }
    
    [Test]
    public async Task CountItemsCaseSecond()
    {
        var query = new FilterCollectionsQuery
        {
            Search = "3",
            Sort = SortCollections.Date,
            Reverse = false,
            Page = 1,
            PerPage = 3
        };

        var handler = new FilterCollectionsHandler(_unitOfWork);

        CollectionsVm response = await handler.Handle(query, CancellationToken.None);
        
        Assert.AreEqual(1, response.Collections.Count, "Collection count must be 1");
    }
    
    [Test]
    public async Task SortByAlphabetAscending()
    {
        var query = new FilterCollectionsQuery
        {
            Sort = SortCollections.Alphabet,
            Reverse = true,
            Page = 1,
            PerPage = 5
        };

        var handler = new FilterCollectionsHandler(_unitOfWork);

        CollectionsVm response = await handler.Handle(query, CancellationToken.None);

        var unsorted = response.Collections.ToList();
        var sorted = response.Collections.OrderBy(x => x.Name).ToList();

        int count = response.Collections.Count;
        
        for (int i = 0; i < count; i++)
        {
            Assert.AreEqual(sorted[i].Id, unsorted[i].Id);
        }
    }
    
    [Test]
    public async Task SortByAlphabetDescending()
    {
        var query = new FilterCollectionsQuery
        {
            Sort = SortCollections.Alphabet,
            Reverse = false,
            Page = 1,
            PerPage = 5
        };

        var handler = new FilterCollectionsHandler(_unitOfWork);

        CollectionsVm response = await handler.Handle(query, CancellationToken.None);

        var unsorted = response.Collections.ToList();
        var sorted = response.Collections.OrderByDescending(x => x.Name).ToList();

        int count = response.Collections.Count;
        
        for (int i = 0; i < count; i++)
        {
            Assert.AreEqual(sorted[i].Id, unsorted[i].Id);
        }
    }
}