using EFCore.MySql.FunctionalTests.Models;
using System.Threading.Tasks;
using Xunit;

namespace EFCore.MySql.FunctionalTests.Tests.Models
{
    public class SequenceTest
    {
        [Fact]
        public async Task SequenceIncrement()
        {
            var sequence1 = new Sequence();
            var sequence2 = new Sequence();
            using (var scope = new AppDbScope())
            {
                scope.AppDb.Sequence.AddRange(new []{sequence1, sequence2});
                await scope.AppDb.SaveChangesAsync();
            }
            Assert.Equal(sequence1.Id + 1, sequence2.Id);
        }
    }
}
