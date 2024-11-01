using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PasswordApp
{
    public partial class _Default : Page
    {
        public static string encryptedContent = string.Empty;
        public static string filePath = ConfigurationManager.AppSettings["FilePath"];
        private byte[] encryptionKey = Cryptography.CreateKey(ConfigurationManager.AppSettings["Key"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (File.Exists(filePath))
                {
                    encryptedContent = File.ReadAllText(filePath).Trim();
                    ViewState["EncryptedContent"] = encryptedContent;
                    BindGV();
                }
            }
            else
            {
                encryptedContent = ViewState["EncryptedContent"]?.ToString();
            }
        }

        private void BindGV()
        {
            var data = LoadFile();

            passwordGV.DataSource = data;
            passwordGV.DataBind();
        }

        // Parse each line as a single entry
        private List<PasswordEntry> LoadFile()
        {
            List<PasswordEntry> list = encryptedContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new PasswordEntry
                    {
                        Site = parts[0].Trim(),
                        EmailorUsername = parts[1].Trim(),
                        Password = Cryptography.Decrypt(parts[2].Trim(), encryptionKey)
                    };
                }).ToList();

            return list;
        }

        protected void passwordGV_RowEditing(object sender, GridViewEditEventArgs e)
        {
            passwordGV.EditIndex = e.NewEditIndex;
            BindGV();
        }

        protected void passwordGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            passwordGV.EditIndex = -1;
            BindGV();
        }

        protected void passwordGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            GridViewRow row = passwordGV.Rows[index];

            string site = (row.Cells[0].Controls[0] as TextBox)?.Text;
            string username = (row.Cells[1].Controls[0] as TextBox)?.Text;
            string password = (row.Cells[2].Controls[0] as TextBox)?.Text;

            var lines = LoadFile();

            // Update the specific line with the new values
            lines[index] = new PasswordEntry { Site = site, EmailorUsername = username, Password = password };

            // Re-encrypt the updated data
            encryptedContent = string.Join(Environment.NewLine, lines.Select(entry =>
            {
                string encryptedPassword = Cryptography.Encrypt(entry.Password, encryptionKey);
                return $"{entry.Site},{entry.EmailorUsername},{encryptedPassword}";
            }));

            File.WriteAllText(filePath, encryptedContent);
            ViewState["EncryptedContent"] = encryptedContent;

            passwordGV.EditIndex = -1;
            BindGV();
        }

        protected void passwordGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;

            var lines = LoadFile();

            // Remove the entry at the specified index
            if (index >= 0 && index < lines.Count)
            {
                lines.RemoveAt(index);
            }

            encryptedContent = string.Join(Environment.NewLine, lines.Select(entry =>
            {
                string encryptedPassword = Cryptography.Encrypt(entry.Password, encryptionKey);
                return $"{entry.Site},{entry.EmailorUsername},{encryptedPassword}";
            }));

            File.WriteAllText(filePath, encryptedContent);
            ViewState["EncryptedContent"] = encryptedContent;

            BindGV();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Collect data from GridView and format it for saving
            var data = new System.Text.StringBuilder();
            foreach (GridViewRow row in passwordGV.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string site = row.Cells[0].Text;
                    string username = row.Cells[1].Text; 
                    string password = Cryptography.Encrypt(row.Cells[2].Text, encryptionKey);

                    data.AppendLine($"{site}, {username}, {password}");
                }
            }

            File.WriteAllText(filePath, data.ToString());
        }

        protected void TabMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            TabView.ActiveViewIndex = Convert.ToInt32(e.Item.Value);
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            // Retrieve input values
            string site = txtNewSite.Text.Trim();
            string username = txtNewUsername.Text.Trim();
            string password = txtNewPassword.Text.Trim();

            if (!string.IsNullOrEmpty(site) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                string encryptedPassword = Cryptography.Encrypt(password, encryptionKey);

                string newEntry = $"{site},{username},{encryptedPassword}";

                // Append the new entry to the encrypted content and save it to the file
                encryptedContent += Environment.NewLine + newEntry;
                File.WriteAllText(filePath, encryptedContent);

                // Clear input fields
                txtNewSite.Text = "";
                txtNewUsername.Text = "";
                txtNewPassword.Text = "";

                ViewState["EncryptedContent"] = encryptedContent;

                BindGV();
            }
        }

    }

    public class PasswordEntry
    {
        public string Site { get; set; }
        public string EmailorUsername { get; set; }
        public string Password { get; set; }

    }
}