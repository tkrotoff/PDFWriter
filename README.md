# Experimental C# library to create PDF files

Takes a C# DataSet object and converts it to PDF file format.

## Example

```C#
DataSet data = new DataSet("Sample");
data.ReadXml("mydataset.xml");

PageLayout pageLayout = new PageLayout();
pageLayout.RightHeader = "Current Date";
Page.PageLayout = pageLayout;

string tmp = PDFWriter.GetPDF(data);

StreamWriter fileWriter = new StreamWriter("mydataset.pdf");
fileWriter.Write(tmp);
fileWriter.Close();
```

## Implementation

The implementation is experimental. Still the source code is simple, clean, documented and should be pretty stable.
A number of unit tests come with the source code.
No external dependencies, PDFWriter creates PDF files from scratch.
