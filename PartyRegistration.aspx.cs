using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;

namespace ElectionCommission
{
    public partial class PartRegistration : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);
        bool Flag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindState();
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue != String.Empty)
            {
                //BindVoterList(ddlState.SelectedValue.ToString().Trim());
            }
            else
            {

            }
        }

        protected void BtnSubmit_click(object sender, EventArgs e)
        {
            byte[] imageData = null;

            con.Open();
            SqlCommand cmd = new SqlCommand("Prc_InsertPartyReg", con);
            cmd.CommandType = CommandType.StoredProcedure;           
            cmd.Parameters.AddWithValue("@Name", TxtName.Text);
            cmd.Parameters.AddWithValue("@Address", TxtAddr.Text);
            cmd.Parameters.AddWithValue("@VoterId", TxtVoterId.Text);
            if (fileUpload.HasFile)
            {
                imageData = fileUpload.FileBytes;                
            }
            cmd.Parameters.AddWithValue("@Symbol", imageData);
            cmd.Parameters.AddWithValue("@State", ddlState.SelectedValue.ToString().Trim());
            SqlParameter ParamResult1 = new SqlParameter("@Result", SqlDbType.VarChar, 100);
            ParamResult1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ParamResult1);
            cmd.ExecuteNonQuery();
            if (ParamResult1.Value.ToString().Trim() == "Success")
            {
                //if (proof1.PostedFile.FileName != "")
                //{
                //    //ImageUploadforProof1();
                //    byte[] imageData = fileUpload.FileBytes;
                //    // Save the image to the database
                //    int imageId = SaveImageToDatabase(imageData);
                //}
                TxtVoterId.Text = "";
                TxtName.Text = "";
                TxtAddr.Text = "";
                TxtAddr.Text = "";
                lblStatus.Visible = true;
                lblStatus.Text = "Candidate Created Succssfully";
                lblStatus.ForeColor = System.Drawing.Color.BlueViolet;

            }
            else
            {
                lblStatus.Visible = true;
                lblStatus.Text = ParamResult1.Value.ToString();
                lblStatus.ForeColor = System.Drawing.Color.BlueViolet;
            }
            con.Close();


        }

        private void BindState()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter ad = new SqlDataAdapter("prc_getState", con);
            ad.SelectCommand.CommandTimeout = 10000;
            DataSet ds = new DataSet();
            ad.Fill(ds, "tblWono");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlState.DataTextField = ds.Tables[0].Columns[0].ToString();
                ddlState.DataValueField = ds.Tables[0].Columns[1].ToString();
                ddlState.DataSource = ds;
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("Please Select", String.Empty));
                ddlState.SelectedIndex = 1;
                //lnkCalldetails.Text = "Add Details";
            }

            con.Close();
            //obj = null;     
        }

        //private void ImageUploadforProof1()
        //{
        //    // Initialize variables
        //    lblStatus.Text = "";
        //    string sSavePath;
        //    string sThumbExtension;
        //    int intThumbWidth;
        //    int intThumbHeight;

        //    // Set constant values
        //    sSavePath = "PartSymbol/";
        //    sThumbExtension = "_thumb";
        //    intThumbWidth = 160;
        //    intThumbHeight = 120;

        //    string strServerPath;
        //    string strServerPathname;



        //    // If file field isn’t empty
        //    if (proof1.PostedFile != null)
        //    {
        //        // Check file size (mustn’t be 0)
        //        //HttpPostedFile myFile = filUpload.PostedFile;
        //        HttpPostedFile myFile = proof1.PostedFile;
        //        int nFileLen = myFile.ContentLength;
        //        if (nFileLen == 0)
        //        {
        //            Flag = false;
        //            lblStatus.Visible = true;
        //            lblStatus.Text = "Pls. Upload the Doument";
        //            return;
        //        }

        //        if (nFileLen > 204800)
        //        {
        //            lblStatus.Text = "The file size should not be greater than 200 KB.";
        //            lblStatus.Visible = true;
        //            Flag = false;
        //            return;
        //        }

        //        // Check file extension (must be JPG)
        //        //if (((System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".jpg") || (System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".jpeg") || (System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".png")))
        //        //{
        //        //    LblMsg.Visible = true;
        //        //    LblMsg.Text = "The file must have an extension of JPG";
        //        //    Flag = false;
        //        //    return;
        //        //}s

        //        // Read file into a data stream
        //        byte[] myData = new Byte[nFileLen];



        //        myFile.InputStream.Read(myData, 0, nFileLen);

        //        // Make sure a duplicate file doesn’t exist.  If it does, keep on appending an incremental numeric until it is unique
        //        //string sFilename = System.IO.Path.GetFileName(myFile.FileName);
        //        string sFilename = "proof" + TxtName.Text + ".jpg";
        //        int file_append = 0;

        //        strServerPath = Server.MapPath(sSavePath + sFilename);
        //        strServerPathname = Server.MapPath(sSavePath);
        //        //strServerPathname = strServerPathname.Replace("\\HPACCESSORIES\\HPACCESSORIES", "\\HPACCESSORIES");
        //        //strServerPath = strServerPath.Replace("\\HPACCESSORIES\\HPACCESSORIES", "\\HPACCESSORIES");



        //        if (System.IO.File.Exists(strServerPath))
        //        {
        //            System.IO.File.Delete(strServerPath);
        //        }
        //        while (System.IO.File.Exists(strServerPath))
        //        {
        //            file_append++;
        //            sFilename = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + file_append.ToString() + ".jpg";
        //        }

        //        // Save the stream to disk
        //        System.IO.FileStream newFile = new System.IO.FileStream(strServerPath, System.IO.FileMode.Create);
        //        newFile.Write(myData, 0, myData.Length);
        //        newFile.Close();

        //        // Check whether the file is really a JPEG by opening it
        //        System.Drawing.Image.GetThumbnailImageAbort myCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
        //        Bitmap myBitmap;
        //        try
        //        {
        //            //tdphoto.Visible=true;
        //            myBitmap = new Bitmap(strServerPath);

        //            // If jpg file is a jpeg, create a thumbnail filename that is unique.
        //            file_append = 0;
        //            //string sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + sThumbExtension + ".jpg";
        //            //string sThumbFile = Session["AppID"].ToString().Replace("/","-") + sThumbExtension + ".jpg";
        //            string sThumbFile = TxtName.Text + sThumbExtension + ".jpg";
        //            if ((System.IO.File.Exists(strServerPathname + sThumbFile)))
        //            {
        //                System.IO.File.Delete(strServerPathname + sThumbFile);
        //            }
        //            while (System.IO.File.Exists(strServerPathname + sThumbFile))
        //            {
        //                file_append++;
        //                sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + file_append.ToString() + sThumbExtension + ".jpg";
        //            }

        //            // Save thumbnail and output it onto the webpage
        //            System.Drawing.Image myThumbnail = myBitmap.GetThumbnailImage(intThumbWidth, intThumbHeight, myCallBack, IntPtr.Zero);
        //            //myThumbnail.Save(strServerPathname + sThumbFile);
        //            //imgPicture.ImageUrl = sSavePath + sThumbFile;

        //            // Displaying success information

        //            //LblMsg.Text = LblMsg.Text.ToString() + " - File uploaded successfully!";

        //            // Destroy objects
        //            myThumbnail.Dispose();
        //            myBitmap.Dispose();

        //        }
        //        catch (ArgumentException errArgument)
        //        {
        //            // The file wasn't a valid jpg file
        //            Flag = false;
        //            lblStatus.Visible = true;
        //            lblStatus.Text = "The file wasn't a valid jpg file.";
        //            //tdphoto.Visible=false;
        //            System.IO.File.Delete(strServerPath);
        //            return;
        //        }
        //    }
        //}

        public bool ThumbnailCallback()
        {
            return false;
        }

        private int SaveImageToDatabase(byte[] imageData)
        {
            using (SqlConnection connection = new SqlConnection("YourConnectionString"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Images (ImageData) VALUES (@ImageData); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@ImageData", imageData);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                byte[] imageData = fileUpload.FileBytes;

                // Save the image to the database
                int imageId = SaveImageToDatabase(imageData);

                // Store the image ID in Session
               // Session["ImageId"] = imageId;

            }
        }
    }
}