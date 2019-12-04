using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IronPdf;
using System.IO;

namespace PDFextraction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //create a collection to store all the file locations from foreach file
        Dictionary<string, string> pdfs = new Dictionary<string, string>();
        Dictionary<string, string> output = new Dictionary<string, string>();

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {

            //open dialog to select PDF files
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    lbFiles.Items.Add(file);
                    //add file paths to dictionary as key
                    pdfs.Add(file, "");
                }
                btnExtract.IsEnabled = true;
            }
        }

        private void BtnExtract_Click(object sender, RoutedEventArgs e)
        {

            foreach (KeyValuePair<string, string> pdf in pdfs)
            {
                PdfDocument p = PdfDocument.FromFile(pdf.Key);  //select pdf
                string pdfText = p.ExtractAllText();            //extract text from pdf

                //if author is specified, determine who it is
                if (pdfText.Contains("References") && !pdfText.Contains("Add references to this area"))
                {
                    string author = pdfText.Substring(pdfText.LastIndexOf("References") + 11);
                    author = author.Replace("\n", " ");
                    author = author.Replace("\r", " ");
                    author = author.Replace("\t", " ");
                    lbOutput.Items.Add(author);
                    
                    //add result to new dict
                    string path = pdf.Key;
                    string kbName = path.Substring(path.LastIndexOf("\\") + 1);
                    output[kbName] = author;     //save to dict and write to file all at once later
                }
                //if KB file doesn't have author specified
                else if (pdfText.Contains("Add references to this area")) 
                {
                    string path = pdf.Key;
                    string kbName = path.Substring(path.LastIndexOf("\\") + 1);
                    output[kbName] = "-----NO REFERENCES SPECIFIED-----";
                }
                else //just in case references field is missing, etc.. mark for review
                {
                    output[pdf.Key] = "???";
                }
            }

            btnSave.IsEnabled = true;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter f = new StreamWriter(@"C:\PEELAPPS\output.csv", true))
            {
                foreach (KeyValuePair<string, string> op in output)
                {
                    f.WriteLine(op.Key + "," + op.Value);
                }
            }
        }
    }
}
