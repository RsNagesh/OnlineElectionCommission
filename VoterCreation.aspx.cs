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
using System.Text.RegularExpressions;
using System.Collections;

namespace ElectionCommission
{
    public partial class VoterCreation : System.Web.UI.Page
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

        public static string Base64Decode(string base64EncodedData)
        {
            //var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            //return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(base64EncodedData);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        protected void BtnSubmit_click(object sender, EventArgs e)
        {
            string strpassword = Base64Encode(txtPasswd.Text);

            con.Open();
            SqlCommand cmd = new SqlCommand("Prc_InsertVoterReg", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VoterId", TxtVoterId.Text);
            cmd.Parameters.AddWithValue("@Name", TxtName.Text);
            cmd.Parameters.AddWithValue("@Address", TxtAddr.Text);
            cmd.Parameters.AddWithValue("@State", drpState.SelectedValue.ToString().Trim());
            cmd.Parameters.AddWithValue("@passwd", strpassword);            
            SqlParameter ParamResult1 = new SqlParameter("@Result", SqlDbType.VarChar, 100);
            ParamResult1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ParamResult1);
            cmd.ExecuteNonQuery();
            if (ParamResult1.Value.ToString().Trim() == "Success")            
            {
                if (proof1.PostedFile.FileName != "")
                {
                    ImageUploadforProof1();
                }
                TxtVoterId.Text = "";
                TxtName.Text = "";
                TxtAddr.Text = "";
                TxtAddr.Text = "";
                txtPasswd.Text = "";
                lblStatus.Visible = true;
                lblStatus.Text = "Login Created Succssfully";
                lblStatus.ForeColor = System.Drawing.Color.FloralWhite;

            }
            else
            {
                lblStatus.Visible = true;
                lblStatus.Text = ParamResult1.Value.ToString();
                lblStatus.ForeColor = System.Drawing.Color.FloralWhite;
            }
            con.Close();
            

        }

        private void ImageUploadforProof1()
        {
            // Initialize variables
            lblStatus.Text = "";
            string sSavePath;
            string sThumbExtension;
            int intThumbWidth;
            int intThumbHeight;

            // Set constant values
            sSavePath = "DocumentProof/";
            sThumbExtension = "_thumb";
            intThumbWidth = 160;
            intThumbHeight = 120;

            string strServerPath;
            string strServerPathname;



            // If file field isn’t empty
            if (proof1.PostedFile != null)
            {
                // Check file size (mustn’t be 0)
                //HttpPostedFile myFile = filUpload.PostedFile;
                HttpPostedFile myFile = proof1.PostedFile;
                int nFileLen = myFile.ContentLength;
                if (nFileLen == 0)
                {
                    Flag = false;
                    lblStatus.Visible = true;
                    lblStatus.Text = "Pls. Upload the Doument";
                    return;
                }

                if (nFileLen > 204800)
                {
                    lblStatus.Text = "The file size should not be greater than 200 KB.";
                    lblStatus.Visible = true;
                    Flag = false;
                    return;
                }

                // Check file extension (must be JPG)
                //if (((System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".jpg") || (System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".jpeg") || (System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".png")))
                //{
                //    LblMsg.Visible = true;
                //    LblMsg.Text = "The file must have an extension of JPG";
                //    Flag = false;
                //    return;
                //}s

                // Read file into a data stream
                byte[] myData = new Byte[nFileLen];



                myFile.InputStream.Read(myData, 0, nFileLen);

                // Make sure a duplicate file doesn’t exist.  If it does, keep on appending an incremental numeric until it is unique
                //string sFilename = System.IO.Path.GetFileName(myFile.FileName);
                string sFilename = "proof" + TxtName.Text + ".jpg";
                int file_append = 0;

                strServerPath = Server.MapPath(sSavePath + sFilename);
                strServerPathname = Server.MapPath(sSavePath);
                //strServerPathname = strServerPathname.Replace("\\HPACCESSORIES\\HPACCESSORIES", "\\HPACCESSORIES");
                //strServerPath = strServerPath.Replace("\\HPACCESSORIES\\HPACCESSORIES", "\\HPACCESSORIES");



                if (System.IO.File.Exists(strServerPath))
                {
                    System.IO.File.Delete(strServerPath);
                }
                while (System.IO.File.Exists(strServerPath))
                {
                    file_append++;
                    sFilename = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + file_append.ToString() + ".jpg";
                }

                // Save the stream to disk
                System.IO.FileStream newFile = new System.IO.FileStream(strServerPath, System.IO.FileMode.Create);
                newFile.Write(myData, 0, myData.Length);
                newFile.Close();

                // Check whether the file is really a JPEG by opening it
                System.Drawing.Image.GetThumbnailImageAbort myCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap;
                try
                {
                    //tdphoto.Visible=true;
                    myBitmap = new Bitmap(strServerPath);

                    // If jpg file is a jpeg, create a thumbnail filename that is unique.
                    file_append = 0;
                    //string sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + sThumbExtension + ".jpg";
                    //string sThumbFile = Session["AppID"].ToString().Replace("/","-") + sThumbExtension + ".jpg";
                    string sThumbFile = TxtName.Text + sThumbExtension + ".jpg";
                    if ((System.IO.File.Exists(strServerPathname + sThumbFile)))
                    {
                        System.IO.File.Delete(strServerPathname + sThumbFile);
                    }
                    while (System.IO.File.Exists(strServerPathname + sThumbFile))
                    {
                        file_append++;
                        sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) + file_append.ToString() + sThumbExtension + ".jpg";
                    }

                    // Save thumbnail and output it onto the webpage
                    System.Drawing.Image myThumbnail = myBitmap.GetThumbnailImage(intThumbWidth, intThumbHeight, myCallBack, IntPtr.Zero);
                    //myThumbnail.Save(strServerPathname + sThumbFile);
                    //imgPicture.ImageUrl = sSavePath + sThumbFile;

                    // Displaying success information

                    //LblMsg.Text = LblMsg.Text.ToString() + " - File uploaded successfully!";

                    // Destroy objects
                    myThumbnail.Dispose();
                    myBitmap.Dispose();

                }
                catch (ArgumentException errArgument)
                {
                    // The file wasn't a valid jpg file
                    Flag = false;
                    lblStatus.Visible = true;
                    lblStatus.Text = "The file wasn't a valid jpg file.";
                    //tdphoto.Visible=false;
                    System.IO.File.Delete(strServerPath);
                    return;
                }
            }
        }

        public bool ThumbnailCallback()
        {
            return false;
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
                drpState.DataTextField = ds.Tables[0].Columns[0].ToString();
                drpState.DataValueField = ds.Tables[0].Columns[1].ToString();
                drpState.DataSource = ds;
                drpState.DataBind();
                drpState.Items.Insert(0, new ListItem("Please Select", String.Empty));
                drpState.SelectedIndex = 1;
                //lnkCalldetails.Text = "Add Details";
            }

            con.Close();
            //obj = null;     
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpState.SelectedValue != String.Empty)
            {
                //Bindwono1(ddlwono.SelectedValue.ToString().Trim());
            }
            else
            {

            }
        }

    }
}