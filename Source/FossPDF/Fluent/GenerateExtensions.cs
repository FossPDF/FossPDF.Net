using System;
using System.Collections.Generic;
using System.IO;
using FossPDF.Drawing;
using FossPDF.Infrastructure;

namespace FossPDF.Fluent
{
    public static class GenerateExtensions
    {
        #region PDF
        
        public static byte[] GeneratePdf(this IDocument document)
        {
            return document.GeneratePdf(out var _);
        }

        public static byte[] GeneratePdf(this IDocument document, out int totalPages)
        {
            using var stream = new MemoryStream();
            totalPages = DocumentGenerator.GeneratePdf(stream, document);
            return stream.ToArray();
        }
        
        public static void GeneratePdf(this IDocument document, string filePath)
        {
            var data = document.GeneratePdf();
            
            if (File.Exists(filePath))
                File.Delete(filePath);
            
            File.WriteAllBytes(filePath, data);
        }

        public static void GeneratePdf(this IDocument document, Stream stream)
        {
            var data = document.GeneratePdf();
            stream.Write(data, 0, data.Length);
        }
        
        #endregion

        #region XPS
        
        public static byte[] GenerateXps(this IDocument document)
        {
            using var stream = new MemoryStream();
            document.GenerateXps(stream);
            return stream.ToArray();
        }
        
        public static void GenerateXps(this IDocument document, string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Create);
            document.GenerateXps(stream);
        }

        public static void GenerateXps(this IDocument document, Stream stream)
        {
            DocumentGenerator.GenerateXps(stream, document);
        }
        
        #endregion

        #region Images

        public static IEnumerable<byte[]> GenerateImages(this IDocument document)
        {
            return DocumentGenerator.GenerateImages(document);
        }
        
        /// <param name="filePath">Method should return fileName for given index</param>
        public static void GenerateImages(this IDocument document, Func<int, string> filePath)
        {
            var index = 0;
            
            foreach (var imageData in document.GenerateImages())
            {
                var path = filePath(index);
                
                if (File.Exists(path))
                    File.Delete(path);
                
                File.WriteAllBytes(path, imageData);
                index++;
            }
        }

        #endregion

        #region SVG

        public static IEnumerable<string> GenerateSvg(this IDocument document)
        {
            return DocumentGenerator.GenerateSvg(document);
        }

        /// <param name="filePath">Method should return fileName for given index</param>
        public static void GenerateSvg(this IDocument document, Func<int, string> filePath)
        {
            var index = 0;

            foreach (var svgString in document.GenerateSvg())
            {
                var path = filePath(index);

                if (File.Exists(path))
                    File.Delete(path);

                File.WriteAllText(path, svgString);
                index++;
            }
        }

        #endregion
    }
}