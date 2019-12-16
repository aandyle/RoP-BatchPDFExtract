# RoP-BatchPDFExtract
Bulk extract references field from Knowledge Base articles and Technical Documentation articles on the Region of Peel internal knowledge base. Export and append all results to a .csv file.

## Getting Started

Create folder C:\PEELAPPS\ with full permissions on your machine. A output.csv file will be created in this location with the file name and extracted data for each file. If this file exists, data will be appended to the file. 

## Usage

Region of Peel's KB/TD files contain References field at the end of the document. Select target PDF files, click extract then click save to save the results to a .csv file.

![Screenshot](https://github.com/aandyle/RoP-BatchPDFExtract/blob/master/screenshot%20redacted.PNG)

## Limitations

Max 40 PDF files can be selected at a time with PDF library development usage limits.

## Built With

* [IronPDF](https://ironpdf.com/) - Library to read PDF files; development usage only

## Authors

* **Andy Le** - *Design/Development* - [aandyle](https://github.com/aandyle/)
