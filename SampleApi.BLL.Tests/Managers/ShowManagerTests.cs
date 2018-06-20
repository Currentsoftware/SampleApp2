using System;
using System.Collections.Generic;
using System.Diagnostics;
using Moq;
using SampleApi.BLL.Managers;
using SampleApi.Common.Data;
using SampleApi.Common.Entities;
using SampleApi.Common.Exceptions;
using Xunit;

namespace SampleApi.BLL.Tests.Managers
{
    public static class ShowManagerTests
    {
        [Fact]
        public static void GetShowsShouldReturnProperCollection()
        {
            var mockProvider = new Mock<ITVMazeDataProvider>();
            var shows = MockedShows();
            var members1 = MockedCastMembers(1);
            var members2 = MockedCastMembers(2);

            mockProvider.Setup(s => s.GetShows(0)).Returns(shows);
            mockProvider.Setup(s => s.GetCastMembers(1)).Returns(members1);
            mockProvider.Setup(s => s.GetCastMembers(2)).Returns(members2);

            var manager = new ShowManager(mockProvider.Object);

            var returnedShows = manager.GetShows(0);

            Assert.NotEmpty(returnedShows);

            var ageSortedProperly = false;

            foreach (var s in returnedShows)
            {
                var a1 = s.Cast[0];
                var a2 = s.Cast[1];

                ageSortedProperly = a2.Birthdate > a1.Birthdate;
                break;
            }

            Assert.True(ageSortedProperly);
        }

        [Fact]
        public static void GetShowsShouldPauzeWhen429Occurrs()
        {
            var mockProvider = new Mock<ITVMazeDataProvider>();
            var shows = MockedShows();
            var members1 = MockedCastMembers(1);
            var members2 = MockedCastMembers(2);
            var count = 0;

            mockProvider.Setup(s => s.GetShows(0)).Returns(shows);
            mockProvider.Setup(s => s.GetCastMembers(1)).Callback(() =>
            {
                count++;
                if (count == 1)
                {
                    throw new DataSourceOverloadException();
                }
            }).Returns(members1);
            mockProvider.Setup(s => s.GetCastMembers(2)).Returns(members2);

            var manager = new ShowManager(mockProvider.Object);

            var stopwatch = Stopwatch.StartNew();

            var returnedShows = manager.GetShows(0);

            var duration = stopwatch.ElapsedMilliseconds;

            Assert.NotEmpty(returnedShows);
            Assert.True(duration > 5000);

            var ageSortedProperly = false;

            foreach (var s in returnedShows)
            {
                var a1 = s.Cast[0];
                var a2 = s.Cast[1];

                ageSortedProperly = a2.Birthdate > a1.Birthdate;
                break;
            }

            Assert.True(ageSortedProperly);
        }

        private static List<Show> MockedShows()
        {
            var shows = new List<Show>();

            var show1 = new Show()
            {
                Id = 1,
                Name = "The Foobar Show"
            };
            shows.Add(show1);

            var show2 = new Show()
            {
                Id = 2,
                Name = "The Baz Show"
            };
            shows.Add(show2);

            return shows;
        }

        private static List<CastMember> MockedCastMembers(int showId)
        {
            var members = new List<CastMember>();

            if (showId == 1)
            {
                var member1 = new CastMember()
                {
                    Id = 1,
                    Birthdate = new DateTime(1967, 4, 12),
                    Name = "Foo Bar"
                };
                members.Add(member1);

                var member2 = new CastMember()
                {
                    Id = 2,
                    Birthdate = new DateTime(1948, 8, 21),
                    Name = "Lorem ipsum"
                };
                members.Add(member2);
            }
            else
            {
                var member1 = new CastMember()
                {
                    Id = 1,
                    Birthdate = new DateTime(1984, 12, 9),
                    Name = "Sit Amet"
                };
                members.Add(member1);

                var member2 = new CastMember()
                {
                    Id = 2,
                    Birthdate = new DateTime(1993, 6, 5),
                    Name = "Dolor Sit"
                };
                members.Add(member2);
            }

            return members;
        }
    }
}
