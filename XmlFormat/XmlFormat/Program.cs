using System.IO;
using System.Text;
using System.Xml;

//
// MIT License
// 
// Copyright (c) 2020 Pharap (@Pharap)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//

namespace XmlFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var file in args)
                Process(file);
        }

        static void Process(string file)
        {
            var tempFile = Path.GetTempFileName();

            using (var input = XmlReader.Create(File.OpenRead(file), readerSettings))
            using (var output = XmlWriter.Create(tempFile, writerSettings))
                output.WriteNode(input, true);

            File.Copy(tempFile, file, true);
            File.Delete(tempFile);
        }

        static readonly XmlReaderSettings readerSettings = new XmlReaderSettings()
        {
            CloseInput = true,
            DtdProcessing = DtdProcessing.Ignore,
            ConformanceLevel = ConformanceLevel.Fragment,
            IgnoreWhitespace = true,
            IgnoreComments = false,
        };

        static readonly XmlWriterSettings writerSettings = new XmlWriterSettings()
        {
            CloseOutput = true,
            Encoding = Encoding.UTF8,
            Indent = true,
            IndentChars = "\t",
            NewLineChars = "\r\n",
            NewLineOnAttributes = false,
            NewLineHandling = NewLineHandling.Replace,
            OmitXmlDeclaration = false,
            ConformanceLevel = ConformanceLevel.Fragment,
            CheckCharacters = true,
        };
    }
}
