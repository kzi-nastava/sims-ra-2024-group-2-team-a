using PdfiumViewer;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace BookingApp.WPF.Utils.Reports {
    public class PdfPreviewGenerator {
        public static System.Drawing.Image GeneratePdfPreview(IDocument document) {
            using (var stream = new MemoryStream()) {
                QuestPDF.Settings.License = LicenseType.Community;
                document.GeneratePdf(stream);

                stream.Position = 0;

                using (var pdfDocument = PdfDocument.Load(stream)) {
                    var pageImage = pdfDocument.Render(0, 300, 300, true);
                    return pageImage;
                }
            }
        }
    }
}
