﻿namespace Streams
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;
    using System.Globalization;
    using System.IO;
    using System.Text;

    [TestClass]
    public class MemoryStreamTest
    {
        [TestMethod]
        [DeploymentItem("../../../Resources/Streams/TextFile1.txt")]
        public void WhenReadFromTextFileIntoMemoryStreamThenBytesCanConvertToCorrectStrings()
        {
            const string fileName = "TextFile1.txt";
            var fileAsBytes = File.ReadAllBytes(fileName);
            var fileAsString = File.ReadAllText(fileName);

            using (var memoryStream = new MemoryStream(fileAsBytes))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                for (var i = 0; i < fileAsBytes.Length; i++)
                {
                    var charFromByte = Encoding.UTF8.GetString(new[] { binaryReader.ReadByte() });
                    memoryStream.Position--;
                    var charDirectlyFromBinary = binaryReader.ReadChar().ToString(CultureInfo.InvariantCulture);
                    var charFromFile = fileAsString[i].ToString(CultureInfo.InvariantCulture);

                    Check.That(charFromFile).IsEqualTo(charFromByte).And.IsEqualTo(charDirectlyFromBinary);
                }
            }

        }
    }
}
