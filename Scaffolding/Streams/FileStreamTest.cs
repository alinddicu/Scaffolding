using System.IO;
using NFluent;

namespace Streams
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FileStreamTest
    {
        [TestMethod]
        [DeploymentItem("../../../Resources/Streams/TextFile1.txt")]
        public void WhenCopyFromFileToFileThenTheResultIsCorrect()
        {
            using (var origin = new FileStream("TextFile1.txt", FileMode.Open))
            using (var result = new FileStream("TextFile2.txt", FileMode.CreateNew))
            {
                origin.CopyTo(result);
                var bytes = new byte[result.Length];
                result.Position = 0;
                result.Read(bytes, 0, (int)result.Length);
                origin.Position = 0;
                using (var memoryStream = new MemoryStream(bytes))
                {
                    for (var i = 0; i < memoryStream.Length; i++)
                    {
                        Check.That(memoryStream.ReadByte()).IsEqualTo(origin.ReadByte());
                    }
                }
            }
        }
    }
}
