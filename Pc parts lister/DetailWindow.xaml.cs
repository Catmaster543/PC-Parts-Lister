using Pc_parts_lister;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace Pc_parts_lister
{
    public partial class DetailWindow : Window
    {
        public Component Komponenta { get; }

        public DetailWindow(Component komponenta)
        {
            InitializeComponent();

            Komponenta = komponenta;
            DataContext = Komponenta;

            MarkDownRenderer markDownRenderer = new MarkDownRenderer();

            Description_FlowDocument.Document = markDownRenderer.Render(Komponenta.Description);

            if (komponenta.imagePaths.Count > 0)
            {
                Null_Photos_TextBlock.Visibility = Visibility.Collapsed;
            }

            if (komponenta.FullImagePath != null)
            {
                Null_Main_Pic_TextBlock.Visibility = Visibility.Collapsed;
            }

            #region Hiding
            PowerBox.Visibility = Visibility.Collapsed;
            CapacityBox.Visibility = Visibility.Collapsed;
            #endregion

            /*
            if (Komponenta.Type == "CPU")
            {
                TypeBox2.Visibility = Visibility.Collapsed;
            }
            else if (Komponenta.Type == "Disk")
            {
                CapacityBox.Visibility = Visibility.Visible;
                SerBox.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
            }
            else if (Komponenta.Type == "GPU")
            {
                SubSerBox.Visibility = Visibility.Collapsed;
            }
            else if (komponenta.Type == "RAM")
            {
                SubSerBox.Visibility = Visibility.Collapsed;
            }
            else if (komponenta.Type == "Mb")
            {
                SerBox.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
            }
            else if (komponenta.Type == "PSU")
            {
                TypeBox2.Visibility = Visibility.Collapsed;
                SerBox.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
                PowerBox.Visibility = Visibility.Visible;
            }
            else if (komponenta.Type == "Case")
            {
                SerBox.Visibility = Visibility.Collapsed;
                SubSerBox.Visibility = Visibility.Collapsed;
            }

            MainPicButton.Click += OpenImage_Click;

            if (Komponenta.imagePaths != null)
            {
                for (int i = 0; i < Komponenta.imagePaths.Count; i++)
                {
                    if (Komponenta.imagePaths[i] != null && File.Exists(Komponenta.imagePaths[i]))
                    {
                        ConstructAnImageFrame(Komponenta.imagePaths[i]);
                    }
                    else if (!File.Exists(Komponenta.imagePaths[i]))
                    {
                        Komponenta.imagePaths.Remove(Komponenta.imagePaths[i]);
                    }
                }
            }
            /* FlowDoc testground
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run("Hello stupid FlowDoctor"));
            document.Blocks.Add(paragraph);
            Description_FlowDocument.Document = document;
            */
        }



        // Původ z Edit window
        void ConstructAnImageFrame(string imagePath)
        {
            Grid grid = new Grid();
            Button deleteButton = new Button();
            Button imageButton = new Button();

            ImagesPanel.Children.Insert(ImagesPanel.Children.Count - 1, grid);
            grid.Margin = new Thickness(7);
            grid.Width = 60;
            grid.Height = 60;

            if (File.Exists(imagePath))
            {
                ImageBrush imageBrush = new ImageBrush(LoadImage(imagePath));
                imageBrush.Stretch = Stretch.Uniform;
                imageButton.Background = imageBrush;
            }
            else if (!File.Exists(imagePath))
            {
                return;
            }
            imageButton.Width = 60;
            imageButton.Height = 60;
            imageButton.BorderThickness = new Thickness(0);
            imageButton.Tag = imagePath;
            imageButton.Click += OpenImage_Click;
            grid.Children.Add(imageButton);
        }

        private void OpenImage_Click(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Photo_View Photo_Page = new Photo_View(button.Tag.ToString(), Komponenta);
            Photo_Page.ShowDialog();
        }

        BitmapImage LoadImage(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(path, UriKind.Relative);
            bitmap.EndInit();
            bitmap.Freeze();

            return bitmap;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }



    public class MarkDownRenderer
    {
        public Component Komponenta { get; }

        public FlowDocument Render(string markdown)
        {
            FlowDocument flowDocument = new FlowDocument();
            string[] lines = BreakDownStringToLines(markdown);
            Paragraph[] paragraphs = InspectLines(lines);
            foreach (Paragraph paragraph in paragraphs)
            {
                flowDocument.Blocks.Add(paragraph);
            }
            return flowDocument;
        }

        public string[] BreakDownStringToLines(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            return lines;
        }

        public Paragraph[] InspectLines(string[] lines)
        {
            Paragraph[] paragraphs = new Paragraph[lines.Count()];
            int i = 0;
            foreach (string line in lines)
            {
                if (line.Length <= 1 || string.IsNullOrWhiteSpace(line))
                {
                    paragraphs[i] = new Paragraph();
                }
                else if (line[0] == '#' && line[1] != '#')
                {
                    paragraphs[i] = ParseHeading(line);
                }
                else if (line[0] == '#' && line[1] == '#')
                {
                    paragraphs[i] = ParseHeading2(line);
                }
                else
                {
                    paragraphs[i] = ParseParagraph(line);
                }
                i++;
            }
            return paragraphs;
        }

        public Paragraph ParseParagraph(string paragraphText)
        {
            Paragraph paragraph = new Paragraph();
            List<Inline> inlines = ParseInline(paragraphText);
            foreach (Inline inline in inlines)
            {
                paragraph.Inlines.Add(inline);
            }
            return paragraph;
        }

        public Paragraph ParseHeading(string headingText) 
        {
            string outHeadingText = headingText.Trim('#');
            Paragraph paragraph = new Paragraph();
            paragraph.FontSize = 28;
            ParseInline(outHeadingText);
            List<Inline> inlines = ParseInline(outHeadingText);
            foreach (Inline inline in inlines)
            {
                paragraph.Inlines.Add(inline);
            }
            return paragraph;
        }

        public Paragraph ParseHeading2(string headingText)
        {
            string outHeadingText = headingText.Trim('#');
            Paragraph paragraph = new Paragraph();
            paragraph.FontSize = 24;
            ParseInline(outHeadingText);
            List<Inline> inlines = ParseInline(outHeadingText);
            foreach (Inline inline in inlines)
            {
                paragraph.Inlines.Add(inline);
            }
            return paragraph;
        }

        public List<Inline> ParseInline(string text)
        {
            string buffer = "";
            bool isItalic = false;
            bool isBold = false;
            List<Inline> inlines = new List<Inline>();
            for (int i = 0; i < text.Length; i++)
            {
                if (i+1 == text.Length)
                {
                    if (text[i] == '*')
                    {
                        if (isItalic)
                        {
                            isItalic = false;

                            Italic italic = new Italic();
                            italic.Inlines.Add(new Run(buffer));
                            inlines.Add(italic);
                            ClearBuffer(ref buffer);
                        }
                        else if (isBold)
                        {
                            isBold = false;

                            Bold bold = new Bold();
                            bold.Inlines.Add(new Run(buffer));
                            inlines.Add(bold);
                            ClearBuffer(ref buffer);
                        }
                    }
                    else
                    {
                        buffer += text[i];
                        Run run = new Run(buffer);
                        inlines.Add(run);
                        ClearBuffer(ref buffer);
                    }
                }

                else if (text[i] == '*' && text[i + 1] != '*')
                {
                    if (isItalic && !isBold)
                    {
                        isItalic = false;

                        Italic italic = new Italic();
                        italic.Inlines.Add(new Run(buffer));
                        inlines.Add(italic);

                        ClearBuffer(ref buffer);
                    }
                    else if (isBold && !isItalic)
                    {
                        isItalic = true;

                        Bold bold = new Bold();
                        bold.Inlines.Add(new Run(buffer));
                        inlines.Add(bold);

                        ClearBuffer(ref buffer);

                        
                    }
                    else if (isItalic)
                    {
                        isItalic = false;

                        Italic italic = new Italic();
                        italic.Inlines.Add(new Run(buffer));

                        Bold italicBold = new Bold();
                        italicBold.Inlines.Add(new Italic(italic));

                        inlines.Add(italicBold);

                        ClearBuffer(ref buffer);
                    }
                    else
                    {
                        isItalic = true;

                        Run run = new Run(buffer);
                        inlines.Add(run);
                        ClearBuffer(ref buffer);
                    }
                }

                else if (text[i] == '*' && text[i + 1] == '*')
                {
                    i++;
                    if (isBold && !isItalic)
                    {
                        isBold = false;

                        Bold bold = new Bold();
                        bold.Inlines.Add(new Run(buffer));
                        inlines.Add(bold);
                        ClearBuffer(ref buffer);
                    }
                    else if (isItalic && !isBold)
                    {
                        isBold = true;

                        Italic italic = new Italic();
                        italic.Inlines.Add(new Run(buffer));
                        inlines.Add(italic);

                        ClearBuffer(ref buffer);
                    }
                    else if (isBold)
                    {
                        isBold = false;

                        Bold bold = new Bold();
                        bold.Inlines.Add(new Run(buffer));
                        
                        Italic boldItalic = new Italic();
                        boldItalic.Inlines.Add(new Bold(bold));

                        inlines.Add(boldItalic);
                                
                        ClearBuffer(ref buffer);
                    }
                    else
                    {
                        isBold = true;

                        Run run = new Run(buffer);
                        inlines.Add(run);
                        ClearBuffer(ref buffer);
                    }
                }
                else
                {
                    buffer += text[i];
                }
            }
            return inlines;
        }

        public void ClearBuffer(ref string buffer)
        {
            buffer = "";
        }
    }
}


