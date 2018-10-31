using LumenWorks.Framework.IO.Csv;
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSV_Upload_Files.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                           
                        
                        }
                        return View(csvTable);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

        public ActionResult Import()
        {
            return View();
        }

        //protected void ImportCSV(object sender, EventArgs e)
        //{
        //    //Upload and save the file
        //    string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
        //    FileUpload1.SaveAs(csvPath);

        //    //Create a DataTable.
        //    DataTable dt = new DataTable();
        //    dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
        //new DataColumn("Name", typeof(string)),
        //new DataColumn("Country",typeof(string)) });

        //    //Read the contents of CSV file.
        //    string csvData = File.ReadAllText(csvPath);

        //    //Execute a loop over the rows.
        //    foreach (string row in csvData.Split('\n'))
        //    {
        //        if (!string.IsNullOrEmpty(row))
        //        {
        //            dt.Rows.Add();
        //            int i = 0;

        //            //Execute a loop over the columns.
        //            foreach (string cell in row.Split(','))
        //            {
        //                dt.Rows[dt.Rows.Count - 1][i] = cell;
        //                i++;
        //            }
        //        }
        //    }

        //    //Bind the DataTable.
        //    GridView.DataSource = dt;
        //    GridView.DataBind();
        //}
    }
}