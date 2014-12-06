using System.Globalization;
using System.IO;
using NFluent;

namespace Streams
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MemoryStreamTest
    {
        [TestMethod]
        [DeploymentItem("../../../Resources/Streams/TextFile1.txt")]
        public void TestMethod1()
        {
            const string fileName = "TextFile1.txt";
            var fileAsBytes = File.ReadAllBytes(fileName);
            var fileAsString = File.ReadAllText(fileName);

            using (var memoryStream = new MemoryStream(fileAsBytes))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                for (var i = 0; i < fileAsBytes.Length; i++)
                {
                    var charFromByte = System.Text.Encoding.UTF8.GetString(new byte[] { binaryReader.ReadByte() });
                    memoryStream.Position--;
                    var charDirectlyFromBinary = binaryReader.ReadChar().ToString(CultureInfo.InvariantCulture);
                    var charFromFile = fileAsString[i].ToString(CultureInfo.InvariantCulture);

                    Check.That(charFromFile).IsEqualTo(charFromByte).And.IsEqualTo(charDirectlyFromBinary);
                }
            }

        }
    }
}
