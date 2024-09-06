using System.Collections.Generic;
using System.Linq;
using Xunit;
using WordFinderApp;

namespace WordFinderTest
{
    public class WordFinderTests
    {
        [Fact]
        public void Find_ShouldReturnTop10Words_WhenWordsAreFound()
        {
            // Arrange
            var matrix = new List<string>
            {
                "wwfcolddc",
                "dochillpo",
                "frwindwtl",
                "vcsnowkxd",
                "shfcegdfb",
                "sigogfddg",
                "ululwindt",
                "llldlrlrl"
            };

            var wordStream = new List<string> { "cold", "chill", "wind", "snow", "cold", "wind", "cold" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Equal("cold", result[0]);
            Assert.Equal("chill", result[1]);
            Assert.Equal("wind", result[2]);
            Assert.Equal("snow", result[3]);
        }

        [Fact]
        public void Find_ShouldReturnEmpty_WhenNoWordsAreFound()
        {
            // Arrange
            var matrix = new List<string>
            {
                "cold",
                "chill",
                "wind",
                "snow"
            };

            var wordStream = new List<string> { "hot", "summer", "rain" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Find_ShouldReturnEmpty_WhenMatrixIsEmpty()
        {
            // Arrange
            var matrix = new List<string>();
            var wordStream = new List<string> { "cold", "chill", "wind", "snow" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Find_ShouldReturnEmpty_WhenWordStreamIsEmpty()
        {
            // Arrange
            var matrix = new List<string>
            {
                "cold",
                "chill",
                "wind",
                "snow"
            };

            var wordStream = new List<string>();
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Find_ShouldHandleCaseSensitivityCorrectly()
        {
            // Arrange
            var matrix = new List<string>
            {
                "cold",
                "Chill",
                "Wind",
                "snow"
            };

            var wordStream = new List<string> { "cold", "chill", "wind", "snow" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("cold", result[0]);
            Assert.Equal("snow", result[1]);
        }

        [Fact]
        public void Find_ShouldOnlyCountWordsOnce_WhenWordsAppearMultipleTimesInStream()
        {
            // Arrange
            var matrix = new List<string>
            {
                "coldc",
                "chillh",
                "windi",
                "snowl",
                "sgtnj"
            };

            var wordStream = new List<string> { "cold", "cold", "cold", "chill" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("chill", result[0]);
            Assert.Equal("cold", result[1]);
        }

        [Fact]
        public void Find_ShouldWorkWithMatrixContainingSingleRow()
        {
            // Arrange
            var matrix = new List<string>
            {
                "sdgcoldfdchillfsnowfdwind"
            };

            var wordStream = new List<string> { "cold", "chill", "snow", "wind" };
            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.Equal(4, result.Count);
            Assert.Equal("chill", result[0]);
            Assert.Equal("cold", result[1]);
            Assert.Equal("snow", result[2]);
            Assert.Equal("wind", result[3]);
        }
    }
}