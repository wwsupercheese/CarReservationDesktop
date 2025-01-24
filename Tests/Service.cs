using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Reposytory;
using System.Collections.Generic;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace Service.Tests
{
    [TestClass]
    public class MainTests
    {
        private Mock<DataBase> mockDataBase;
        private Mock<Translator> mockTranslator;
        private Main main;

        public MainTests()
        {
            mockDataBase = new Mock<DataBase>();
            mockTranslator = new Mock<Translator>();
            main = new Main(mockDataBase.Object, mockTranslator.Object);
        }

        [TestMethod]
        public void TestCustomSort()
        {
            // Arrange
            var cars = new List<Car>
            {
                new Car(new Dictionary<string, string> { { "price", "20000" } }),
                new Car(new Dictionary<string, string> { { "price", "15000" } }),
                new Car(new Dictionary<string, string> { { "price", "30000" } })
            };
            // Act
            var sortedCars = main.Custom_sort(cars, "price");

            // Assert
            Assert.AreEqual("15000", sortedCars[0]["price"]);
            Assert.AreEqual("20000", sortedCars[1]["price"]);
            Assert.AreEqual("30000", sortedCars[2]["price"]);
        }

        [TestMethod]
        public void TestIsString_WithInteger_ReturnsFalse()
        {
            // Act
            var result = main.Is_string("123");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsString_WithBoolean_ReturnsFalse()
        {
            // Act
            var result = main.Is_string("True");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIsString_WithNonNumeric_ReturnsTrue()
        {
            // Act
            var result = main.Is_string("Hello");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGetCondition_WithEmptySearch_ReturnsTrue()
        {
            // Arrange
            var search = new Dictionary<string, List<string>>();

            // Act
            var condition = main.Get_Condition(search);

            // Assert
            Assert.AreEqual("TRUE", condition);
        }

        [TestMethod]
        public void TestGetCondition_WithValidSearch_ReturnsCorrectCondition()
        {
            // Arrange
            var search = new Dictionary<string, List<string>>
            {
                { "colour", new List<string> { "Red", "Blue" } },
                { "price", new List<string> { "20000", "30000" } }
            };

            // Act
            var condition = main.Get_Condition(search);

            // Assert
            Assert.IsTrue(condition.Contains("colour = 'Red'") || condition.Contains("colour = 'Blue'"));
            Assert.IsTrue(condition.Contains("price = 20000") || condition.Contains("price = 30000"));
        }

        [TestMethod]
        public void TestGetCategorySearch_ReturnsCorrectCategories()
        {
            // Arrange
            var expectedCategories = new Dictionary<string, List<string>>
            {
                { "colour", new List<string> { "Red", "Blue" } },
                { "price", new List<string> { "15000", "20000" } }
            };

            mockDataBase.Setup(db => db.GetTable("SELECT column_name FROM information_schema.columns WHERE" +
                " table_name = 'Car' AND column_name NOT LIKE '%id%' AND column_name NOT LIKE '%image%' " +
                "UNION ALL SELECT column_name FROM information_schema.columns WHERE table_name = 'Model'" +
                "  AND column_name NOT LIKE '%id%';"))
            .Returns(new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "column_name", "colour" } },
                new Dictionary<string, string> { { "column_name", "price" } }
            });

            mockDataBase.Setup(db => db.GetTable("SELECT DISTINCT colour FROM \"Car\"" +
                " INNER JOIN \"Model\" ON \"Car\".model_id = \"Model\".id"))
            .Returns(new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> {{ "colour", "Blue" } },
                new Dictionary<string, string> { { "colour", "Red" } }
            });
            mockTranslator.Setup(t => t.GetTranslate(It.IsAny<string>()))
           .Returns((string input) => input);

            mockDataBase.Setup(db => db.GetTable("SELECT DISTINCT " + "price" +
                    " FROM \"Car\" INNER JOIN \"Model\" ON \"Car\".model_id = \"Model\".id"))
            .Returns(new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "price", "15000" } },
                new Dictionary<string, string> { { "price", "20000" } }
            });


            // Act
            var categories = main.GetCategorySearch();

            // Assert
            CollectionAssert.AreEquivalent(expectedCategories["colour"], categories["colour"]);
            CollectionAssert.AreEquivalent(expectedCategories["price"], categories["price"]);
        }

        [TestMethod]
        public void TestGetCategorySort_ReturnsCorrectSortCategories()
        {
            // Arrange
            var expectedSorts = new List<string> { "price" };

            mockDataBase.Setup(db => db.GetTable(It.IsAny<string>()))
                .Returns(new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string> { { "column_name", "price" } }
                });
            mockTranslator.Setup(t => t.GetTranslate(It.IsAny<string>()))
                .Returns((string input) => input);

            // Act
            var sorts = main.GetCategorySort();

            // Assert
            CollectionAssert.AreEquivalent(expectedSorts, sorts);
        }
        [TestMethod]
        public void Search_Cars_ReturnsExpectedCars()
        {
            // Arrange
            var searchInput = new Dictionary<string, List<string>>
            {
                { "brand_name", new List<string> { "Toyota", "Honda" } }
            };
            var sortInput = "price";

            mockTranslator.Setup(t => t.GetTranslate(It.IsAny<string>()))
                .Returns((string input) => input);
            mockTranslator.Setup(t => t.GetWord(It.IsAny<string>()))
                .Returns((string input) => input);
            mockTranslator.Setup(t => t.GetWord(searchInput))
                .Returns((Dictionary<string, List<string>> input) => input);
            mockTranslator.Setup(t => t.GetTranslate(It.IsAny<Dictionary<string, string>>()))
               .Returns((Dictionary<string, string> input) => input);


            var conditionString = "brand IN ('Toyota', 'Honda')";

            var tableResult = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "brand_name", "Toyota" }, { "price", "100" } },
                new Dictionary<string, string> { { "brand_name", "Honda" }, { "price", "50" } },
            };

            mockDataBase.Setup(db => db.GetTable(It.IsAny<string>())).Returns(tableResult);

            // Act
            var result = main.Search_Cars(searchInput, sortInput);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count); // Ожидаем, что будет 2 машины
            Assert.AreEqual("Honda", result[0]["brand_name"]); // Проверяем, что первая машина - Honda
                                                
        }
    }

    [TestClass]
    public class TranslatorTests
    {
        private Translator translator;

        [TestInitialize]
        public void Setup()
        {
            translator = new Translator();
        }

        [TestMethod]
        public void Add_ShouldAddTranslation()
        {
            // Arrange
            string key = "hello";
            string value = "привет";

            // Act
            translator.Add(key, value);

            // Assert
            Assert.AreEqual(value, translator.translate_base[key]);
            Assert.AreEqual(key, translator.uses_translate[value]);
        }

        [TestMethod]
        public void GetTranslate_ShouldReturnTranslatedWord()
        {
            // Arrange
            translator.Add("hello", "привет");

            // Act
            var result = translator.GetTranslate("hello");

            // Assert
            Assert.AreEqual("привет", result);
        }

        [TestMethod]
        public void GetTranslate_ShouldReturnOriginalWord_WhenTranslationNotFound()
        {
            // Act
            var result = translator.GetTranslate("unknown");

            // Assert
            Assert.AreEqual("unknown", result);
        }

        [TestMethod]
        public void GetWord_ShouldReturnOriginalWord_WhenTranslationExists()
        {
            // Arrange
            translator.Add("hello", "привет");

            // Act
            var result = translator.GetWord("привет");

            // Assert
            Assert.AreEqual("hello", result);
        }

        [TestMethod]
        public void GetWord_ShouldReturnOriginalWord_WhenTranslationNotFound()
        {
            // Act
            var result = translator.GetWord("unknown");

            // Assert
            Assert.AreEqual("unknown", result);
        }

        [TestMethod]
        public void GetTranslate_ShouldReturnTranslatedDictionary()
        {
            // Arrange
            var translations = new Dictionary<string, string>
            {
                { "hello", "привет" },
                { "world", "мир" }
            };

            foreach (var pair in translations)
            {
                translator.Add(pair.Key, pair.Value);
            }

            var input = new Dictionary<string, string>
            {
                { "hello", "world" }
            };

            // Act
            var result = translator.GetTranslate(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("мир", result["привет"]);
        }

        [TestMethod]
        public void GetWord_ShouldReturnTranslatedWordsFromDictionary()
        {
            // Arrange
            var translations = new Dictionary<string, string>
            {
                { "hello", "привет" },
                { "world", "мир" }
            };

            foreach (var pair in translations)
            {
                translator.Add(pair.Key, pair.Value);
            }

            var input = new Dictionary<string, List<string>>
            {
                { "greetings", new List<string> { "привет", "мир" } }
            };

            // Act
            var result = translator.GetWord(input);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2, result["greetings"].Count);
            Assert.AreEqual(result["greetings"][0], "hello");
            Assert.AreEqual(result["greetings"][1], "world");
        }
    }
}