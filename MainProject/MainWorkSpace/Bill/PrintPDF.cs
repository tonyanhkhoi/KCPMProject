﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using System.Threading.Tasks;
using MainProject.Model;

namespace MainProject.MainWorkSpace.Bill
{
    class PrintPDF
    {
        #region Fields
        static string dest = @".\Bill.pdf"; //Path.Combine(AppManager.GetPreferencesFolder(), "Bill.pdf");
        public static string dir_font = @".\vuArial.ttf";

        static NumberFormatInfo nfi;
        private Rectangle defaultSize;
        FileStream os;
        Document doc;
        PdfWriter writer;
        BaseFont basef;

        Store store = new Store();
        private string _name = Store.StoreName;
        private string _phone = Store.StorePhone;
        private string _address = Store.StoreAddress;
        #endregion //End Fields

        private static PrintPDF instance;
        public static PrintPDF Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrintPDF();
                }
                return instance;
            }
        }

        private PrintPDF()
        {
            basef = BaseFont.CreateFont(dir_font, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberGroupSeparator = " ";
            //open stream to write on the file
        }
        public bool createBill(BILL info)
        {
            //check if file is open
            try
            {
                using (FileStream r = File.OpenWrite(dest)) { }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not exists, create new file");
            }
            catch (Exception e)
            {
                Console.WriteLine("File is used by another process");
                WindowService.Instance.OpenMessageBox("Lỗi", "Tệp tin đang được mở, vui lòng tắt tệp tin trước rồi thử lại", System.Windows.MessageBoxImage.Error);
                return false;
            }

            os = new FileStream(dest, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            defaultSize = new Rectangle(PageSize.A5.Width, 300 + info.DETAILBILLs.Count * 24 + info.DETAILBILLs.Count(i => i.PRODUCT.Name.Length > 16) * 15);
            doc = new Document(defaultSize);
            doc.SetMargins(0, 0, 0, 0);

            writer = PdfWriter.GetInstance(doc, os);

            //start write
            doc.Open();

            //add title paragraph
            doc.Add(this.createTitle());

            //add name and date of the bill
            doc.Add(createInfoPurchase(info));

            //add product table 
            doc.Add(this.createTableInfoProduct(info));

            //add end paragraph
            doc.Add(createEndBill());

            //end write
            doc.Close();
            os.Close();
            writer.Close();

            doc.Dispose();
            os.Dispose();
            writer.Dispose();

            Process.Start(dest);
            Console.WriteLine("Create file PDF success");
            return true;

        }
        private Paragraph createTitle()
        {
            Paragraph title = new Paragraph();
            title.Add(new Paragraph(_name, new Font(basef, 18, Font.BOLD))
            { Alignment = Element.ALIGN_CENTER, SpacingAfter = 10 });
            title.Add(new Paragraph("Địa chỉ: " + _address, new Font(basef, 10, Font.ITALIC))
            { Alignment = Element.ALIGN_CENTER, SpacingAfter = 2 });
            title.Add(new Paragraph("Phone: " + _phone, new Font(basef, 10, Font.ITALIC))
            { Alignment = Element.ALIGN_CENTER, SpacingAfter = 15 });
            title.Add(new Paragraph("HOÁ ĐƠN THANH TOÁN", new Font(basef, 20, Font.BOLD))
            { Alignment = Element.ALIGN_CENTER, SpacingAfter = 25 });
            return title;
        }
        private PdfPTable createTableInfoProduct(BILL info)
        {
            Font font = new Font(basef, 12);
            PdfPTable table = new PdfPTable(4);
            table.DefaultCell.Border = PdfPCell.NO_BORDER;

            //set height column of table
            int[] columnWidths = new int[4];
            columnWidths[0] = 70;
            columnWidths[1] = 30;
            columnWidths[2] = 40;
            columnWidths[3] = 40;
            table.SetWidths(columnWidths);
            int defaultBorder = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER;

            table.AddCell(new PdfPCell(new Phrase("Tên món ăn", new Font(basef, 12, Font.BOLD)))
            { Border = defaultBorder, VerticalAlignment = PdfPCell.ALIGN_MIDDLE, PaddingBottom = 10, PaddingTop = 8 });
            table.AddCell(new PdfPCell(new Phrase("Số lượng", new Font(basef, 12, Font.BOLD)))
            { HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = defaultBorder, VerticalAlignment = PdfPCell.ALIGN_MIDDLE, PaddingBottom = 10, PaddingTop = 8 });
            table.AddCell(new PdfPCell(new Phrase("Đơn giá", new Font(basef, 12, Font.BOLD)))
            { HorizontalAlignment = PdfPCell.ALIGN_CENTER, Border = defaultBorder, VerticalAlignment = PdfPCell.ALIGN_MIDDLE, PaddingBottom = 10, PaddingTop = 8 });
            table.AddCell(new PdfPCell(new Phrase("Thành tiền", new Font(basef, 12, Font.BOLD)))
            { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = defaultBorder, VerticalAlignment = PdfPCell.ALIGN_MIDDLE, PaddingBottom = 10, PaddingTop = 8 });

            //add list product
            foreach (var item in info.DETAILBILLs)
            {
                table.AddCell(new PdfPCell(new Phrase(item.PRODUCT.Name, font))
                { Border = PdfPCell.NO_BORDER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE, PaddingBottom = 10 });

                table.AddCell(new PdfPCell(new Phrase(item.Quantity.ToString(), font))
                { Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE, PaddingBottom = 10 });

                table.AddCell(new PdfPCell(new Phrase(item.UnitPrice.ToString("N0", nfi) + "đ", font))
                { Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_CENTER, VerticalAlignment = PdfPCell.ALIGN_MIDDLE, PaddingBottom = 10 });

                table.AddCell(new PdfPCell(new Phrase((item.UnitPrice * item.Quantity).ToString("N0", nfi) + "đ", font))
                { Border = PdfPCell.NO_BORDER, HorizontalAlignment = PdfPCell.ALIGN_RIGHT, VerticalAlignment = PdfPCell.ALIGN_MIDDLE, PaddingBottom = 10 });
            }
            //----------------------------------------------------------------------------------------------------
            //add table sum price of bill
            //table.AddCell(new PdfPCell(new Phrase("Thành tiền:", new Font(basef, 12, Font.BOLD)))
            //{ Colspan = 3, Border = PdfPCell.TOP_BORDER, PaddingTop = 5 });

            //table.AddCell(new PdfPCell(new Phrase(info.TotalPrice.ToString("N0", nfi), new Font(basef, 12, Font.BOLD)))
            //{ HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 5 });

            //Giảm giá

            //table.AddCell(new PdfPCell(new Phrase("Giảm giá: ", new Font(basef, 10)))
            //{ Colspan = 3, Border = PdfPCell.NO_BORDER, PaddingBottom = 10 });

            //table.AddCell(new PdfPCell(new Phrase((info.TOTAL - info.Price_Bill).ToString("N0", nfi), new Font(basef, 10)))
            //{ HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER, PaddingBottom = 10 });

            //-----------------------------------------------------------------------------------------------------
            table.AddCell(new PdfPCell(new Phrase("Tổng cộng", new Font(basef, 13, Font.BOLD)))
            { Colspan = 3, Border = PdfPCell.TOP_BORDER, PaddingTop = 15 });

            table.AddCell(new PdfPCell(new Phrase(info.TotalPrice.ToString("N0", nfi) + " đ", new Font(basef, 13, Font.BOLD)))
            { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.TOP_BORDER, PaddingTop = 15 });

            table.AddCell(new PdfPCell(new Phrase("Tiền khách đưa", new Font(basef, 10, Font.NORMAL)))
            { Colspan = 3, Border = PdfPCell.NO_BORDER, PaddingTop = 10, PaddingLeft = 20 });

            table.AddCell(new PdfPCell(new Phrase(info.MoneyCustomer.ToString("N0", nfi) + " đ", new Font(basef, 10, Font.NORMAL)))
            { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER, PaddingTop = 10, PaddingLeft = 20 });

            table.AddCell(new PdfPCell(new Phrase("Tiền trả khách", new Font(basef, 8, Font.NORMAL)))
            { Colspan = 3, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10, PaddingLeft = 20 });

            table.AddCell(new PdfPCell(new Phrase((info.MoneyCustomer - info.TotalPrice).ToString("N0", nfi) + " đ", new Font(basef, 8, Font.NORMAL)))
            { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.BOTTOM_BORDER, PaddingBottom = 10, PaddingLeft = 20 });
            //-----------------------------------------------------------------------------------------------------           
            return table;
        }
        private Paragraph createEndBill()
        {
            Paragraph para = new Paragraph("Cảm ơn quý khách đã ghé, hẹn gặp lại quý khách!", new Font(basef, 10));
            para.Alignment = Element.ALIGN_CENTER;
            return para;
        }
        private PdfPTable createInfoPurchase(BILL info)
        {
            Font font = new Font(basef, 12);
            PdfPTable table = new PdfPTable(2);
            float[] columns = new float[2];
            columns[0] = 25;
            columns[1] = 55;
            table.SetWidths(columns);
            table.AddCell(new PdfPCell(new Phrase("Số bàn: " + (info.TABLE != null? info.TABLE.Name.ToString() : "Mang về"), font)) { HorizontalAlignment = PdfPCell.ALIGN_LEFT, Border = PdfPCell.NO_BORDER });
            table.AddCell(new PdfPCell(new Phrase("Ngày mua" + info.CheckoutDay.ToString(" hh:mm:ss, dd/MM/yyyy"), font)) { HorizontalAlignment = PdfPCell.ALIGN_RIGHT, Border = PdfPCell.NO_BORDER });
            table.SpacingAfter = 20;
            return table;
        }
    }
}
